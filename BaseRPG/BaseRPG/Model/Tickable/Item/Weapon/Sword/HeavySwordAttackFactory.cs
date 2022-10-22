using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon.Sword
{
    public class HeavySwordAttackFactory : IAttackFactory
    {
        public override Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            throw new NotImplementedException();
        }
    }
}
