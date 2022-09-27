using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack:ITickable
    {
        private IAttacking attacker;
        private PositionManager position;
        private IAttackStrategy attackStrategy;
        public void OnAttackHit(IAttackable attackable) {
            AttackabilityService attackabilityService = new AttackabilityService();
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
