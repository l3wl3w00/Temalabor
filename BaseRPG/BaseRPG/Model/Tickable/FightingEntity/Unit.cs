using BaseRPG.Controller.Utility;
using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : IGameObject, IAttackable, IAttacking, ISkillCaster, ICollisionDetector<IGameObject>
    {

        private Health health;
        private IMovementManager movementManager;

        // This field describes how this unit moves.
        // This is NOT the only way a unit can move,
        // for example the player can move their own unit as they like
        private Default<IMovementStrategy> movementStrategy;

        private Stat damage;
        private double speed = 100;
        private bool exists = true;

        public event Action OnCeaseToExist;

        public Health Health { get { return health; } }
        public int Damage => throw new NotImplementedException();
        public IMovementUnit NextMovement { get => MovementStrategy.CalculateNextMovement(MovementManager, speed); }
        public IPositionUnit Position { get { return movementManager.Position; } }
        public IMovementUnit LastMovement
        {
            get
            {
                return movementManager.LastMovement;
            }
        }

        public abstract AttackBuilder AttackFactory(string v);

        public IMovementManager MovementManager => movementManager;

        public virtual void OnTick(double delta) {
            MovementManager.Move(NextMovement?.Scaled(delta));
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


        public int CalculateDamage() {
            return damage.Value;
        }

        public void Move(IMovementUnit movement) {
            movementManager.Move(movement);
        }

        public Unit(int maxHp, IMovementManager movementManager, 
            IMovementStrategy movementStrategy ) {
            health = new Health(maxHp);
            health.HealthReachedZeroEvent += () => Exists = false;
            this.movementManager = movementManager;
            this.movementStrategy = new(movementStrategy);
        }

        protected abstract string Type { get; }

        protected virtual void OnExistsSet(bool value) { }
        public bool Exists { 
            get 
            {
                return exists;
            }
            set 
            {
                if (!value) {
                    OnCeaseToExist?.Invoke();
                }
                OnExistsSet(value);
                exists = value;
            } 
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

        public void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            string key = Type;
            if (dict.ContainsKey(key))
            {
                dict[key].Add(this);
                return;
            }
            dict.Add(key, new List<ISeparable> { this });
        }

        public void TakeDamage(double damage)
        {
            health.CurrentValue -= damage;
        }

        public void Cast(ISkill skill)
        {
            skill.OnCast(this);
        }

        public virtual void OnCollision(ICollisionDetector<IGameObject> other)
        {
            
        }
    }
}
