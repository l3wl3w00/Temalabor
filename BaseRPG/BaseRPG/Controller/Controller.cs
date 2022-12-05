using BaseRPG.Controller.Initialization;
using BaseRPG.Controller.Input;
using BaseRPG.Controller.Input.InputActions;
using BaseRPG.Controller.Input.InputActions.Attack;
using BaseRPG.Controller.Input.InputActions.Effect;
using BaseRPG.Controller.Input.InputActions.Movement;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.Utility;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Utility;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using BaseRPG.Model.Worlds.Blocks;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.UIElements;
using BaseRPG.View.UIElements.Inventory;
using BaseRPG.View.UIElements.Spell;
using BaseRPG.View.WorldView;
using Microsoft.Graphics.Canvas.UI.Xaml;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace BaseRPG.Controller
{
    public class Controller:ICanQueueAction,ICanQueueFunc<bool>
    {
        #region private fields
        private PlayerControl playerControl;
        private InputHandler inputHandler;
        private ViewManager viewManager;
        private CollisionNotifier2D collisionNotifier;
        private readonly Game game;
        private bool running = true;
        private CallbackQueue callbackQueue = new();
        private BoolCallbackQueue boolCallbackQueue = new();
        private IImageProvider imageProvider;
        #endregion

        #region properties
        public Game Game => game;
        public InputHandler InputHandler { get { return inputHandler; } }
        public bool Running { get => running; }
        public PlayerControl PlayerControl { set { playerControl = value; } get => playerControl; }
        public DrawableProvider DrawableProvider { get; internal set; }
        public IImageProvider ImageProvider => imageProvider;

        public BoolCallbackQueue BoolCallbackQueue { get => boolCallbackQueue; }
        public CollisionNotifier2D CollisionNotifier => collisionNotifier;
        #endregion

        public Controller(ViewManager view, CollisionNotifier2D collisionNotifier, Game game)
        {
            this.viewManager = view;
            this.collisionNotifier = collisionNotifier;
            this.game = game;
            game.CollisionNotifier = collisionNotifier;
        }
        public void MainLoop(int msBetweenTicks = 0) {
            
            lock (game) {
                DeltaLoopHandler loopHandler = new();
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += (a,b) => Console.WriteLine("Tick fps: "+loopHandler.Fps);
                timer.Start();
                while (running)
                {
                    
                    double delta = loopHandler.Tick();
                    Tick(delta);
                    Thread.Sleep(msBetweenTicks);

                    
                }
            }
            
        }

        public void QueueAction(Action action) {
            callbackQueue.QueueAction(action);
        }
        public void Tick(double delta) {
            boolCallbackQueue.Tick();
            callbackQueue.ExecuteAll();
            inputHandler.OnTick();
            game.OnTick(delta);
            playerControl.OnTick(delta);
        }
        public void Initialize(
            IGameConfigurer gameConfigurer,
            MainWindow window) {

            inputHandler = new();
            PositionObserver globalMousePositionObserver = new PositionObserver(() => inputHandler.MousePosition + viewManager.CurrentCamera.MiddlePosition);
            gameConfigurer.Configure(this, viewManager, globalMousePositionObserver);
            DrawableProvider = gameConfigurer.DrawableProvider;
            
            imageProvider = gameConfigurer.ImageProvider;
            BindingHandler binding = BindingHandler.CreateAndLoad(@"Assets\config\input-binding.json");
            collisionNotifier.KeepTrackOf(globalMousePositionObserver);
            
            window.WindowControl = new DefaultWindowinitializer(binding, Game.Instance.Hero.Inventory,gameConfigurer.InventoryControl, gameConfigurer.SpellControl).Initialize(window);
            inputHandler.Initialize(
                RawInputProcessedInputMapper.FromBinding(binding),
                new ProcessedInputActionMapper.Builder()
                .AddMapping("move-forward", new MovementInputAction(MoveDirection.Forward, playerControl))
                .AddMapping("move-left", new MovementInputAction(MoveDirection.Left, playerControl))
                .AddMapping("move-right", new MovementInputAction(MoveDirection.Right, playerControl))
                .AddMapping("move-backward", new MovementInputAction(MoveDirection.Backward, playerControl))
                .AddMapping("light-attack", new LightAttackInputAction(playerControl))
                .AddMapping("skill-1", new DashSkillOnPressInputAction(playerControl.ControlledUnit, globalMousePositionObserver,0))
                .AddMapping("skill-2", new InvincibilitySkillOnPressInputAction(playerControl.ControlledUnit, 1))
                .AddMapping("skill-3", new MeteorCreatingSkillOnReleaseInputAction(
                    playerControl.ControlledUnit,
                    globalMousePositionObserver,
                    window.Controller,
                    imageProvider,
                    gameConfigurer.AnimationProvider
                    ))
                .AddMapping("skill-4",new DamagingStunSkillOnPressInputAction(playerControl.ControlledUnit,collisionNotifier))
                .AddMapping("settings-window",new CustomInputAcion(actionOnPressed: () =>  window.WindowControl.OpenOrClose(SettingsWindow.WindowName)))
                .AddMapping("inventory-window",new CustomInputAcion(actionOnPressed: () =>  window.WindowControl.OpenOrClose(InventoryWindow.WindowName)))
                .AddMapping("spells-window",new CustomInputAcion(actionOnPressed: () =>  window.WindowControl.OpenOrClose(SpellsWindow.WindowName)))
                .Create()
            );
            
            viewManager.GlobalMousePositionProvider = globalMousePositionObserver;
            
            window.OnKeyDown += inputHandler.KeyDown;
            window.OnKeyUp += inputHandler.KeyUp;
            window.OnPointerPressed += inputHandler.MouseDown;
            window.OnPointerReleased += inputHandler.MouseUp;
            window.OnPointerMoved += inputHandler.MouseMoved;
        }

        public void AddView(IDrawable drawable, int layer = 0) {
            viewManager.CurrentWorldView.AddView(drawable, layer);
        }

        public void AddShape(IShape2D shape)
        {
            
            if (System.Diagnostics.Debugger.IsAttached)
                viewManager.CurrentWorldView.AddView(
                    new ShapeView(shape, new PositionObserver(() => 
                    PositionUnit2D.ToVector2D(shape.MovementManager.Position)
                    )),
                    int.MaxValue);
            collisionNotifier.AddToObservedShapes(shape);
        }
        public void AddVisible(ShapeViewPair fullGameObject, int layer = 0) {
            QueueAction(
                () => 
                AddVisibleInstantly(fullGameObject, layer));
        }
        public void AddVisibleInstantly(ShapeViewPair fullGameObject, int layer = 0)
        {
            if (fullGameObject.View != null)
                AddView(fullGameObject.View, layer);
            if (fullGameObject.Shape != null)
                AddShape(fullGameObject.Shape);
        }

        internal void RemoveView(IDrawable drawable)
        {
            viewManager.CurrentWorldView.RemoveView(drawable);
        }

        public void QueueWithResult(Func<bool> func, Action<bool> onResult)
        {
            throw new NotImplementedException();
        }
    }
}
