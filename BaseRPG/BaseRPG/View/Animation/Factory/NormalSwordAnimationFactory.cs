using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory
{
    public class NormalSwordAnimationFactory : IAttackAnimationFactory
    {
        private readonly Controller.Controller controller;
        private Weapon weapon;

        public NormalSwordAnimationFactory(Controller.Controller controller, Weapon weapon)
        {
            this.controller = controller;
            this.weapon = weapon;
        }

        public ImageSequenceAnimation CreateImageSequence(AttackBuilder attackBuilder)
        {
            return null;
        }

        public TransformationAnimation2D CreateTransformation(AttackBuilder attackBuilder)
        {
            SwordSwingAnimation swordSwingAnimation =
                    new SwordSwingAnimation(Angle.FromDegrees(120), 0.3);
            swordSwingAnimation.OnAnimationAlmostEnding +=
                a => controller.QueueAction(() => attackBuilder.CreateAttack(
                        new PhysicsFactory2D().CreatePosition(
                            Vector2D.FromPolar(25*App.IMAGE_SCALE, swordSwingAnimation.StartingAngle)
                            + PositionUnit2D.ToVector2D(weapon.Owner.Position)

                    ))
                );
            return swordSwingAnimation;
        }
    }
}
