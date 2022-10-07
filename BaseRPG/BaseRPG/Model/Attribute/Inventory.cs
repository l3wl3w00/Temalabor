using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.DefensiveItem;
using BaseRPG.Model.Tickable.Item.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class Inventory: ICollector<Item>
    {
        private List<Item> items = new();
        public event Action<Weapon> WeaponEquipped;
        public event Action<DefensiveItem> DefensiveItemEquipped;
        private DefensiveItem equippedArmor;
        private DefensiveItem equippedShoe;
        private Weapon equippedWeapon;

        public List<Item> Items => items;

        public DefensiveItem EquippedArmor { get => equippedArmor; set { 
                equippedArmor = value;
                DefensiveItemEquipped?.Invoke(equippedArmor);
            } }
        public DefensiveItem EquippedShoe { get => equippedShoe; set { 
                equippedShoe = value;
                DefensiveItemEquipped?.Invoke(equippedShoe);
            } }
        public Weapon EquippedWeapon { get => equippedWeapon; set {
                equippedWeapon = value;
                WeaponEquipped?.Invoke(equippedWeapon);
            } }

        public void Drop(Item item) {
            throw new NotImplementedException();
        }

        public void Collect(Item collectible)
        {
            collectible.OnCollect(this);
            items.Add(collectible);
        }
    }
}
