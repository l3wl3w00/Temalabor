using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Worlds;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Attacks
{
    public class AttackBuilder
    {
        public event Action<Attack> CreatedEvent;
        public AttackBuilder(IAttackStrategy attackStrategy, World world, double lifeTime = 0, int numberOfMaxTargets = 1)
        {
            this.attackStrategy = attackStrategy;
            this.world = world;
            this.lifeTime = lifeTime;
            this.numberOfMaxTargets = numberOfMaxTargets;
            attackabilityService = new AttackabilityService.Builder().CreateByDefaultMapping();
        }
        private IAttacking attacker;
        private IAttackStrategy attackStrategy;
        private readonly World world;
        private IPositionUnit initialPosition;
        private double lifeTime;
        private int numberOfMaxTargets;
        private  AttackabilityService attackabilityService;

        public AttackBuilder Attacker(IAttacking value)
        {
            attacker = value;
            return this;
        }
        public AttackBuilder AttackabilityService(AttackabilityService attackabilityService) {
            this.attackabilityService = attackabilityService;
            return this;
        }
        private Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            Attack attack = new Attack(attacker, position, attackStrategy, world,attackabilityService,lifeTime,numberOfMaxTargets);
            CreatedEvent?.Invoke(attack);
            return attack;

        }
        public Attack CreateAttack(IPositionUnit position)
        {
            if (attacker == null) throw new RequiredParameterNull("attacker was null");
            return CreateAttack(attacker, position);
        }
        public Attack CreateInFrontOf(Unit unit, double distanceFromUnit)
        {
            attacker = unit;
            var temp = unit.Position.Copy();
            temp.MoveBy(unit.LastMovement.WithLength(distanceFromUnit));
            initialPosition = temp;
            return CreateAttack(attacker, initialPosition);
        }
        public Attack CreateTargeted(Unit target)
        {
            return CreateAttack(attacker, target.Position);
        }
    }
}
