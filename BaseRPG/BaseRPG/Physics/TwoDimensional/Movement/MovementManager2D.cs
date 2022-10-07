using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class MovementManager2D : IMovementManager
    {
        private PositionUnit2D position;
        private IMovementUnit lastMovement;

        public event Action Moved;

        public MovementManager2D(double x, double y)
        {
            this.position = new PositionUnit2D(x, y);
        }
        
        public IPositionUnit Position { get { return position; } }
        public void Move(IMovementUnit movement)
        {
            lastMovement = movement;
            position.MoveBy(lastMovement);
            Moved?.Invoke();
        }


        public IMovementUnit LastMovement
        {
            get
            {
                if (lastMovement == null) {
                    return new MovementUnit2D(0, 0);
                }
                return lastMovement;
            }
        }
        private Vector2D toVector2D(IMovementUnit movementUnit) {
            double[] values = movementUnit.Values;
            return new(values[0], values[1]);
        }
        private Vector2D toVector2D(IPositionUnit positionUnit)
        {
            double[] values = positionUnit.Values;
            return new(values[0], values[1]);
        }
    }
}
