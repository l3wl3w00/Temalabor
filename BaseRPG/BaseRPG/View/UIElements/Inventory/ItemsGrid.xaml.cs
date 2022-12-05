using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.UIElements.CustomControl;
using BaseRPG.View.UIElements.DrawingArgsFactory;
using BaseRPG.View.UIElements.Initialization;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.Inventory
{
    public sealed partial class ItemsGrid : UserControl
    {
        private InventoryControl inventoryControl;
        private DrawableProvider drawableProvider;
        public List<ButtonWithCanvas> ItemPlaces
        {
            get 
            {
                List<ButtonWithCanvas> result = new();
                foreach (var item in grid.Children)
                {
                    if (item is ButtonWithCanvas)
                        result.Add(item as ButtonWithCanvas);
                }
                return result;
            }
        }


        public ItemsGrid()
        {
            this.InitializeComponent();
            
        }

        public void Update() {
            for (int i = 0; i < inventoryControl.Inventory.Capacity; i++)
            {
                var item = inventoryControl.GetItemAt(i);
                ItemPlaces[i].Drawable = drawableProvider.GetDrawable(item, "inventory");
            }
            ItemPlaces.ForEach(i => i.Canvas.Invalidate());
        }

        internal void Init(InventoryControl inventoryControl, DrawableProvider drawableProvider)
        {
            this.drawableProvider = drawableProvider;
            this.inventoryControl = inventoryControl;
            inventoryControl.OnCollected += (i,d)=>DispatcherQueue.TryEnqueue(Update);
            new GridFillStrategy().Fill(grid, CreateButtonWithCanvas, 4, inventoryControl.Inventory.Capacity);

            ItemPlaces.ForEach(i => i.DrawingArgsFactory = new ImageButtonDrawingArgsFactory(new(i.Canvas.Width, i.Canvas.Height)));
            for (int i = 0; i < ItemPlaces.Count; i++)
            {
                int index = i;
                ItemPlaces[i].ButtonClick += (s, a) => ItemLeftClicked(index);
                ItemPlaces[i].ButtonRightClick += (s, a) => ItemRightClicked(index);
            }
            inventoryControl.OnUnequipped += (i, d) => Update();
            Update();
        }

        private void ItemRightClicked(int index)
        {
            inventoryControl.DropItem(index);
            Update();
        }

        private ButtonWithCanvas CreateButtonWithCanvas(int index) {
            var buttonWithCanvas = new ButtonWithCanvas();
            buttonWithCanvas.Button.BorderThickness = new Thickness(1);

            return buttonWithCanvas;
        }
        private void ItemLeftClicked(int index) {
            inventoryControl.EquipItem(index);
            Update();
        }
    }
}
