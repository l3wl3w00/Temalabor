using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
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
        
        private IAttacking attacker;
        private IAttackStrategy attackStrategy;
        private World world;
        private IPositionUnit initialPosition;
        private double lifeTime;
        private int numberOfMaxTargetsInOneStep;
        private int numberOfMaxTargets = int.MaxValue;
        private  AttackabilityService attackabilityService;
        private IMovementStrategy movementStrategy = new EmptyMovementStrategy();
        private IAttackLifetimeOverStrategy attackLifetimeOverStrategy = new DestroyAfterAllTargetsHitStrategy();

        public event Action<Attack> CreatedEvent;
        public AttackBuilder(IAttackStrategy attackStrategy, double lifeTime = 0, int numberOfMaxTargetsInOneStep = 1)
        {
            this.attackStrategy = attackStrategy;
            this.lifeTime = lifeTime;
            this.numberOfMaxTargetsInOneStep = numberOfMaxTargetsInOneStep;
            attackabilityService = new AttackabilityService.Builder().CreateByDefaultMapping();
        }
        public AttackBuilder World(World world)
        {
            this.world = world;
            return this;
        }
        public AttackBuilder NumberOfMaxTargets(int value)
        {
            numberOfMaxTargets = value;
            return this;
        }
        public AttackBuilder Attacker(IAttacking value)
        {
            attacker = value;
            return this;
        }
        public AttackBuilder LifeTime(double lifeTime) {
            this.lifeTime = lifeTime;
            return this;
        }
        public AttackBuilder MovementStrategy(IMovementStrategy value)
        {
            movementStrategy = value;
            return this;
        }
        public AttackBuilder AttackLifetimeOverStrategy(IAttackLifetimeOverStrategy value)
        {
            attackLifetimeOverStrategy = value;
            return this;
        }
        public AttackBuilder AttackLifetimeOverStrategy(Func<Attack,bool> value)
        {
            attackLifetimeOverStrategy = new DelegateLifetimeOverStrategy(value);
            return this;
        }
        public AttackBuilder AttackabilityService(AttackabilityService attackabilityService) {
            this.attackabilityService = attackabilityService;
            return this;
        }

        private Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            Attack attack = new Attack(
                attacker, position, attackStrategy,movementStrategy, attackLifetimeOverStrategy,
                world,attackabilityService,lifeTime,
                numberOfMaxTargetsInOneStep,numberOfMaxTargets);
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
