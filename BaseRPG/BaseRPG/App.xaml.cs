using BaseRPG.Controller;
using BaseRPG.Controller.Initialization;
using BaseRPG.Controller.Input;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.WorldView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Game game;
        private IPhysicsFactory physicsFactory;
        Controller.Controller controller;
        private MainWindow window;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            AllocConsole();
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        
        private void wireDependencies(ViewManager viewManager) {
            
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            game = new Game();
            window = new MainWindow();
            window.ViewManager = new(game, new DefaultWorldNameImageMapper(), window.Canvas);
            controller = new(game, window.ViewManager);
            window.Controller = controller;

            window.OnResourcesReady += StartGame;
            window.Activate();
            
        }
        private void StartGame(IImageProvider imageProvider) {
            controller.Initialize(
                new DefaultInitializationStrategy(new ScalingImageProvider(4, imageProvider)),
                new PhysicsFactory2D(),
                window);
            StartLogic();
        }
        private void StartLogic() {
            
            var t1 = new Thread(o => controller.MainLoop(window.Canvas));
            t1.IsBackground = true;
            t1.Start();
        }
    }
}
