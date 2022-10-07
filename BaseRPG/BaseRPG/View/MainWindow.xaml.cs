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
using BaseRPG.View.Animation;
using System.Diagnostics;
using BaseRPG.Controller.Utility;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public event Action<RawImageProvider> OnResourcesReady;
        private Controller.Controller controller;
        private ViewManager viewManager;
        public ViewManager ViewManager { get { return viewManager; } set { viewManager = value; } }
        private RawImageProvider rawImageProvider;
        private DeltaLoopHandler drawLoopHandler;

        public CanvasControl Canvas => canvas;

        public Controller.Controller Controller { get => controller; set => controller = value; }

        public event Action<object, PointerRoutedEventArgs> OnPointerPressed;
        public event Action<object, PointerRoutedEventArgs> OnPointerReleased;
        public event Action<object, PointerRoutedEventArgs> OnPointerMoved;
        public event Action<object, KeyRoutedEventArgs> OnKeyDown;
        public event Action<object, KeyRoutedEventArgs> OnKeyUp;

        public MainWindow()
        {
            this.InitializeComponent();
            drawLoopHandler = new DeltaLoopHandler();
        }
        public async Task CreateResourceAsync(CanvasControl sender)
        {
            rawImageProvider = new();
            await rawImageProvider.LoadImages(sender);
        }
        public void canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
            sender.Invalidate();
            drawLoopHandler.FirsTickEvent += () => OnResourcesReady(rawImageProvider);
        }
        
        public void canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var delta = drawLoopHandler.Tick();
            DrawingArgs drawingArgs = new DrawingArgs(sender,args,delta);
            viewManager.Draw(drawingArgs);
            canvas.Invalidate();
        }
        private void PointerMoved(object sender, PointerRoutedEventArgs e)
        {

            OnPointerMoved?.Invoke(sender, e);
            //controller.InputHandler.MouseDown(e.GetCurrentPoint((Grid)sender));
        }

        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
            OnPointerPressed?.Invoke(sender,e);
            //controller.InputHandler.MouseDown(e.GetCurrentPoint((Grid)sender));
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            OnPointerReleased?.Invoke(sender, e);
            //controller.InputHandler.MouseUp(e.GetCurrentPoint((Grid)sender));
        }

        private void KeyDown(object sender, KeyRoutedEventArgs e)
        {
            OnKeyDown?.Invoke(sender, e);
            //controller.InputHandler.KeyDown(e);
        }
        private void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            OnKeyUp?.Invoke(sender, e);
            //controller.InputHandler.KeyUp(e);
        }

    }
}
