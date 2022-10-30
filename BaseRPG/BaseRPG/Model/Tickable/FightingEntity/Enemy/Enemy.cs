using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Enemy
{
    public class Enemy : Unit
    {

        

        protected override string Type { get { return "Enemy"; } }
        private readonly Dictionary<string, AttackBuilder> attacks;
        private InRangeDetector inRangeDetector;
        private Unit target;
        public event Action<IAttackable> AttackableInRange;

        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Enemy;

        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public Unit Target => target;

        public InRangeDetector InRangeDetector => inRangeDetector; 

        public Enemy(
            int maxHp, IMovementManager movementManager,
            Unit target,
            Dictionary<string, AttackBuilder> attacks,
            InRangeDetector inRangeDetector)
            : base(maxHp, movementManager, new FollowingMovementStrategy(target.MovementManager))
        {
            this.target = target;
            this.attacks = attacks;
            this.inRangeDetector = inRangeDetector;
            inRangeDetector.Exists = true;
            target.OnCeaseToExist += OnTargetDead;
            //inRangeDetector.OnInRange += OnInRange;
            inRangeDetector.OnExitedRange += g => StartMoving();

        }
        public void OnTargetDead(){
            this.target = null;
            DefaultMovementStrategy = new EmptyMovementStrategy();
            MovementStrategy = DefaultMovementStrategy;
        }
        public override void OnCollision(ICollisionDetector<IGameObject> gameObject) 
        {
            
        }
        public void OnInRange(ICollisionDetector<IGameObject> gameObject) {
            if (gameObject == this) return;
            
        }
        public override void OnTick(double delta)
        {
            base.OnTick(delta);
            if (inRangeDetector.IsInRange(target)) {
                if (target is IAttackable)
                {
                    var canAttack = AttackabilityService.Builder.CreateByDefaultMapping().CanAttack(this, target as IAttackable);
                    if (canAttack)
                    {
                        StopMoving();
                        AttackableInRange?.Invoke(target as IAttackable);
                    }
                }
            }
        }
        protected override void OnExistsSet(bool value) { 
            inRangeDetector.Exists = value;
        }

        public override AttackBuilder AttackFactory(string v)
        {
            return attacks[v];
        }
        public class Builder {
            private readonly Dictionary<string, AttackBuilder> attacks = new();
            private InRangeDetector inRangeDetector;
            private Unit target;
            private int maxHp;
            private IMovementManager movementManager;
            public Builder(
                int maxHp, IMovementManager movementManager,
                Unit target,
                InRangeDetector inRangeDetector)
            {
                this.maxHp = maxHp;
                this.movementManager = movementManager;
                this.target = target;
                this.inRangeDetector = inRangeDetector;
            }
            public Builder Attack(string name, AttackBuilder builder) { 
                attacks.Add(name, builder);
                return this;
            }
            public Builder Attack(string name, IAttackStrategy strategy)
            {
                attacks.Add(name, new AttackBuilder(strategy));
                return this;
            }
            public Enemy Build()
            {
                Enemy enemy = new Enemy(maxHp,movementManager,target,attacks,inRangeDetector);
                inRangeDetector.Exists = true;
                target.OnCeaseToExist += enemy.OnTargetDead;
                //inRangeDetector.OnInRange += OnInRange;
                inRangeDetector.OnExitedRange += g => enemy.StartMoving();
                return enemy;
            }


        }

    }
}
