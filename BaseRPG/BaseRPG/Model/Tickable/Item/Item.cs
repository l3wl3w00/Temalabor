using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item
{
    public abstract class Item : GameObject,ICollectible
    {
        public Item(World currentWorld) : base(currentWorld)
        {
        }

        public override bool Exists { get => true; }

        public override event Action OnCeaseToExist;

        //public void OnCollect(ICollector collector)
        //{
        //    //Do something
        //}



        public void OnCollision(GameObject gameObject)
        {
        }

        public override void Step(double delta)
        {
            //throw new NotImplementedException();
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public void OnCollectedByHero(Hero hero)
        {
            hero.CollectItem(this);
        }

        public abstract void EquippedBy(Inventory inventory);
    }
}
