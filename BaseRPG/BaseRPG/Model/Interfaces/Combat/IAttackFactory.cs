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
    public interface IAttackFactory
    {
        
        public Attack CreateAttack(IAttacking attacker,IPositionUnit position);
    }
}
