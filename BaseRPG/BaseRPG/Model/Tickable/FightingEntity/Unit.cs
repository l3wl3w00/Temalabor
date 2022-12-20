﻿using BaseRPG.Model.Attribute;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Data;
using BaseRPG.Model.Effects;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.Movement;
using BaseRPG.Model.ReflectionStuff;
using BaseRPG.Model.ReflectionStuff.InteractionBuilder;
using BaseRPG.Model.ReflectionStuff.InteractionBuilder.Factory;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.State;
using BaseRPG.Model.State.CrowdControl;
using BaseRPG.Model.State.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : GameObject, IAttackable, IAttacking, ICollisionDetector
    {

        private Health health;
        private IMovementManager movementManager;
        private SkillManager skillManager;
        /// <summary>
        /// This field describes how this unit moves.
        /// This is NOT the only way a unit can move,
        /// for example the player can move their own unit as they like
        /// </summary>
        private Default<IMovementStrategy> movementStrategy;
        private double speed;
        private bool exists = true;
        private EffectManager effectManager = new();
        private DamageTakingStateHandler damageTakingStateHandler;
        private MovementStateHandler movementStateHandler;
        public override event Action OnCeaseToExist;
        public SkillManager SkillManager { get => skillManager; set { skillManager = value; } }
        public Health Health { get { return health; } }
        public int Damage => throw new NotImplementedException();
        public IMovementUnit NextMovement { get => MovementStrategy.CalculateNextMovement(MovementManager, speed); }
        public IPositionUnit Position { get { return movementManager.Position; } }
        public double Armor { get; } = 0;
        public IMovementUnit LastMovement
        {
            get
            {
                return movementManager.LastMovement;
            }
        }

        protected Unit(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy, SkillManager skillManager, World currentWorld, bool addToWorldInstantly = true) : base(currentWorld, addToWorldInstantly)
        {
            health = new Health(maxHp);
            this.skillManager = skillManager;
            health.HealthReachedZeroEvent += () => SetExists(false);
            this.movementManager = movementManager;
            this.movementStrategy = new(movementStrategy);
            damageTakingStateHandler = new(new NormalDamageTakingState(this));
            movementStateHandler = new(new NormalMovementState());
        }

        public abstract AttackBuilder AttackFactory(string v);

        public IMovementManager MovementManager => movementManager;
        public override void BeforeStep(double delta)
        {
            base.BeforeStep(delta);
            if (!(this is Hero.Hero))
            effectManager.OnTick(delta);
            var nextMovement = NextMovement;
            if(nextMovement != null)
                _queueMovement(nextMovement.Scaled(delta));
        }
        public override void Step(double delta) {
            MovementManager.MoveQueued();
        }
        public bool CastSkill<PARAM_TYPE>(string name, PARAM_TYPE skillCastParams)
        {
            return skillManager.Cast(name, skillCastParams);
        }

        public void StopMoving() {
            MovementStrategy = new EmptyMovementStrategy();
        }
        public void StartMoving()
        {
            movementStrategy.Reset();
        }
        //public virtual void Attack(string attackName) {
        //    attacks[attackName.ToLower()]?.CreateAttack(this,movementManager.Position);

        public bool SwitchDamageTakingState(IDamageTakingState damageTakingState) {
            return damageTakingStateHandler.SwichState(damageTakingState);
        }
        public void BackToPreviousDamageTakingState() {
            damageTakingStateHandler.BackToPreviousState();
        }
        public bool SwitchMovementState(IMovementState newState)
        {
            return movementStateHandler.SwichState(newState);
        }
        public void BackToPreviousMovementState()
        {
            movementStateHandler.BackToPreviousState();
        }

        public void QueueMovement(IMovementUnit movement) {
            this.QueueAction(()=> _queueMovement(movement));
        }

        public void MoveInstantly(IMovementUnit movement)
        {
            movementManager.Move(movement);
        }
        private void _queueMovement(IMovementUnit movementUnit) {
            if (movementUnit == null) {
                movementManager.QueueMovement(movementUnit);
                return;
            }
            movementManager.QueueMovement(movementStateHandler.CalculateMovement(movementUnit));
        }


        protected abstract string Type { get; }

        protected virtual void OnExistsSet(bool value) { }
        public override bool Exists => exists;
        private void SetExists(bool value){
            if (!value)
            {
                OnCeaseToExist?.Invoke();
            }
            OnExistsSet(value);
            exists = value;
        }

        public abstract AttackabilityService.Group OffensiveGroup { get; }
        public abstract AttackabilityService.Group DefensiveGroup { get; }
        public IMovementStrategy MovementStrategy {
            get => movementStrategy.CurrentValue;
            set => movementStrategy.CurrentValue = value;
        }
        public IMovementStrategy DefaultMovementStrategy
        {
            get => movementStrategy.DefaultValue;
            set => movementStrategy.DefaultValue = value;
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            string key = Type;
            if (dict.ContainsKey(key))
            {
                dict[key].Add(this);
                return;
            }
            dict.Add(key, new List<ISeparable> { this });
        }

        public void TakeDamage(double damage,IAttacking attacker)
        {
            if (!Exists) return;
            health.CurrentValue -= damageTakingStateHandler.CalculateDamage(damage);
            if (!Exists){
                OnKilledBy(attacker);
            }
        }
        public void OnKilledBy(IAttacking attacker)
        {
            InteractionFactory.Instance
                .CreateAttackInteraction(attacker,this)
                .Attack();
        }
        public void AddEffect(Effect effect) {
            effectManager.AddEffect(effect);
        }

        bool ICollisionDetector.CanCollide(ICollisionDetector other)
        {
            return true;
        }
        public void SeletBySkillTargetability(LinkedList<Unit> targetableUnits, LinkedList<ICollisionDetector> targetableOther) {
            targetableUnits.AddLast(this);
        }

        public abstract class Builder {
            private double speed = 100;
            private int maxHp;
            private IMovementManager movementManager;
            private IMovementStrategy movementStrategy;
            private readonly World world;
            private SkillManager skillManager = new();
            private GenericCallbackQueue<Unit> callbackQueue = new();

            protected World World => world;

            protected Builder(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy, World world)
            {
                this.maxHp = maxHp;
                this.movementManager = movementManager;
                this.movementStrategy = movementStrategy;
                this.world = world;
            }
            public Builder SkillManager(SkillManager skillManager) {
                this.skillManager = skillManager;
                return this;
            }
            public Builder Speed(double value) {
                this.speed = value;
                return this;
            }
            public Unit Build() {
                Unit unit =  Build(maxHp, movementManager, movementStrategy, skillManager, World);
                unit.speed = speed;
                callbackQueue.ExecuteAll(unit);
                return unit;
            }
            public abstract Unit Build(int maxHp, IMovementManager movementManager,
                IMovementStrategy movementStrategy, SkillManager skillManager, World world);
        }
    }
}
