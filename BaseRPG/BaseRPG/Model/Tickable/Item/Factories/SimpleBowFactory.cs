using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.ItemInterfaces;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Factories
{
    public class SimpleBowFactory : IItemFactory
    {
        public SimpleBowFactory()
        {
        }

        public static event Action<Item> OnItemCreatedStatic;
        public event Action<Item> OnItemCreated;

        public Item Create(World world)
        {
            var result = new Weapon.Weapon(
                    heavyAttackFactory: new AttackBuilder(new DamagingAttackStrategy(40)),
                    lightAttackFactory: new AttackBuilder(new DamagingAttackStrategy(20))
                        .AttackLifetimeOverStrategy(new DestroyAfterAllTargetsHitStrategy())
                        .NumberOfMaxTargets(1)
                        .LifeTime(1),
                    world);
            OnItemCreated?.Invoke(result);
            OnItemCreatedStatic?.Invoke(result);
            return result;
        }
    }
}
