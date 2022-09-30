using BasicRPG.Model.Attribute;
using BasicRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : ITickable, IAttackable
    {
        private Health health;
        private PositionManager position;
        private Stat damage;
        public abstract void OnTick();

        public abstract void Attack();

        public void OnAttacked(IAttacking attacker) { }

        public int CalculateDamage() {
            return damage.Value;
        }

    }
}
