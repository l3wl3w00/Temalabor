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
    public class Inventory
    {
        private List<Item> items;
        private DefensiveItem equippedArmor;
        private DefensiveItem equippedShoe;
        private Weapon equippedWeapon;
        public void Equip(Weapon weapon) {
            throw new NotImplementedException();
        }
        public void EquipArmor(DefensiveItem armor){
            throw new NotImplementedException();
        }
        public void EquipShoe(DefensiveItem shoe){
            throw new NotImplementedException();
        }
        public void Drop(Item item) {
            throw new NotImplementedException();
        }
    }
}
