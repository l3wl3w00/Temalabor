using BaseRPG.Model.Attribute;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public class InventoryControl
    {
        private Inventory inventory;
        public InventoryControl(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public Inventory Inventory { get { return inventory; } }
        public void EquipItem(Item item) {
            inventory.Equip(item);
            
        }
    }
}
