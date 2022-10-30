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
using BaseRPG.View.Windows;
using System.CodeDom.Compiler;
using Windows.UI;

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
        private InventoryWindow userControl = new InventoryWindow();
        
        public CanvasVirtualControl Canvas => canvas;

        public Controller.Controller Controller { get => controller; set => controller = value; }

        public event Action<object, PointerRoutedEventArgs> OnPointerPressed;
        public event Action<object, PointerRoutedEventArgs> OnPointerReleased;
        public event Action<object, PointerRoutedEventArgs> OnPointerMoved;
        public event Action<object, KeyRoutedEventArgs> OnKeyDown;
        public event Action<object, KeyRoutedEventArgs> OnKeyUp;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SizeChanged += (s, e) =>
            {
                canvas.Width = e.Size.Width;
                canvas.Height = e.Size.Height;
            };
            
            drawLoopHandler = new DeltaLoopHandler();
            //canvas2.Invalidate();
            //canvas3.Invalidate();
        }
        public async Task CreateResourceAsync(CanvasVirtualControl sender)
        {
            rawImageProvider = new();
            await rawImageProvider.LoadImages(sender);
        }
        public void canvas_CreateResources(CanvasVirtualControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
            sender.Invalidate();
            drawLoopHandler.FirsTickEvent += () => OnResourcesReady(rawImageProvider);
        }
        
        public void canvas_Draw(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        {
            
            foreach (var region in args.InvalidatedRegions)
            {
                using (var ds = sender.CreateDrawingSession(region))
                {
                    var delta = drawLoopHandler.Tick();
                    DrawingArgs drawingArgs = new DrawingArgs(sender, args, delta, controller.InputHandler.MousePosition, ds);
                    viewManager.Draw(drawingArgs);
                    
                }
            }
            canvas.Invalidate();
        }
        //public void canvas2_Draw(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        //{

        //    foreach (var region in args.InvalidatedRegions)
        //    {
        //        using (var ds = sender.CreateDrawingSession(region))
        //        {
        //            ds.FillRectangle(0, 0, (float)canvas2.Width, (float)canvas2.Height,Color.FromArgb(255,0,0,255));
        //        }
        //    }
        //    canvas2.Invalidate();
        //}
        private void PointerMoved(object sender, PointerRoutedEventArgs e)
        {

            OnPointerMoved?.Invoke(sender, e);
        }

        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
            OnPointerPressed?.Invoke(sender,e);
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            OnPointerReleased?.Invoke(sender, e);
        }

        private void KeyDown(object sender, KeyRoutedEventArgs e)
        {
            OnKeyDown?.Invoke(sender, e);
        }
        private void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            OnKeyUp?.Invoke(sender, e);
        }

    }
}
