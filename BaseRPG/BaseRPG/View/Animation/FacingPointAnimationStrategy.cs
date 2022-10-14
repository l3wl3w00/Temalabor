using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    // Draws the given image on the line defined by
    // - the position given when calling the animation, and
    // - the position given when creating the object. 
    public class FacingPointAnimationStrategy : IAnimationStrategy
    {
        private PositionTracker facingPositionTracker;
        private double distanceOffsetTowardsPointer;
        public FacingPointAnimationStrategy(
            PositionTracker facingPositionTracker,
            double distanceOffsetTowardsPointer = 0)
        {
            this.facingPositionTracker = facingPositionTracker;
            this.DistanceOffsetTowardsPointer = distanceOffsetTowardsPointer;
            FirstPointOffset = new(0, 0);
        }

        public event Action<IAnimationStrategy> OnAnimationCompleted;

        public Vector2D FirstPointOffset { private get; set; }
        public double DistanceOffsetTowardsPointer { private get => distanceOffsetTowardsPointer; set => distanceOffsetTowardsPointer = value; }
        public Vector2D PointPosition { set { facingPositionTracker.Position = value; } }

        public Transform2DEffect GetImage(DrawingArgs animationArgs,Matrix3x2 initialMatrix = new())
        {
            Vector2D firstPoint = animationArgs.PositionOnScreen + FirstPointOffset;
            Vector2D secondpoint = facingPositionTracker.Position;
            var distanceVector = (secondpoint - firstPoint);
            float angle = MathF.Atan2((float)distanceVector.Y, (float)distanceVector.X) + MathF.PI / 2;

            Transform2DEffect transform2DEffect = new Transform2DEffect();
            transform2DEffect.TransformMatrix =
                initialMatrix *
                Matrix3x2.CreateTranslation(0,(float)-distanceOffsetTowardsPointer)*
                Matrix3x2.CreateRotation(angle);
            return transform2DEffect;
        }
    }
}
