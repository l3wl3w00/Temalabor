using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
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
        private IImageRenderer imageRenderer;
        private double distanceOffsetTowardsPointer;
        public FacingPointAnimationStrategy(
            PositionTracker facingPositionTracker,
            IImageRenderer imageRenderer,
            double distanceOffsetTowardsPointer = 0)
        {
            this.facingPositionTracker = facingPositionTracker;
            this.imageRenderer = imageRenderer;
            this.DistanceOffsetTowardsPointer = distanceOffsetTowardsPointer;
            FirstPointOffset = new(0, 0);
        }
        public FacingPointAnimationStrategy(
            IImageRenderer imageRenderer,
            double distanceOffsetTowardsPointer = 0):this(new(),imageRenderer,distanceOffsetTowardsPointer)
        {

        }

        public event Action<IAnimationStrategy> OnAnimationCompleted;

        public Vector2D FirstPointOffset { private get; set; }
        public bool Debug { private get; set; }
        public double DistanceOffsetTowardsPointer { private get => distanceOffsetTowardsPointer; set => distanceOffsetTowardsPointer = value; }
        public Vector2D PointPosition { set { facingPositionTracker.Position = value; } }
        public void Animate(DrawingArgs animationArgs)
        {
            Vector2D firstPoint = animationArgs.PositionOnScreen + FirstPointOffset;
            Vector2D secondpoint = facingPositionTracker.Position;
            var offset = (secondpoint - firstPoint).Normalize() * DistanceOffsetTowardsPointer;

            imageRenderer.SetImageRotation((float)Math.Atan2(offset.Y, offset.X) + MathF.PI / 2);
            imageRenderer.Render( new(
                animationArgs.Sender,
                animationArgs.Args,
                animationArgs.Delta,
                new((float)(firstPoint.X + offset.X),
                    (float)(firstPoint.Y + offset.Y)
                    )));
            //Debug = true;
            if (Debug)
            {
                Vector2 firstPointVec = new((float)firstPoint.X, (float)firstPoint.Y);
                Vector2 secondPointVec = new((float)secondpoint.X, (float)secondpoint.Y);
                animationArgs.Args.DrawingSession.FillCircle(
                    firstPointVec,
                    10, Windows.UI.Color.FromArgb(255, 255, 0, 0));
                animationArgs.Args.DrawingSession.FillCircle(
                    secondPointVec,
                    10, Windows.UI.Color.FromArgb(255, 255, 0, 0));
                animationArgs.Args.DrawingSession.DrawLine(firstPointVec, secondPointVec, Windows.UI.Color.FromArgb(255, 255, 0, 0), 2);
            }
        }


    }
}
