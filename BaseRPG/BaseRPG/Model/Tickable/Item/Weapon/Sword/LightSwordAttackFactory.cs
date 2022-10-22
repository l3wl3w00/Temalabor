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
    public class LightSwordAttackFactory : IAttackFactory
    {
        public override Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            Attack attack = new Attack(attacker, position, new DamagingAttackStrategy());
            CreatedEvent?.Invoke(attack);
            return attack;
        }

    }
}
