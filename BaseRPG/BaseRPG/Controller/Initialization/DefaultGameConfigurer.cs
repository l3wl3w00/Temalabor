using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Effects.DamagingStun;
using BaseRPG.Model.Effects.Dash;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Effects.Invincibility;
using BaseRPG.Model.Game;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Factories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.Model.Worlds.Blocks;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.Factory;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.UIElements.ItemCollectionUI;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;


namespace BaseRPG.Controller.Initialization
{

    internal class DefaultGameConfigurer : IGameConfigurer
    {
        public static readonly double VERY_LARGE_NUMBER = 9999999999999999999D;
        private AnimationProvider animationProvider = AnimationProvider.CreateDefault();
        private IImageProvider imageProvider;
        private readonly CollisionNotifier2D collisionNotifier;
        private InventoryControl inventoryControl;
        private SpellControl spellControl;
        private Controller controller;
        private readonly DrawableProvider drawableProvider = new();

        public IImageProvider ImageProvider => imageProvider;

        public AnimationProvider AnimationProvider => animationProvider;

        public DrawableProvider DrawableProvider => drawableProvider;

        public InventoryControl InventoryControl { get => inventoryControl; set => inventoryControl = value; }

        public SpellControl SpellControl => spellControl;

        public ShopControl ShopControl { get; set; }

        public DefaultGameConfigurer(IImageProvider imageProvider, CollisionNotifier2D collisionNotifier)
        {
            this.imageProvider = imageProvider;
            this.collisionNotifier = collisionNotifier;
        }
        
        public void Configure(Controller controller, ViewManager viewManager, PositionObserver globalMousePositionObserver,MainWindow window)
        {
            this.controller = controller;
            FollowingCamera2D followingCamera = new FollowingCamera2D(new(0, 0), viewManager.Canvas.Size);
            Game.Instance.CurrentWorldChanged += (name, world) => {

                viewManager.SetCurrentWorldView(name,world,imageProvider,followingCamera);
            };
            Game.Instance.ChangeWorld("Empty");
            var block = new Block(new PositionUnit2D(200, 0), Game.Instance.CurrentWorld);
            var pos = PositionUnit2D.ToVector2D(block.Position);
            controller.AddView(new DrawingImage(@"Assets\image\blocks\normal-block-outlined.png", imageProvider, pos));
            controller.AddShape(new Polygon(block, new PhysicsFactory2D().CreateMovementManager(block.Position), Polygon.RectangleVertices(new(0, 0), 128, 128)));

            
            var shop = AddShop(150,200);
            SimpleSwordFactory simpleSwordFactory = new SimpleSwordFactory();
            simpleSwordFactory.OnItemCreated += item => CreateSwordViews(@"Assets\image\weapons\red-sword-outlined.png", controller, item as Weapon, null);
            shop.AddItem(simpleSwordFactory.Create(Game.Instance.CurrentWorld), 1);

            SimpleBowFactory.OnItemCreatedStatic += item => CreateBowViews(@"Assets\image\weapons\normal-bow\normal-bow-outlined.png", controller, item as Weapon, null, globalMousePositionObserver);
            shop.AddItem(new SimpleBowFactory().Create(Game.Instance.CurrentWorld),2);

            var shop2 = AddShop(0, 200);
            SimpleSwordFactory simpleSwordFactory2 = new SimpleSwordFactory();
            simpleSwordFactory2.OnItemCreated += item => CreateSwordViews(@"Assets\image\weapons\normal-sword-outlined.png", controller, item as Weapon, null);
            shop2.AddItem(simpleSwordFactory2.Create(Game.Instance.CurrentWorld), 1);

            Hero hero = CreateHero(controller,Game.Instance.CurrentWorld);
            CreateEnemy(controller, hero, 150, 300, @"Assets\image\enemies\zombie-outlined.png", animationProvider.Get("zombie-attack"), 0.2, 180);
            CreateEnemy(controller, hero, -200, 200, @"Assets\image\enemies\zombie-outlined.png", animationProvider.Get("zombie-attack"), 0.5, 50);
            CreateEnemy(controller, hero, -250, 0, @"Assets\image\enemies\slime-outlined.png", animationProvider.Get("slime-attack"), 0.1, 100);
            CreateEnemy(controller, hero, 150, -200, @"Assets\image\enemies\slime-outlined.png", animationProvider.Get("slime-attack"), 0.2, 90);
            controller.PlayerControl = new PlayerControl(hero,DrawableProvider);
            

            InventoryControl = new InventoryControl(hero.Inventory,drawableProvider,controller);
            spellControl = new SpellControl(hero.SkillManager,drawableProvider,controller.BoolCallbackQueue);

            //Weapon w = new SimpleBowFactory().Create(Game.Instance.CurrentWorld) as Weapon;
            //w.Owner = hero;
            //InventoryControl.CollectItem(w);

            
            //SimpleSwordFactory simpleSwordFactory2 = new SimpleSwordFactory();
            //simpleSwordFactory2.OnItemCreated += item => CreateSwordViews(@"Assets\image\weapons\normal-sword-outlined.png", controller, item as Weapon, hero);
            //InventoryControl.CollectItem(simpleSwordFactory2.Create(Game.Instance.CurrentWorld));
            
            //InventoryControl.EquipItem(1);
            Game.Instance.Hero = hero;
            followingCamera.FollowedUnit = hero;// Game.Instance.CurrentWorld.GameObjectContainer.All[1] as Unit;
        }

        private Shop AddShop(int x,int y) {
            var shop = new Shop(new PositionUnit2D(x, y), Game.Instance.CurrentWorld);
            shop.InteractionStarted += (h, s) => {
                ShopControl.ClickedOnShop(shop);
            };
            var shopPos = PositionUnit2D.ToVector2D(shop.Position);
            controller.AddView(new DrawingImage(@"Assets\image\blocks\shop-outlined.png", imageProvider, shopPos));
            controller.AddShape(new Polygon(shop, new PhysicsFactory2D().CreateMovementManager(shop.Position), Polygon.RectangleVertices(new(0, 0), 128, 128)));
            return shop;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="controller"></param>
        /// <param name="weaponViews"> This dictionary contains all the views that the function creates after being called </param>
        /// <returns></returns>

        private Dictionary<string, IDrawable> CreateSwordViews(string image, Controller controller, Weapon item, Hero hero) {
            var result = new WeaponViewBuilder(new(image, imageProvider), controller, item
                , w => new NormalSwordAnimationFactory(controller, w))
                .EquippedBy(hero)
                .LightAttack2DBuilder(
                    new Attack2DBuilder(@"Assets\image\attacks\sword-attack-effect.png")
                    .ImageProvider(imageProvider)
                    .PolygonShape(new List<Point2D> {
                            new(0,-50),
                            new(-100,30),
                            new(0,70),
                            new(100,30)
                        }
                    ))
                .LightAttackCreatedCallback(g => controller.AddVisible(g), 0.15)
                .CreateWeapon();
            drawableProvider.Connect(result["equipped"], item, "equipped");
            drawableProvider.Connect(result["inventory"], item, "inventory");
            return result;
        }
        private Dictionary<string, IDrawable> CreateBowViews(string image, Controller controller, Weapon item, Hero hero, IPositionProvider globalMousePositionObserver)
        {
            var result = new WeaponViewBuilder(new(image, imageProvider), controller, item as Weapon
                , w => new NormalBowAnimationFactory(animationProvider, imageProvider, controller, w, globalMousePositionObserver))
                .EquippedBy(hero)
                .LightAttack2DBuilder(
                    new Attack2DBuilder(@"Assets\image\attacks\arrow-outlined.png")
                    .ImageProvider(imageProvider)
                    .PolygonShape(Polygon.RectangleVertices(new(0, 0), 15, 35)
                    ))
                .LightAttackCreatedCallback(g => controller.AddVisible(g), 0)
                .CreateWeapon();
            drawableProvider.Connect(result["equipped"], item, "equipped");
            drawableProvider.Connect(result["inventory"], item, "inventory");
            return result;
        }
        private Hero CreateHero(Controller controller, World world) {

            var heroImage = @"Assets\image\characters\character1-outlined.png";
            var movementManager = Game.Instance.PhysicsFactory.CreateMovementManager();
            
            var time = 0.2;
            Hero hero = new Hero.HeroBuilder(100, movementManager, Game.Instance.CurrentWorld).Build() as Hero;
            hero.SkillManager =
                    new SkillManager.Builder()
                    .WithSkill(
                        new EffectCreatingSkill<DashEffectCreationParams>(
                            "dash",
                            new DashEffectFactory(time, 250,
                            (effect) =>
                                controller.AddView(
                                    new EffectView.Builder(effect, movementManager,
                                        ImageSequenceAnimation.WithTimeFrame(imageProvider, animationProvider.Get("dash-effect"), time))
                                        .DefaultTransformationAnimation(
                                            new FacingPointOnCallbackAnimation(-25 * App.IMAGE_SCALE,
                                                PositionObserver.CreateForLastMovement(movementManager, VERY_LARGE_NUMBER)
                                            )
                                        )
                                    .Build(), -100)
                                )
                            )
                        )
                    .WithSkill(
                        new EffectCreatingSkill<TargetedEffectParams>(
                            "invincibility",
                            new InvincibilityEffectFactory(5, (effect) =>
                            controller.AddView(
                                new EffectView.Builder(effect, movementManager,
                                    ImageSequenceAnimation.LoopingAnimation(imageProvider, animationProvider.Get("invincibility-effect"), 0.5))
                                    .DefaultTransformationAnimation(
                                        new FacingPointOnCallbackAnimation(0,
                                            PositionObserver.CreateForLastMovement(movementManager, VERY_LARGE_NUMBER)
                                            )
                                        )
                                    .Build(), 200)
                                )
                            )
                        )
                    .WithSkill(
                        new AttackCreatingSkill("meteor",
                        new AttackBuilder(
                            new DamagePerSecondAttackStrategy(100), 0.5, int.MaxValue)
                                .World(world)
                                .Attacker(hero)
                                .AttackabilityService(
                                    new AttackabilityService.Builder().AllowIf((attacker, attacked) => attacked == attacker).CreateByDefaultMapping()),
                            a =>
                                controller.AddVisibleInstantly(new Attack2DBuilder(@"Assets\image\effects\meteor\lava-ground-circle.png")
                                .ImageProvider(imageProvider)
                                .Attack(a)
                                .OwnerPosition(movementManager.Position)
                                .PolygonShape(Polygon.CircleVertices(new(0, 0), 300, 50))
                                .CreateAttack(0, false))
                        )
                    )
                    .WithSkill(new EffectCreatingSkill<TargetedEffectParams>("stun", new DamagingStunEffectFactory(hero, 5)))
                    .Create();
            var shape = Polygon.ForUnit(
                       hero,
                       Polygon.RectangleVertices(new(0, 0), 120, 120),
                       collisionNotifier
                       );
            UnitView heroView = 
                new UnitView.Builder(new DrawingImage(heroImage, imageProvider),hero,new ShapeView(shape,new PositionObserver(()=>PositionUnit2D.ToVector2D(hero.Position))))
                .IdleAnimation(ImageSequenceAnimation.SingleImage(imageProvider,heroImage))
                .WithFacingPointAnimation().Build();
            drawableProvider.Connect(heroView,hero);
            controller.AddShape(shape);
            controller.AddView(drawableProvider.GetDrawablesAsListOf(hero)[0], 200);
            return hero;
        }
        

        private void CreateEnemy(Controller controller, Hero hero, double x, double y,string idleImage,List<string> attackAnimation,double timeBetweenFrames, double speed) {
            Enemy enemy = new Enemy.EnemyBuilder(
                100, Game.Instance.PhysicsFactory.CreateMovementManager(
                    Game.Instance.PhysicsFactory.CreatePosition(x,y)),
                hero, Game.Instance.CurrentWorld).
                Attack("normal", new DamagingAttackStrategy(1))
                .Speed(speed)
                .Build() as Enemy;
            var enemyImage = idleImage;
            Polygon shape = Polygon.ForUnit(enemy,Polygon.CircleVertices(new(0,0),50), collisionNotifier);
            List<string>.Enumerator enumerator = attackAnimation.GetEnumerator();
            enumerator.MoveNext();
            UnitView enemyView = new UnitView.Builder(new DrawingImage(enemyImage,imageProvider), enemy, new ShapeView(shape, new PositionObserver(() => PositionUnit2D.ToVector2D(enemy.Position))))
                .IdleAnimation(ImageSequenceAnimation.SingleImage(imageProvider, enemyImage))
                .Animation(
                    "attack",
                    new ImageSequenceAnimation(
                        imageProvider,
                        enumerator,
                        a => controller.QueueAction( () => { 
                                var attack = enemy.Attack("normal");
                                    if (attack != null)
                                        controller.AddVisibleInstantly(
                                        new Attack2DBuilder(@"Assets\image\attacks\enemy-attack.png")
                                            .ImageProvider(imageProvider)
                                            .Attack(attack)
                                            .OwnerPosition(enemy.Position)
                                            .PolygonShape(Polygon.RectangleVertices(new(0, 0), 120, 80))
                                            .CreateAttack()
                                        );
                                }
                            ), timeBetweenFrames
                        )
                )
                //.IdleTransformationAnimation(new EmptyAnimation(new(0,0)))
                .WithFacingPointAnimation()
                .Build();

            var enemyConfigurer = new EnemyConfigurer(enemy)
                .WithUnitView(enemyView)
                .Range(70);
            drawableProvider.Connect(enemyView, enemy);
            controller.AddView(drawableProvider.GetDrawablesAsListOf(enemy)[0], 100);
            controller.AddShape(shape);
            controller.AddShape(enemyConfigurer.FullInRangeDetectorShape);
        }

    }
}
