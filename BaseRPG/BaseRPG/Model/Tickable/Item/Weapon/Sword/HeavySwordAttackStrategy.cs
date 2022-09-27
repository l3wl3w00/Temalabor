using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon.Sword
{
    public class HeavySwordAttackStrategy: IAttackStrategy
    {
        public void OnAttackHit(IAttackable attackable) {
            throw new NotImplementedException();
        }
    }
}
