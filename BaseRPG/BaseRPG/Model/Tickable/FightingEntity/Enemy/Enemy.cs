using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Item.Weapon;
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
        
        private readonly Dictionary<string, IAttackFactory> attacks;
        
        protected override string Type { get { return "Enemy"; } }

        private InRangeDetector inRangeDetector;

        public event Action<IGameObject> AttackableInRange;

        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Enemy;

        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public Enemy(
            int maxHp, IMovementManager movementManager,
            IMovementStrategy movementStrategy,
            Dictionary<string, IAttackFactory> attacks,
            InRangeDetector inRangeDetector)
            : base(maxHp, movementManager, movementStrategy)
        {
            this.attacks = attacks;
            this.inRangeDetector = inRangeDetector;
            inRangeDetector.Exists = true;
            inRangeDetector.OnInRange += OnInRange;
            inRangeDetector.OnExitedRange += g=>StartMoving();
            
        }
        public override void OnCollision(IGameObject gameObject) {
            
        }
        public void OnInRange(IGameObject gameObject) {
            if (gameObject == this) return;
            if (gameObject is IAttackable) {
                var canAttack = AttackabilityService.Builder.CreateByDefaultMapping().CanAttack(this, gameObject as IAttackable);
                if (canAttack)
                {
                    StopMoving();
                    AttackableInRange?.Invoke(gameObject);
                }
            }
        }
        public override void OnTick(double delta)
        {
            base.OnTick(delta);
            
        }
        protected override void OnExistsSet(bool value) { 
            inRangeDetector.Exists = value;
        }

        public override IAttackFactory AttackFactory(string v)
        {
            return attacks[v];
        }
    }
}
