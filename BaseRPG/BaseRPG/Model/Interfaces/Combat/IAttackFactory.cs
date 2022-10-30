using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.Item.Weapon;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Combat
{
    public class AttackBuilder
    {
        public event Action<Attack> CreatedEvent;
        public AttackBuilder(IAttackStrategy attackStrategy)
        {
            this.attackStrategy = attackStrategy;
        }
        private IAttacking attacker;
        private IAttackStrategy attackStrategy;
        private IPositionUnit initialPosition;
        public AttackBuilder Attacker(IAttacking value) { 
            attacker = value;
            return this;
        }
        public Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            Attack attack = new Attack(attacker, position, attackStrategy);
            CreatedEvent?.Invoke(attack);
            return attack;

        }
        public Attack CreateAttack(IPositionUnit position) {
            if (attacker == null) throw new RequiredParameterNull("attacker was null");
            return CreateAttack(attacker, position);
        }
        public Attack CreateInFrontOf(Unit unit,double distanceFromUnit)
        {
            attacker = unit;
            var temp = unit.Position.Copy();
            temp.MoveBy(unit.LastMovement.WithLength(distanceFromUnit));
            initialPosition = temp;
            return CreateAttack(attacker, initialPosition);
        }
        public Attack CreateTargeted(Unit target) {
            return CreateAttack(attacker, target.Position);
        }
    }
}
