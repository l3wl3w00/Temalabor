using BaseRPG.Controller.UnitControl;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.ItemView;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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
    public sealed partial class EquippedItemUI : UserControl
    {
        private InventoryControl inventoryControl;
        private IDrawable equippedWeaponView;
        public EquippedItemUI()
        {
            this.InitializeComponent();
        }

        public IDrawable EquippedWeaponView { get => equippedWeaponView; set => equippedWeaponView = value; }
        public InventoryControl InventoryControl { get => inventoryControl; set => inventoryControl = value; }
        public CanvasControl EquippedWeaponCanvas { get => equippedWeaponCanvas; }
        private void equippedWeaponCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            Size size = EquippedWeaponCanvas.Size;
            equippedWeaponView?.OnRender(new DrawingArgs(sender,1,new(size.Width/2,size.Height/2),new(0,0),args.DrawingSession));
            
        }

        private void equippedWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            equippedWeaponCanvas.Invalidate();
        }
    }
}
