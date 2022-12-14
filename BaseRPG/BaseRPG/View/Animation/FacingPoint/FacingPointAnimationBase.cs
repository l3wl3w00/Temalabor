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

namespace BaseRPG.View.Animation.FacingPoint
{
    // Draws the given image on the line defined by
    // - the position given when calling the animation, and
    // - the position given when creating the object. 
    public abstract class FacingPointAnimationBase : Interfaces.TransformationAnimation2D
    {
        private Vector2D lastSecondPoint = new Vector2D(0,0);
        private double distanceOffsetTowardsPointer;
        public FacingPointAnimationBase(
            double distanceOffsetTowardsPointer)
        {
            DistanceOffsetTowardsPointer = distanceOffsetTowardsPointer;
            FirstPointOffset = new(0, 0);
        }


        public Vector2D FirstPointOffset { protected get; set; }
        public double DistanceOffsetTowardsPointer { private get => distanceOffsetTowardsPointer; set => distanceOffsetTowardsPointer = value; }
        public abstract Vector2D GetFacingPoint(DrawingArgs args);
        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            Vector2D firstPoint = animationArgs.PositionOnScreen + FirstPointOffset;
            Vector2D secondpoint = GetFacingPoint(animationArgs);
            if (secondpoint.Length < 0.0000000001) secondpoint = lastSecondPoint;
            else lastSecondPoint = secondpoint;
            var distanceVector = secondpoint - firstPoint;
            float angle = MathF.Atan2((float)distanceVector.Y, (float)distanceVector.X) + MathF.PI / 2;
            return Matrix3x2.CreateTranslation(0, (float)-distanceOffsetTowardsPointer) *
                Matrix3x2.CreateRotation(angle); ;
        }
    }
}
