using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collect
{
    [Interaction(typeof(Hero), typeof(Weapon), interactionType: InteractionType.Collection)]
    public abstract class HeroCollectsWeaponInteraction : ICollectInteraction
    {
        public void Collect()
        {
            Collectible.Owner = Collector;
            Collector.CollectItem(Collectible);
        }

        public abstract Hero Collector { get; }
        public abstract Weapon Collectible { get; }
    }
}
