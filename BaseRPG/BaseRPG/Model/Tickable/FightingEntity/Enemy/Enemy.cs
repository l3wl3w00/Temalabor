using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
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


        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Enemy;

        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public Enemy(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy,Dictionary<string, IAttackFactory> attacks) 
            : base(maxHp, movementManager,movementStrategy,attacks)
        {
        }
        public override void OnCollision(IGameObject gameObject) {
            
        }

        public override void OnTick()
        {
            //MovementStrategy.Move(MovementManager,1);
        }

    }
}
