using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class PositionObserver : IPositionTracker
    {
        private Func<Vector2D> positionGetterMethod;
        public PositionObserver( Func<Vector2D> positionGetterMethod)
        {
            this.positionGetterMethod = positionGetterMethod;
        }

        public Vector2D Position => positionGetterMethod();

    }
}
