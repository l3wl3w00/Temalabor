using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack:IGameObject
    {
        private IAttacking attacker;
        private IMovementManager movementManager;
        private IAttackStrategy attackStrategy;
        private bool exists;
        private bool hasTakenEffect = false;
        public Attack(IAttacking attacker, IPositionUnit initialPosition, IAttackStrategy attackStrategy)
        {
            this.attacker = attacker;
            this.movementManager = Game.Game.Instance.PhysicsFactory.CreateMovementManager(initialPosition);
            this.attackStrategy = attackStrategy;
        }

        public IPositionUnit Position { get => movementManager.Position; }

        public bool Exists => exists;

        public IMovementManager MovementManager { get { return movementManager; } }

        public void OnAttackHit(IAttackable attackable) {
            AttackabilityService attackabilityService = AttackabilityService.Builder.CreateByDefaultMapping();
            if (!attackabilityService.CanAttack(attacker, attackable)) return;
            attackStrategy.OnAttackHit(attacker,attackable);
        }

        public void OnCollision(IGameObject gameObject)
        {
            if(hasTakenEffect) return;
            if (gameObject is IAttackable)
                OnAttackHit(gameObject as IAttackable);
            //throw new NotImplementedException();
            
            hasTakenEffect = true;
        }

        public void OnTick()
        {
            exists = false;
            //throw new NotImplementedException();
        }

        public void Separate(Dictionary<string, List<IGameObject>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
