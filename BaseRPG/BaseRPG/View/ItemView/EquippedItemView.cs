using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI;

namespace BaseRPG.View.ItemView
{
    public class EquippedItemView : BaseItemView
    {
        private Unit owner;
        private IImageProvider imageProvider;
        private readonly PositionTracker mousePositionTracker;
        private IAnimator animator;
        private Weapon observedWeapon;

        protected override Item ObservedItem { get { return observedWeapon; } }
        public EquippedItemView(Weapon item, Unit owner, IAnimator animator, IImageProvider imageProvider, PositionTracker mousePositionTracker)
        {
            this.observedWeapon = item;
            this.Owner = owner;
            this.animator = animator;
            this.imageProvider = imageProvider;
            this.mousePositionTracker = mousePositionTracker;
        }

        public override Vector2D ObservedPosition => new(Owner.Position.Values[0], Owner.Position.Values[1]);

        public Unit Owner { get => owner; set => owner = value; }

        public override void OnRender(DrawingArgs drawingArgs)
        {
            animator.Animate(drawingArgs);

        }

        public void StartHeavyAttackChargeAnimation() {
            
            throw new NotImplementedException();
        }

        public void StartHeavyAttackStrikeAnimation()
        {
            throw new NotImplementedException();
        }
        private Vector2D OwnerPos { get => new(owner.Position.Values[0], owner.Position.Values[1]); }
        public void StartLightAttackAnimation() {
            SwordSwingAnimationStrategy swordSwingAnimation = new SwordSwingAnimationStrategy(
                     Angle.FromDegrees(120),
                    0.3);
            swordSwingAnimation.OnAnimationAlmostEnding +=
                a => observedWeapon.CreateLightAttack(
                    new PhysicsFactory2D().CreatePosition(
                        Vector2D.FromPolar(100,swordSwingAnimation.StartingAngle) 
                        + new Vector2D(owner.Position.Values[0],owner.Position.Values[1])
                        )
                ) ;
            animator.Start(swordSwingAnimation);
        }
    }
}
