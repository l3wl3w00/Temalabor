using Microsoft.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;

using Microsoft.UI.Xaml.Input;
using System;
using BaseRPG.View;
using System.Timers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Foundation;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Xaml.Controls;
using BaseRPG.View.Image;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly Controller.Controller controller;
        private ViewManager viewManager;
        private RawImageProvider rawImageProvider = new RawImageProvider();
        private bool hasDrawn = false;
        public CanvasControl Canvas => canvas;
        public MainWindow(Controller.Controller controller)
        {
            this.InitializeComponent();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (a, b) => canvas.Invalidate();
            timer.Interval = 10;
            timer.Start();


            this.controller = controller;
            
        }
        public async Task CreateResourceAsync(CanvasControl sender)
        {
            await rawImageProvider.LoadImages(sender);
        }
        public void canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
            sender.Invalidate();
            
        }

        private void OnResourcesInitialized() {
            viewManager = new ViewManager(controller.Game, new ScalingImageProvider(4, rawImageProvider));
        }
        
        public void canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (!hasDrawn) {
                OnResourcesInitialized();
                hasDrawn = true;
            }
            viewManager.Draw(sender, args);
        }

        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            controller.InputHandler.MouseDown(e.GetCurrentPoint((Grid)sender));
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            controller.InputHandler.MouseUp(e.GetCurrentPoint((Grid)sender));
        }

        private void KeyDown(object sender, KeyRoutedEventArgs e)
        {
            controller.InputHandler.KeyDown(e);
        }
        private void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            controller.InputHandler.KeyUp(e);
        }

    }
}
