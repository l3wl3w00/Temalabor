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
    public class Attack : GameObject, ICollisionDetector<GameObject>
    {
        private IAttacking attacker;
        private IMovementManager movementManager;
        private IAttackStrategy attackStrategy;
        private readonly AttackabilityService attackabilityService;
        private double lifeTime;
        private readonly DefaultInt numberOfMaxTargets = new(0);

        public Attack(
            IAttacking attacker,
            IPositionUnit initialPosition,
            IAttackStrategy attackStrategy,
            World world,
            AttackabilityService attackabilityService,
            double lifeTime,
            int numberOfMaxTargets) : base(world)
        {
            this.attacker = attacker;
            movementManager = Game.Game.Instance.PhysicsFactory.CreateMovementManager(initialPosition);
            this.attackStrategy = attackStrategy;
            this.attackabilityService = attackabilityService;
            this.lifeTime = lifeTime;
            this.numberOfMaxTargets = new(numberOfMaxTargets);
        }

        public IPositionUnit Position { get => movementManager.Position; }

        public override bool Exists { get => 
                lifeTime>0;
        }

        public IMovementManager MovementManager { get { return movementManager; } }

        public override event Action OnCeaseToExist;

        public bool OnAttackHit(IAttackable attackable, double delta)
        {
            if (attacker == null) return false;
            if (!attackabilityService.CanAttack(attacker, attackable)) return false;
            attackStrategy.OnAttackHit(attacker, attackable, delta);
            return true;
        }

        public void OnCollision(ICollisionDetector<GameObject> other, double delta)
        {

            if (numberOfMaxTargets.CurrentValue <= 0) return;
            if (other is IAttackable)
            {
                var success = OnAttackHit(other as IAttackable, delta);
                if(success)
                    numberOfMaxTargets.CurrentValue -= 1;
            }

            //throw new NotImplementedException();


        }


        public override void Step(double delta)
        {
            lifeTime -= delta;
            numberOfMaxTargets.Reset();
            //throw new NotImplementedException();
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
