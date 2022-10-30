using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Factories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Physics.TwoDimensional;
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
using BaseRPG.View.WorldView;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaseRPG.Controller.Initialization
{
    
    internal class DefaultGameConfigurer : IGameConfigurer
    {

        private static List<string> enemyAttackAnimation = new List<string>{
            
            @"Assets\image\enemies\attack-animation\slime-attack-0-outlined.png",
            @"Assets\image\enemies\attack-animation\slime-attack-1-outlined.png",
            @"Assets\image\enemies\attack-animation\slime-attack-2-outlined.png",
            @"Assets\image\enemies\attack-animation\slime-attack-3-outlined.png",
        };
        private IImageProvider imageProvider;

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

            Hero hero = CreateHero(controller);
            CreateEnemy(controller, hero, 200,200);
            CreateEnemy(controller, hero, -200, 200);
            CreateEnemy(controller, hero, -200, -200);
            CreateEnemy(controller, hero, 200, -200);
            controller.PlayerControl = new PlayerControl(hero);

            controller.AddVisible(CreateWeapon(hero,controller));
            
            Game.Instance.Hero = hero;
            followingCamera.FollowedUnit = hero;
            
        }

        private FullGameObject2D CreateWeapon(Hero hero, Controller controller) {

            return new Weapon2DBuilder(imageProvider)
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
                .LightAttackCreatedCallback(g => controller.AddVisible(g))
                .CreateSword();
        }
        private Hero CreateHero(Controller controller) {

            var heroImage = @"Assets\image\characters\character1-outlined.png";
            Hero hero = new Hero(100, Game.Instance.PhysicsFactory.CreateMovementManager());
            UnitView heroView = 
                new UnitView.Builder(imageProvider,heroImage,hero)
                .IdleAnimation(ImageSequenceAnimation.SingleImage(imageProvider,heroImage))
                .WithFacingPointAnimation().Build();
            var shape = new Polygon(
                       hero,
                       hero.MovementManager,
                       Polygon.Rectangle(new(0, 0), 120, 120)
                       );
            controller.AddVisible(new(hero, shape, heroView));
            return hero;
        }
        

        private void CreateEnemy(Controller controller, Hero hero, double x, double y) {
            Enemy enemy = new Enemy.Builder(
                100, Game.Instance.PhysicsFactory.CreateMovementManager(
                    Game.Instance.PhysicsFactory.CreatePosition(x,y)),
                hero, new InRangeDetector()).
                Attack("normal", new DamagingAttackStrategy(1))
                .Build();
            
            var enemyImage = @"Assets\image\enemies\slime-outlined.png";

            List<string>.Enumerator enumerator = enemyAttackAnimation.GetEnumerator();
            enumerator.MoveNext();
            UnitView unitView = new UnitView.Builder(imageProvider, enemyImage, enemy)
                .IdleAnimation(ImageSequenceAnimation.SingleImage(imageProvider, enemyImage))
                .Animation(
                    "attack", 
                    new ImageSequenceAnimation(
                        imageProvider,
                        enumerator,
                        a => controller.AddVisible(
                            new Attack2DBuilder(@"Assets\image\attacks\enemy-attack.png")
                            .ImageProvider(imageProvider)
                            .Attack(
                                enemy.AttackFactory("normal").Attacker(enemy).CreateTargeted(hero)
                                ).OwnerPosition(enemy.Position)
                                .PolygonShape(Polygon.Rectangle(new(0, 0), 120, 80))
                                .CreateAttack()
                            )
                        )
                )
                .WithFacingPointAnimation()
                .Build();

            var enemyBuilder = new Enemy2DBuilder(enemy,
                    Polygon.Circle(
                        enemy,
                        enemy.MovementManager,
                        new(0, 0), 50)
                    )
                .WithUnitView(unitView)
                .Range(70);
            controller.AddVisible(enemyBuilder.Create());
            controller.AddVisible(enemyBuilder.FullInRangeDetectorObject);

        }
    }
}
