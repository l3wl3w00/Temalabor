using BaseRPG.Controller;
using BaseRPG.Controller.Initialization;
using BaseRPG.Controller.Input;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.ReflectionStuff.Generation;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
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
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static readonly int IMAGE_SCALE = 4;
        private Game game;
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

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {

            GeneratorInitializer.GenerateAll();
            game = Game.Instance;
            game.PhysicsFactory = new PhysicsFactory2D();
            window = new MainWindow();
            window.ViewManager = new(game, new DefaultWorldNameImageMapper(), window.Canvas);
            controller = new Controller.Controller(window.ViewManager, new CollisionNotifier2D(),Game.Instance);
            window.Controller = controller;
            window.OnResourcesReady += StartGame;
            window.Activate();
            
        }
        private void StartGame(IImageProvider imageProvider) {
            
            controller.Initialize(
                new DefaultGameConfigurer(
                    new CenteredImageProvider(new ScalingImageProvider(IMAGE_SCALE, imageProvider)),
                    controller.CollisionNotifier),
                window);
            new MainLoop(controller).Start();
        }
    }
}
