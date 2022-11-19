using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Effects.DamagingStun;
using BaseRPG.Model.Effects.Dash;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Effects.Invincibility;
using BaseRPG.Model.Game;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.ItemView;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;


namespace BaseRPG.Controller.Initialization
{

    internal class DefaultGameConfigurer : IGameConfigurer
    {
        public static readonly double VERY_LARGE_NUMBER = 9999999999999999999D;
        private AnimationProvider animationProvider = AnimationProvider.CreateDefault();
        private IImageProvider imageProvider;
        private readonly DrawableProvider drawableProvider = new();

        public IImageProvider ImageProvider => imageProvider;

        public AnimationProvider AnimationProvider => animationProvider;

        public DrawableProvider DrawableProvider => drawableProvider;

        public DefaultGameConfigurer(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }

        public void Configure(Controller controller, ViewManager viewManager)
        {
            FollowingCamera2D followingCamera = new FollowingCamera2D(new(0, 0), viewManager.Canvas.Size);
            Game.Instance.CurrentWorldChanged += (name, world) => {

                viewManager.SetCurrentWorldView(name,world,imageProvider,followingCamera);
            };
            Game.Instance.ChangeWorld("Empty");

            Hero hero = CreateHero(controller,Game.Instance.CurrentWorld);
            //CreateEnemy(controller, hero, 200, 200, @"Assets\image\enemies\zombie-outlined.png", animationProvider.Get("zombie-attack"), 0.2, 180);
            //CreateEnemy(controller, hero, -200, 200, @"Assets\image\enemies\zombie-outlined.png", animationProvider.Get("zombie-attack"), 0.5, 50);
            CreateEnemy(controller, hero, -200, -200, @"Assets\image\enemies\slime-outlined.png", animationProvider.Get("slime-attack"), 0.1, 100);
            //CreateEnemy(controller, hero, 200, -200, @"Assets\image\enemies\slime-outlined.png", animationProvider.Get("slime-attack"), 0.2, 90);
            controller.PlayerControl = new PlayerControl(hero);
            Dictionary<string, IDrawable> weaponViews = new();
            Weapon weapon = CreateWeapon(hero, controller,weaponViews);

            drawableProvider.Connect(weaponViews["equipped"], weapon, "equipped");
            drawableProvider.Connect(weaponViews["inventory"], weapon, "inventory");
            controller.AddView(drawableProvider.GetDrawablesOf(weapon)["equipped"],100);
            Game.Instance.Hero = hero;
            followingCamera.FollowedUnit = hero;// Game.Instance.CurrentWorld.GameObjectContainer.All[1] as Unit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="controller"></param>
        /// <param name="weaponViews"> This dictionary contains all the views that the function creates after being called </param>
        /// <returns></returns>
        private Weapon CreateWeapon(Hero hero, Controller controller, Dictionary<string,IDrawable> weaponViews) {

            return new WeaponViewBuilder(imageProvider,controller, Game.Instance.CurrentWorld)
                .Image(@"Assets\image\weapons\normal-sword-outlined.png")
                .EquippedBy(hero, controller.PlayerControl)
                .LightAttackBuilder( 
                    new Attack2DBuilder(@"Assets\image\attacks\sword-attack-effect.png")
                    .ImageProvider(imageProvider)
                    .PolygonShape(new List<Point2D> {
                            new(0,-50),
                            new(-100,30),
                            new(0,70),
                            new(100,30)
                        }
                    ))
                .LightAttackCreatedCallback(g => 
                controller.AddVisible(g)
                )
                .CreateSword(weaponViews);
        }
        private Hero CreateHero(Controller controller, World world) {

            var heroImage = @"Assets\image\characters\character1-outlined.png";
            var movementManager = Game.Instance.PhysicsFactory.CreateMovementManager();
            
            var time = 0.2;
            Hero hero = new Hero.HeroBuilder(100, movementManager, Game.Instance.CurrentWorld)
                .WithSelfTargetEffectSkill(new DashEffectFactory(time, 250, world),
                (effect) =>
                        controller.AddView(
                            new EffectView.Builder(effect, movementManager,
                                ImageSequenceAnimation.WithTimeFrame(imageProvider, animationProvider.Get("dash-effect"), time))
                                .DefaultTransformationAnimation(
                                    new FacingPointOnCallbackAnimation(-100,
                                        PositionObserver.CreateForLastMovement(movementManager, VERY_LARGE_NUMBER)
                                    )
                                )
                            .Build()
                            , -100
                        )
                    )
                .WithSelfTargetEffectSkill(new InvincibilityEffectFactory(5, world)
                    , (effect) =>
                        controller.AddView(
                            new EffectView.Builder(effect, movementManager,
                                ImageSequenceAnimation.LoopingAnimation(imageProvider, animationProvider.Get("invincibility-effect"), 0.5))
                            .DefaultTransformationAnimation(
                                new FacingPointOnCallbackAnimation(0,
                                    PositionObserver.CreateForLastMovement(movementManager, VERY_LARGE_NUMBER)
                                    )
                                )
                            .Build()
                            , 200
                            )
                        )
                .WithAttackCreatingSkill(
                        new AttackBuilder(new DamagePerSecondAttackStrategy(100), world, 0.5, int.MaxValue)
                        .AttackabilityService(
                            new AttackabilityService.Builder().AllowIf((attacker, attacked) => attacked == attacker).CreateByDefaultMapping()
                            )
                        ,
                        a =>
                            controller.AddVisibleInstantly(new Attack2DBuilder(@"Assets\image\effects\meteor\lava-ground-circle.png")
                            .ImageProvider(imageProvider)
                            .Attack(a)
                            .OwnerPosition(movementManager.Position)
                            .PolygonShape(Polygon.CircleVertices(new(0, 0), 300, 50))
                            .CreateAttack(false))
                )
                .WithSkill(unit=>new EffectCreatingSkill<TargetedEffectParams>(unit, new DamagingStunEffectFactory(unit, 5)))
                .Build() as Hero;
            var shape = new Polygon(
                       hero,
                       hero.MovementManager,
                       Polygon.RectangleVertices(new(0, 0), 120, 120)
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
            Polygon shape = Polygon.Circle(enemy,enemy.MovementManager,new(0, 0), 50);
            List<string>.Enumerator enumerator = attackAnimation.GetEnumerator();
            enumerator.MoveNext();
            UnitView enemyView = new UnitView.Builder(new DrawingImage(enemyImage,imageProvider), enemy, new ShapeView(shape, new PositionObserver(() => PositionUnit2D.ToVector2D(enemy.Position))))
                .IdleAnimation(ImageSequenceAnimation.SingleImage(imageProvider, enemyImage))
                .Animation(
                    "attack",
                    new ImageSequenceAnimation(
                        imageProvider,
                        enumerator,
                        a => {
                            
                            controller.QueueAction( () => { 
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
                            );
                        }, timeBetweenFrames
                        )
                )
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
