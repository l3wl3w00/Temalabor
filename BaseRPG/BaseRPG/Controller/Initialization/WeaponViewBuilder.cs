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
using BaseRPG.View.Animation.Factory;
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
        private DrawingImage image;

        private Attack2DBuilder lightAttack2DBuilder;
        private Attack2DBuilder heavyAttack2DBuilder;

        private readonly Controller controller;
        private readonly Weapon weapon;
        private readonly Func<Weapon, IAttackAnimationFactory> factoryCreation;

        public WeaponViewBuilder(DrawingImage image, Controller controller, Weapon weapon, Func<Weapon,IAttackAnimationFactory> factoryCreation)
        {
            this.image = image;
            this.controller = controller;
            this.weapon = weapon;
            this.factoryCreation = factoryCreation;
        }

        public WeaponViewBuilder EquippedBy(Hero hero) {
            weapon.Owner = hero;
            return this;
        }
        public WeaponViewBuilder LightAttack2DBuilder(Attack2DBuilder attackBuilder) {
            lightAttack2DBuilder = attackBuilder;
            return this;
        }
        public WeaponViewBuilder LightAttackCreatedCallback(Action<ShapeViewPair> onAttackCreated,double secondsAfterDestroyed) {
            weapon.LightAttackBuilder.CreatedEvent += 
                (a) => 
                onAttackCreated(
                    lightAttack2DBuilder
                    .Attack(a)
                    .OwnerPosition(weapon.Owner.Position)
                    .CreateAttack(secondsAfterDestroyed)
                    );

            return this;
        }
        
        public Dictionary<string, IDrawable> CreateWeapon()
        {
            Dictionary<string, IDrawable> weaponViews = new();
            EquippedWeaponView equippedItemView =
                new EquippedWeaponView(
                    weapon: weapon,
                    animator:
                    new CustomAnimator(
                        new FacingMouseAnimation(distanceOffsetTowardsPointer: 25*App.IMAGE_SCALE),
                        ImageSequenceAnimation.SingleImage(image)
                        ),
                    factoryCreation(weapon)
                    );
            
            weaponViews.Add("equipped", equippedItemView);
            weaponViews.Add("inventory", new InventoryItemView(weapon, image));
            return weaponViews;
        }
        
    }
}
