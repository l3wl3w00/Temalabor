using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.ItemInterfaces;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Factories
{
    public class SimpleSwordFactory : IItemFactory
    {
        public event Action<Item> OnItemCreated;

        public Item Create(World world)
        {
            var result =  new Weapon.Weapon(
                new AttackBuilder(new DamagingAttackStrategy(40)),
                new AttackBuilder(new DamagingAttackStrategy(40)), 
                world);
            OnItemCreated?.Invoke(result);
            return result;
        }
    }
}
