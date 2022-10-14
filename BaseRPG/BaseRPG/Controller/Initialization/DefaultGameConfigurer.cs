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
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
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
        
        private IImageProvider imageProvider;

        public DefaultGameConfigurer(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }

        public void Configure(Controller controller)
        {
            
            FollowingCamera2D followingCamera = new FollowingCamera2D(new(0, 0), controller.ViewManager.Canvas.Size);
            Game.Instance.CurrentWorldChanged += (name, world) => {

                controller.ViewManager.SetCurrentWorldView(name,world,imageProvider,followingCamera);
            };
            Game.Instance.ChangeWorld("Empty");


            Hero hero = CreateHero(controller);
            Enemy enemy = CreateEnemy(controller, hero);
            controller.PlayerControl = new PlayerControl(hero);
            Weapon sword = CreateSword(controller, hero);
            Game.Instance.Hero = hero;
            hero.Collect(sword);
            hero.Equip(sword);
            followingCamera.FollowedUnit = hero;
            
        }
        private Hero CreateHero(Controller controller) {
            Dictionary<string, IAttackFactory> attacks = new Dictionary<string, IAttackFactory>();
            attacks.Add("light", new LightSwordAttackFactory());

            var heroImage = @"Assets\image\characters\character1-outlined.png";
            Hero hero = new Hero(100, Game.Instance.PhysicsFactory.CreateMovementManager(), attacks);

            UnitView heroView = 
                new UnitView(hero,
                    new DefaultImageRenderer(
                        imageProvider.GetByFilename(heroImage),
                        imageProvider.GetSizeByFilename(heroImage))
                    );
            controller.AddVisible(new(hero,null, heroView));
            return hero;
        }
       
        private Weapon CreateSword(Controller controller, Hero hero) {
            Weapon sword = new Weapon(new HeavySwordAttackFactory(), new LightSwordAttackFactory(),hero);
            var swordImage = @"Assets\image\weapons\normal-sword-outlined.png";
            DefaultImageRenderer swordImageRenderer = new DefaultImageRenderer(
                           imageProvider.GetByFilename(swordImage),
                           imageProvider.GetSizeByFilename(swordImage)
                        );
            EquippedItemView equippedItemView = 
                new EquippedItemView(
                    item: sword,
                    owner: hero,
                    animator: 
                    new DefaultAnimator(
                        defaultStrategy: 
                        new FacingPointAnimationStrategy(
                            facingPositionTracker: controller.InputHandler.MousePositionTracker,
                            distanceOffsetTowardsPointer: 100
                            ),
                        imageProvider,swordImage
                        ),
                    imageProvider: imageProvider,
                    mousePositionTracker: controller.InputHandler.MousePositionTracker
                    );
            controller.PlayerControl.EquippedItemView = equippedItemView;
            SetAttackCreation(controller,sword);
            controller.AddVisible(new(sword, null, equippedItemView));
            return sword;
        }
        private void SetAttackCreation(Controller controller,Weapon weapon) {
            weapon.LightAttackCreatedEvent +=
                (a) => 
                controller.CreateAttackView(
                    a, 
                    weapon.Owner.Position,
                    @"Assets\image\attacks\sword-attack-effect.png",
                    imageProvider,
                    new Polygon(
                        a,
                        a.MovementManager,
                        new List<Point2D> {
                            new(0,-50),
                            new(-100,30),
                            new(0,70),
                            new(100,30)
                            
                        })
                    );
        }
        
        private Enemy CreateEnemy(Controller controller, Hero hero) {
            Enemy enemy = new Enemy(
                100, Game.Instance.PhysicsFactory.CreateMovementManager(
                    Game.Instance.PhysicsFactory.CreatePosition(100, 100)),
                new FollowingMovementStrategy(hero.MovementManager),new Dictionary<string, IAttackFactory>()
                ) ;
            var enemyImage = @"Assets\image\enemies\slime-outlined.png";
            var shape = new Polygon(
                        enemy,
                        enemy.MovementManager,
                        new List<Point2D> {
                            new(-50,-50),
                            new(-50,50),
                            new(50,50),
                            new(50,-50)
                        });

            enemy.MovementManager.Moved += () =>
            {
                Angle angle = new Vector2D(enemy.LastMovement.Values[0], enemy.LastMovement.Values[1]).SignedAngleTo(new(0, 1),true);
                shape.SetRotation(angle.Radians);
            };
            var fullGameObject =
                new FullGameObject2D(
                    enemy,
                    shape,
                    new UnitView(enemy,
                        new DefaultImageRenderer(
                            imageProvider.GetByFilename(enemyImage),
                            imageProvider.GetSizeByFilename(enemyImage))
                        )
                    ) ;

            controller.AddVisible(fullGameObject);
            controller.AddControl(new AutomaticUnitControl(enemy));

            return enemy;
        }
    }
}
