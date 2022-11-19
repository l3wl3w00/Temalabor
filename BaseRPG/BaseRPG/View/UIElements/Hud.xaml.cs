using BaseRPG.Model.Data;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Interfaces;
using BaseRPG.View.UIElements.Inventory;
using Microsoft.Graphics.Canvas.UI.Xaml;
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
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements
{
    public sealed partial class Hud : UserControl
    {
        private IImageProvider imageProvider;
        private Hero hero;
        public CanvasControl SettingsButtonCanvas => settingsButtonCanvas;
        public event Action<string> WindowButtonClicked;
        public Hud()
        {
            this.InitializeComponent();
        }

        public IImageProvider ImageProvider { get => imageProvider; set => imageProvider = value; }
        public CanvasControl InventoryButtonCanvas => inventoryButtonCanvas;
        public HealthView HealthView {
            set 
            {
                healthViewControl.HealthView = value; 
            }
        }

        public Hero Hero {
            get {
                return hero;
            }
            internal set {
                var heroWasNull = hero == null;
                hero = value;
                HealthView = new HealthView(hero.Health,250, Color.FromArgb(255, 0, 150, 255),4);

                if (heroWasNull)
                {
                    levelText.Text = hero.Level.ToString();
                    hero.OnLevelUpCallback(
                        (newLevel)=> DispatcherQueue.TryEnqueue(() => levelText.Text = newLevel.ToString())
                    );
                    experienceControl.Hero = hero;
                }
            }
        }

        public void settingsButton_Draw(CanvasControl sender, CanvasDrawEventArgs args) {
            if (ImageProvider == null) return;
            var image = ImageProvider.GetByFilename(@"Assets\image\icons\settings-outlined.png");
            args.DrawingSession.DrawImage(image);
        }

        public void inventoryButton_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (ImageProvider == null) return;
            var image = ImageProvider.GetByFilename(@"Assets\image\icons\inventory-outlined.png");
            args.DrawingSession.DrawImage(image);
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowButtonClicked?.Invoke(SettingsWindow.WindowName);
        }

        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            WindowButtonClicked?.Invoke(InventoryWindow.WindowName);
        }

        private void levelText_LayoutUpdated(object sender, object e)
        {

        }
    }
}
