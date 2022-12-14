using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.ImageSequence;
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
    public class EquippedWeaponView : BaseItemView
    {
        private Animator animator;
        private readonly IAttackAnimationFactory lightAnimationFactory;
        private Weapon observedWeapon;
        public override bool Exists => Owner.Exists && observedWeapon.Exists; 
        protected override Item ObservedItem { get { return observedWeapon; } }
        public EquippedWeaponView(Weapon weapon, Animator animator,
            IAttackAnimationFactory lightAnimationFactory)
        {
            this.observedWeapon = weapon;
            this.animator = animator;
            this.lightAnimationFactory = lightAnimationFactory;
        }

        public override Vector2D ObservedPosition => PositionUnit2D.ToVector2D(Owner.Position);

        public Unit Owner { get => observedWeapon.Owner; }

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
        public void StartLightAttackAnimation(AttackBuilder attackFactory) {

                animator.Start(
                    lightAnimationFactory.CreateTransformation(attackFactory),
                    lightAnimationFactory.CreateImageSequence(attackFactory));
        }
    }
}
