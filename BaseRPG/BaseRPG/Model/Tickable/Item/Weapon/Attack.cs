using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack: IGameObject,ICollisionDetector<IGameObject>
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

        public event Action OnCeaseToExist;

        public void OnAttackHit(IAttackable attackable) {
            if (attacker == null) return;
            AttackabilityService attackabilityService = AttackabilityService.Builder.CreateByDefaultMapping();
            if (!attackabilityService.CanAttack(attacker, attackable)) return;
            attackStrategy.OnAttackHit(attacker,attackable);
        }

        public void OnCollision(ICollisionDetector<IGameObject> other)
        {
            if (other == attacker) return;
            if(hasTakenEffect) return;
            if (other is IAttackable) {
                OnAttackHit(other as IAttackable);
                hasTakenEffect = true;
            }
                
            //throw new NotImplementedException();
            
            
        }

        public void OnTick(double delta)
        {
            exists = false;
            //throw new NotImplementedException();
        }

        public void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
