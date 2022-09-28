using BaseRPG.Model.Interfaces.Collecting;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Hero
{
    public class Hero : Unit, ICollector
    {
        protected override string Type { get { return "Hero"; } }
        private Model.Attribute.Inventory inventory;
        private Model.Attribute.ExperienceManager experienceManager;

        public Hero(int maxHp, Vector2D initialPosition) : base(maxHp, initialPosition)
        {
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public void OnCollect(ICollectible collectible)
        {
            throw new NotImplementedException();
        }

        public override void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
