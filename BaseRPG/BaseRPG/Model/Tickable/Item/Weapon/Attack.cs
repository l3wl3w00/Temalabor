using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Services;
using System;


namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack:ITickable
    {
        private IAttacking attacker;
        private PositionManager position;
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
    }
}
