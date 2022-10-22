using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Item.Weapon;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Combat
{
    public abstract class IAttackFactory
    {
        public Action<Attack> CreatedEvent;
        private IAttacking attacker;
        public IAttacking Attacker {
            set 
            { 
                attacker = value;
            }
        }
        public abstract Attack CreateAttack(IAttacking attacker, IPositionUnit position);
        public Attack CreateAttack(IPositionUnit position) {
            if (attacker == null) throw new RequiredParameterNull("attacker was null");
            return CreateAttack(attacker, position);
        }
    }
}
