using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Factories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.ItemView;
using BaseRPG.View.WorldView;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    internal class DefaultInitializationStrategy : IInitializationStrategy
    {
        private IImageProvider imageProvider;

        public DefaultInitializationStrategy(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }

        public void Initialize(Controller controller, IPhysicsFactory physicsFactory)
        {
            
            FollowingCamera2D followingCamera = new FollowingCamera2D(new(0, 0), controller.ViewManager.Canvas.Size);
            controller.Game.CurrentWorldChanged += (name, world) => {

                controller.ViewManager.SetCurrentWorldView(name,world,imageProvider,followingCamera);
            };
            controller.Game.ChangeWorld("Empty");


            Hero hero = CreateHero(controller, physicsFactory);
            Enemy enemy = CreateEnemy(controller, hero,physicsFactory);
            controller.PlayerControl = new PlayerControl(hero);
            Weapon sword = CreateSword(controller, hero);
            controller.Game.Hero = hero;
            hero.Collect(sword);
            hero.Equip(sword);
            followingCamera.FollowedUnit = hero;
            
        }
        private Hero CreateHero(Controller controller, IPhysicsFactory physicsFactory) {
            Dictionary<string, IAttackFactory> attacks = new Dictionary<string, IAttackFactory>();
            attacks.Add("light", new LightSwordAttackFactory());

            var heroImage = @"Assets\image\characters\character1-outlined.png";
            Hero hero = new Hero(100, physicsFactory.CreateMovementManager(), attacks);

            UnitView heroView = 
                new UnitView(hero,
                    new DefaultImageRenderer(
                        imageProvider.GetByFilename(heroImage),
                        imageProvider.GetSizeByFilename(heroImage))
                    );
            controller.AddVisible(hero, heroView);
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
                            imageRenderer: swordImageRenderer,
                            distanceOffsetTowardsPointer: 100
                            )
                        ),
                    imageRenderer: swordImageRenderer
                    );
            controller.PlayerControl.EquippedItemView = equippedItemView;
            SetAttackCreation(controller,sword);
            controller.AddVisible(sword, equippedItemView);
            return sword;
        }
        private void SetAttackCreation(Controller controller,Weapon weapon) {
            weapon.LightAttackCreatedEvent +=
                (a) => controller.CreateAttackView(
                    a, weapon.Owner.Position,
                    @"Assets\image\attacks\sword-attack-effect.png",
                    imageProvider);
        }
        
        private Enemy CreateEnemy(Controller controller, Hero hero, IPhysicsFactory physicsFactory) {
            Enemy enemy = new Enemy(
                100, physicsFactory.CreateMovementManager(
                    physicsFactory.CreatePosition(100, 100)),
                new FollowingMovementStrategy(hero.MovementManager),new Dictionary<string, IAttackFactory>()
                ) ;
            var enemyImage = @"Assets\image\enemies\slime-outlined.png";

            controller.AddVisible(enemy,
                new UnitView(enemy,
                    new DefaultImageRenderer(
                        imageProvider.GetByFilename(enemyImage),
                        imageProvider.GetSizeByFilename(enemyImage))
                    )
                );
            controller.AddControl(new AutomaticUnitControl(enemy));

            return enemy;
        }
    }
}
