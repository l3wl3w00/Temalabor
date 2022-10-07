using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.Utility;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.View;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.WorldView;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;

namespace BaseRPG.Controller
{
    public class Controller
    {

        private PlayerControl playerControl;
        private List<AutomaticUnitControl> unitControls = new List<AutomaticUnitControl>();
        private InputHandler inputHandler;
        private ViewManager viewManager;

        public InputHandler InputHandler { get { return inputHandler; } }
        private bool running = true;
        public Game Game { get { return game; } }

        public ViewManager ViewManager { get => viewManager; }
        public bool Running { get => running; }
        public PlayerControl PlayerControl { set { playerControl = value; } get => playerControl; }

        private Game game;
        private GameObjectCollectionControl gameObjectCollectionControl = new();

        public Controller(Game game, ViewManager view)
        {
            this.game = game;
            this.viewManager = view;

        }
        public void MainLoop(CanvasControl canvas) {
            DeltaLoopHandler loopHandler = new();
            while (running) {
                double delta = loopHandler.Tick();
                Tick(delta);
            }
        }
        public void Tick(double delta) {
            gameObjectCollectionControl.AddQueued();
            inputHandler.OnTick();
            playerControl.OnTick(delta);
            unitControls.ForEach(u => u.OnTick(delta));
            game.CurrentWorld.OnTick();

        }
        public void Initialize(
            IInitializationStrategy initializationStrategy,
            IPhysicsFactory physicsFactory,
            MainWindow window) {

            inputHandler = new();

            initializationStrategy.Initialize(this, physicsFactory);


            inputHandler.Initialize(
                RawInputProcessedInputMapper.CreateDefault(),
                ProcessedInputActionMapper.CreateDefault(playerControl)
            );
            window.OnKeyDown += inputHandler.KeyDown;
            window.OnKeyUp += inputHandler.KeyUp;
            window.OnPointerPressed += inputHandler.MouseDown;
            window.OnPointerReleased += inputHandler.MouseUp;
            window.OnPointerMoved += inputHandler.MouseMoved;
        }

        public void CreateAttackView(Attack a, IPositionUnit ownerPosition, string attackImage, IImageProvider imageProvider) {
            DefaultImageRenderer attackImageRenderer = new DefaultImageRenderer(
                imageProvider.GetByFilename(attackImage),
                imageProvider.GetSizeByFilename(attackImage)
                );
            double[] values = ownerPosition.MovementTo(a.Position).Values;
            double initialRotation = Math.Atan2(values[1],values[0]);
            AddVisible(a, new AttackView(a, attackImageRenderer, initialRotation));

        }
        // The only way to add a game object that is visible
        public void AddVisible(IGameObject gameObject, IDrawable view) {
            AddVisibleToWorld(game.CurrentWorld, ViewManager.CurrentWorldView, gameObject, view);
        }
        public void AddVisibleToWorld(World world,WorldView worldView,IGameObject gameObject, IDrawable view)
        {
            gameObjectCollectionControl.QueueForAdd(world,worldView,gameObject, view);
        }
        public void AddControl(AutomaticUnitControl unitControl) { unitControls.Add(unitControl); }
    }
}
