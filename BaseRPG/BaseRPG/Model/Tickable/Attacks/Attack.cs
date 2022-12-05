using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.Attacks
{
    public class Attack : GameObject, ICollisionDetector
    {
        private IAttacking attacker;
        private IMovementManager movementManager;
        private IAttackStrategy attackStrategy;
        private readonly IMovementStrategy movementStrategy;
        private readonly IAttackLifetimeOverStrategy attackLifetimeOverStrategy;
        private readonly AttackabilityService attackabilityService;
        private double lifeTime;
        private readonly DefaultInt numberOfMaxTargetsInOneStep = new(0);
        private int numberOfMaxTargets = 0;

        public Attack(
            IAttacking attacker,
            IPositionUnit initialPosition,
            IAttackStrategy attackStrategy,
            IMovementStrategy movementStrategy,
            IAttackLifetimeOverStrategy attackLifetimeOverStrategy,
            World world,
            AttackabilityService attackabilityService,
            double lifeTime,
            int numberOfMaxTargetsInOneStep,
            int numberOfMaxTargets) : base(world)
        {
            this.attacker = attacker;
            movementManager = Game.Game.Instance.PhysicsFactory.CreateMovementManager(initialPosition);
            this.attackStrategy = attackStrategy;
            this.movementStrategy = movementStrategy;
            this.attackLifetimeOverStrategy = attackLifetimeOverStrategy;
            this.attackabilityService = attackabilityService;
            this.lifeTime = lifeTime;
            this.numberOfMaxTargetsInOneStep = new(numberOfMaxTargetsInOneStep);
            this.numberOfMaxTargets = numberOfMaxTargets;
        }

        public IPositionUnit Position { get => movementManager.Position; }

        public override bool Exists { get =>
                !attackLifetimeOverStrategy.IsOver(this);
        }

        public IMovementManager MovementManager { get { return movementManager; } }

        public double LifeTime { get => lifeTime; }
        public int NumberOfRemainingMaxTargets { get => numberOfMaxTargets; }

        public override event Action OnCeaseToExist;

        public bool OnAttackHit(IAttackable attackable, double delta)
        {
            if (attacker == null) return false;
            if (!attackabilityService.CanAttack(attacker, attackable)) return false;
            attackStrategy.OnAttackHit(attacker, attackable, delta);
            return true;
        }

        public void OnCollision(ICollisionDetector other, double delta)
        {

            if (numberOfMaxTargetsInOneStep.CurrentValue <= 0) return;
            if (numberOfMaxTargets <= 0) return;
            
            if (other is IAttackable)
            {
                var success = OnAttackHit(other as IAttackable, delta);
                if (success) {
                    -- numberOfMaxTargetsInOneStep.CurrentValue;
                    -- numberOfMaxTargets;
                }
            }
        }


        public override void Step(double delta)
        {
            lifeTime -= delta;
            numberOfMaxTargetsInOneStep.Reset();
            IMovementUnit movementUnit = movementStrategy.CalculateNextMovement(movementManager, 1)?.Scaled(delta);
            movementManager.Move(movementUnit);
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public bool CanCollide(ICollisionDetector other)
        {
            return true;
        }
    }
}
