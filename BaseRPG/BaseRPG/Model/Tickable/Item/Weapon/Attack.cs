using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Attack:ITickable
    {
        private PositionManager position;
        private IAttackStrategy attackStrategy;
        public void OnAttackHit(IAttackable attackable) {
            attackStrategy.OnAttackHit();
        }

        public void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
