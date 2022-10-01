using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Services;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack:IGameObject
    {
        private IAttacking attacker;
        private MovementManager position;
        private IAttackStrategy attackStrategy;
        public void OnAttackHit(IAttackable attackable) {
            AttackabilityService attackabilityService = AttackabilityService.Builder.CreateByDefaultMapping();
            if (!attackabilityService.CanAttack(attacker, attackable)) return;
            attackStrategy.OnAttackHit(attacker,attackable);
            attackable.OnAttacked(attacker);
        }

        public void OnTick()
        {
            throw new NotImplementedException();
        }

        public void Separate(Dictionary<string, List<IGameObject>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
