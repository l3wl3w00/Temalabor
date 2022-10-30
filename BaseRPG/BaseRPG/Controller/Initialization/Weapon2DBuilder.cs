using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
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
    public class Weapon2DBuilder
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

        public Weapon2DBuilder(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
            lightAttackFactory = new AttackBuilder(new DamagingAttackStrategy(20));
            heavyAttackFactory = new AttackBuilder(new DamagingAttackStrategy(40));
        }

        private Func<AttackBuilder, SwordSwingAnimation> SwordSwingAnimationCreation(Weapon weapon)
        {
            return (factory) =>
            {
                SwordSwingAnimation swordSwingAnimation =
                    new SwordSwingAnimation(Angle.FromDegrees(120), 0.3);
                swordSwingAnimation.OnAnimationAlmostEnding +=
                    a => factory.CreateAttack(
                        new PhysicsFactory2D().CreatePosition(
                            Vector2D.FromPolar(100, swordSwingAnimation.StartingAngle)
                            + new Vector2D(weapon.Owner.Position.Values[0], weapon.Owner.Position.Values[1])
                            )
                    );
                return swordSwingAnimation;
            };

        }
        public Weapon2DBuilder EquippedBy(Hero hero, PlayerControl playerControl) {
            owner = hero;
            this.playerControl = playerControl;
            return this;
        }
        public Weapon2DBuilder LightAttackBuilder(Attack2DBuilder attackBuilder) {
            lightAttackBuilder = attackBuilder;
            return this;
        }
        public Weapon2DBuilder LightAttackCreatedCallback(Action<FullGameObject2D> onAttackCreated) {
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
        public Weapon2DBuilder HeavyAttackBuilder(Attack2DBuilder attackBuilder)
        {
            heavyAttackBuilder = attackBuilder;
            return this;
        }
        public Weapon2DBuilder Image(string image) {
            this.image = image;
            return this;
        }
        public FullGameObject2D CreateSword()
        {
            
            weapon = new Weapon(heavyAttackFactory, lightAttackFactory, owner);
            owner.Collect(weapon); 
            owner.Equip(weapon);

            EquippedItemView equippedItemView =
                new EquippedItemView(
                    item: weapon,
                    owner: owner,
                    animator:
                    new DefaultAnimator(
                        new FacingMouseAnimation(distanceOffsetTowardsPointer: 100),
                        ImageSequenceAnimation.SingleImage(imageProvider,image)
                        ),
                    SwordSwingAnimationCreation(weapon)
                    );
            playerControl.EquippedItemView = equippedItemView;
            return new(weapon, null, equippedItemView);
        }
    }
}
