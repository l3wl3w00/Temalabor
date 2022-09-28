using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class PositionManager
    {
        private Vector2D position;

        public PositionManager(Vector2D position)
        {
            this.position = position;
        }

        public Vector2D Position { get { return position; } }
        public void Move(Vector2D moveDirection)
        {
            position += moveDirection;
        }
    }
}
