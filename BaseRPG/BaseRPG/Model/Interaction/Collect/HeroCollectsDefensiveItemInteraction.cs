using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.DefensiveItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collect
{
    [Interaction(typeof(Hero), typeof(DefensiveItem), interactionType: InteractionType.Collection)]
    public abstract class HeroCollectsDefensiveItemInteraction : ICollectInteraction
    {
        public void Collect()
        {
            Collector.CollectItem(Collectible);
        }

        public abstract Hero Collector { get; }
        public abstract DefensiveItem Collectible { get; }
    }
}
