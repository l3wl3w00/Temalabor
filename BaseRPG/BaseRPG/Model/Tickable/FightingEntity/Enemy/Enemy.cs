using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
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
        private IAttackFactory attackFactory;

        public Enemy(int maxHp, Vector2D initialPosition) : base(maxHp, initialPosition)
        {
        }

        public override void Attack()
        {
            attackFactory.CreateAttack(Position);
        }

        public override void OnTick()
        {
            throw new NotImplementedException();
        }

        
    }
}
