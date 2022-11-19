using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.ItemView;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    public class WeaponViewBuilder
    {
        private string image;
        private Hero owner;
        private Weapon weapon;
        private PlayerControl playerControl;

        private AttackBuilder lightAttackFactory;
        private AttackBuilder heavyAttackFactory;

        private Attack2DBuilder lightAttackBuilder;
        private Attack2DBuilder heavyAttackBuilder;

        private IImageProvider imageProvider;
        private readonly Controller controller;
        private readonly World world;

        public WeaponViewBuilder(IImageProvider imageProvider, Controller controller, World world)
        {
            this.imageProvider = imageProvider;
            this.controller = controller;
            this.world = world;
            lightAttackFactory = new AttackBuilder(new DamagingAttackStrategy(20), world);
            heavyAttackFactory = new AttackBuilder(new DamagingAttackStrategy(40), world);
        }

        private Func<AttackBuilder, SwordSwingAnimation> SwordSwingAnimationCreation(Weapon weapon,Controller controller)
        {
            return (factory) =>
            {
                SwordSwingAnimation swordSwingAnimation =
                    new SwordSwingAnimation(Angle.FromDegrees(120), 0.3);
                swordSwingAnimation.OnAnimationAlmostEnding +=
                    a => controller.QueueAction( ()=> factory.CreateAttack(
                            new PhysicsFactory2D().CreatePosition(
                                Vector2D.FromPolar(100, swordSwingAnimation.StartingAngle)
                                + new Vector2D(weapon.Owner.Position.Values[0], weapon.Owner.Position.Values[1])
                                
                        ))
                    );
                return swordSwingAnimation;
            };

        }
        public WeaponViewBuilder EquippedBy(Hero hero, PlayerControl playerControl) {
            owner = hero;
            this.playerControl = playerControl;
            return this;
        }
        public WeaponViewBuilder LightAttackBuilder(Attack2DBuilder attackBuilder) {
            lightAttackBuilder = attackBuilder;
            return this;
        }
        public WeaponViewBuilder LightAttackCreatedCallback(Action<ShapeViewPair> onAttackCreated) {
            lightAttackFactory.CreatedEvent += 
                (a) => 
                onAttackCreated(
                    lightAttackBuilder
                    .Attack(a)
                    .OwnerPosition(weapon.Owner.Position)
                    .CreateAttack()
                    );

            return this;
        }
        public WeaponViewBuilder HeavyAttackBuilder(Attack2DBuilder attackBuilder)
        {
            heavyAttackBuilder = attackBuilder;
            return this;
        }
        public WeaponViewBuilder Image(string image) {
            this.image = image;
            return this;
        }
        public Weapon CreateSword(Dictionary<string, IDrawable> weaponViews)
        {
            
            weapon = new Weapon(heavyAttackFactory, lightAttackFactory,  world, owner);
            owner.Collect(weapon); 
            owner.Equip(weapon);

            EquippedItemView equippedItemView =
                new EquippedItemView(
                    item: weapon,
                    owner: owner,
                    animator:
                    new CustomAnimator(
                        new FacingMouseAnimation(distanceOffsetTowardsPointer: 100),
                        ImageSequenceAnimation.SingleImage(imageProvider,image)
                        ),
                    SwordSwingAnimationCreation(weapon,controller)
                    );
            playerControl.EquippedItemView = equippedItemView;
            weaponViews.Add("equipped", equippedItemView);

            InventoryItemView inventoryItemView = new InventoryItemView(weapon, new DrawingImage(@"Assets\image\weapons\normal-sword-outlined.png", imageProvider));
            weaponViews.Add("inventory", inventoryItemView);

            return weapon;
        }
    }
}
