using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory
{
    public class NormalBowAnimationFactory : IAttackAnimationFactory
    {
        private AnimationProvider animationProvider;
        private IImageProvider imageProvider;
        private Controller.Controller controller;
        private Weapon weapon;
        private readonly IPositionProvider positionProvider;

        public NormalBowAnimationFactory(AnimationProvider animationProvider, IImageProvider imageProvider, Controller.Controller controller, Weapon weapon,IPositionProvider positionProvider)
        {
            this.animationProvider = animationProvider;
            this.imageProvider = imageProvider;
            this.controller = controller;
            this.weapon = weapon;
            this.positionProvider = positionProvider;
        }

        public ImageSequenceAnimation CreateImageSequence(AttackBuilder attackBuilder)
        {
            ImageSequenceAnimation bowAttackAnimation = ImageSequenceAnimation.WithTimeFrame(imageProvider,animationProvider.Get("bow-attack"),0.2);
            bowAttackAnimation.OnAnimationCompleted +=
                a => {
                    var ownerToMouseDirection = (positionProvider.Position - PositionUnit2D.ToVector2D(weapon.Owner.Position)).Normalize() * 100;
                    controller.QueueAction(
                        () => attackBuilder
                            
                            .MovementStrategy(new StraightMovementStrategy2D(ownerToMouseDirection*10))
                            .CreateAttack(new PositionUnit2D(ownerToMouseDirection + PositionUnit2D.ToVector2D(weapon.Owner.Position)))
                            
                        );;
                };
                
            return bowAttackAnimation;
        }

        public TransformationAnimation2D CreateTransformation(AttackBuilder attackBuilder)
        {
            return null;
        }
    }
}
