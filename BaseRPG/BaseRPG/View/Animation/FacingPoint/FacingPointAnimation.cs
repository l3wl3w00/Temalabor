using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.FacingPoint
{
    public class FacingPointAnimation : FacingPointAnimationBase
    {
        private Vector2D point;

        public FacingPointAnimation(double distanceOffsetTowardsPointer) : base(distanceOffsetTowardsPointer)
        {
        }

        public Vector2D Point { set { point = value; } }

        public override event Action<Interfaces.TransformationAnimation2D> OnAnimationCompleted;

        public override Vector2D GetFacingPoint(DrawingArgs args)
        {
            return point;
        }
    }
}
