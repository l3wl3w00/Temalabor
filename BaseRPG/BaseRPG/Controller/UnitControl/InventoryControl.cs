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
        private readonly DrawableProvider drawableProvider;
        private readonly Controller controller;
        public event Action<Item, DrawableProvider> OnUnequipped;
        public event Action<Item, DrawableProvider> OnEquipped;
        public event Action<Item, DrawableProvider> OnCollected;
        public InventoryControl(Inventory inventory, DrawableProvider drawableProvider, Controller controller)
        {
            this.inventory = inventory;
            this.drawableProvider = drawableProvider;
            this.controller = controller;
            inventory.ItemCollected += item => OnCollected?.Invoke(item, DrawableProvider);
        }

        public Item GetItemAt(int index) {
            if (index >= inventory.UnequippedItems.Count)
                return null;
            return inventory.UnequippedItems[index];
        }
        public Inventory Inventory { get { return inventory; } }

        public Item EquippedWeapon => inventory.EquippedWeapon;
        public Item EquippedArmor => inventory.EquippedArmor;
        public Item EquippedShoe => inventory.EquippedShoe;

        public DrawableProvider DrawableProvider => drawableProvider;

        public void EquipItem(Item item) {
            UnEquipItem(inventory.EquippedWeapon);
            inventory.Equip(item);
            controller.AddView(drawableProvider.GetDrawable(inventory.EquippedWeapon, "equipped"), 100);
            OnEquipped?.Invoke(item,drawableProvider);
        }
        public void EquipItem(int index)
        {
            if (inventory.UnequippedItems.Count <= index) {
                return;
            }
            EquipItem(inventory.UnequippedItems[index]);
            //inventory.Equip(index);
            //controller.AddView(drawableProvider.GetDrawable(inventory.EquippedWeapon, "equipped"), 100);
            //OnEquipped?.Invoke(inventory.EquippedWeapon, drawableProvider);
        }
        public void UnEquipItem(Item item)
        {
            inventory.UnEquip(item);
            var views = drawableProvider.GetDrawablesOf(item);
            controller.RemoveView(drawableProvider.GetDrawable(item, "equipped"));
            OnUnequipped?.Invoke(item,drawableProvider);
        }
        public void CollectItem(Item item)
        {
            inventory.Collect(item);
        }

        internal void DropItem(int index)
        {
            if (inventory.UnequippedItems.Count <= index)
            {
                return;
            }
            inventory.Drop(inventory.UnequippedItems[index]);
        }
    }
}
