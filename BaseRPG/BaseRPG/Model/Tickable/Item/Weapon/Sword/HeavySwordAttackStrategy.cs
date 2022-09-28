using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon.Sword
{
    public class HeavySwordAttackStrategy: IAttackStrategy
    {
        public void OnAttackHit(IAttacking attacker, IAttackable attacked)
        {
            throw new NotImplementedException();
        }
    }
}
