using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollectible
    {
        void OnCollectedByHero(Hero hero);
    }
}
