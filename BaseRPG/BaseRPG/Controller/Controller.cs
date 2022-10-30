using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.Utility;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.WorldView;
using Microsoft.Graphics.Canvas.UI.Xaml;

using System;
using System.Collections.Generic;


namespace BaseRPG.Controller
{
    public class Controller
    {

        private PlayerControl playerControl;
        private InputHandler inputHandler;
        private ViewManager viewManager;
        private CollisionNotifier2D collisionNotifier;
        private bool running = true;

        public InputHandler InputHandler { get { return inputHandler; } }
        public bool Running { get => running; }
        public PlayerControl PlayerControl { set { playerControl = value; } get => playerControl; }
        private GameObjectCollectionControl gameObjectCollectionControl = new();

        public Controller(ViewManager view, CollisionNotifier2D collisionNotifier)
        {
            this.viewManager = view;
            this.collisionNotifier = collisionNotifier;
        }
        public void MainLoop() {
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
            Game.Instance.OnTick(delta);
            collisionNotifier.CheckCollisions();

        }
        public void Initialize(
            IGameConfigurer gameConfigurer,
            MainWindow window) {

            inputHandler = new();

            gameConfigurer.Configure(this, viewManager);


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
        public void CreateAttackView(Attack attack, IPositionUnit ownerPosition, string attackImage, IImageProvider imageProvider,IShape2D shape) {
            
            DefaultImageRenderer attackImageRenderer = new DefaultImageRenderer(
                attackImage,imageProvider
                );
            double[] values = ownerPosition.MovementTo(attack.Position).Values;
            double initialRotation = Math.Atan2(values[1],values[0]);
            shape.Rotate(initialRotation - Math.PI / 2);
            FullGameObject2D fullAttackObject = 
                new FullGameObject2D(
                    attack, 
                    shape, 
                    new AttackView(attack, attackImageRenderer, initialRotation));
            AddVisible(fullAttackObject);

        }
        // The only way to add a game object that is visible
        public void AddVisible(FullGameObject2D fullGameObject) {
            
            AddVisibleToWorld(Game.Instance.CurrentWorld, viewManager.CurrentWorldView,collisionNotifier, fullGameObject);
        }
        public void AddVisibleToWorld(World world,WorldView worldView,CollisionNotifier2D collisionNotifier, FullGameObject2D fullGameObject)
        {
            
            gameObjectCollectionControl.QueueForAdd(world,worldView, collisionNotifier, fullGameObject);
        }
    }
}
