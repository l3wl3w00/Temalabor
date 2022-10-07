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
        private IPositionUnit position;
        private IAttackStrategy attackStrategy;

        public Attack(IAttacking attacker, IPositionUnit position, IAttackStrategy attackStrategy)
        {
            this.attacker = attacker;
            this.position = position;
            this.attackStrategy = attackStrategy;
        }

        public IPositionUnit Position { get => position; }

        public bool Exists => true;

        public void OnAttackHit(IAttackable attackable) {
            AttackabilityService attackabilityService = AttackabilityService.Builder.CreateByDefaultMapping();
            if (!attackabilityService.CanAttack(attacker, attackable)) return;
            attackStrategy.OnAttackHit(attacker,attackable);
        }

        public void OnTick()
        {
            //throw new NotImplementedException();
        }

        public void Separate(Dictionary<string, List<IGameObject>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
