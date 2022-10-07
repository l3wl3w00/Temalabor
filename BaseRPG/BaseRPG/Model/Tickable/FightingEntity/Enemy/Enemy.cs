using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
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

        public Enemy(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy,Dictionary<string, IAttackFactory> attacks) 
            : base(maxHp, movementManager,movementStrategy,attacks)
        {
        }


        public override void OnTick()
        {
            //MovementStrategy.Move(MovementManager,1);
        }

    }
}
