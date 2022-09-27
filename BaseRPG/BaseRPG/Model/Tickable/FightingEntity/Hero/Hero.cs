using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Hero
{
    public class Hero : Unit, Interfaces.ICollector
    {
        private Model.Attribute.Inventory inventory;
        private Model.Attribute.ExperienceManager experienceManager;

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
