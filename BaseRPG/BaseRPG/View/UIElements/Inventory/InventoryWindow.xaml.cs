using BaseRPG.Controller.UnitControl;
using BaseRPG.View.EntityView;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.Inventory
{
    public sealed partial class InventoryWindow : CustomWindow
    {
        public static readonly string WindowName = "inventory";
        private InventoryControl inventoryControl;
        private DrawableProvider drawableProvider;
        public InventoryWindow(Model.Attribute.Inventory inventory,DrawableProvider drawableProvider)
        {
            this.InitializeComponent();
            Inventory = inventory;
            equippedItemUI.InventoryControl = new InventoryControl(Inventory);
            equippedItemUI.EquippedWeaponView = drawableProvider.GetDrawable(Inventory.EquippedWeapon,"inventory");
        }

        public Model.Attribute.Inventory Inventory { get; }
    }
}
