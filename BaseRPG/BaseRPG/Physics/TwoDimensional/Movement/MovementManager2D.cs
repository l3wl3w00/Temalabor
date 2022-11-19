using BaseRPG.Model.Data;
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
        private List<IMovementUnit> queuedMovements = new();
        public event Action Moved;

        public MovementManager2D(double x, double y)
        {
            this.position = new PositionUnit2D(x, y);
        }
        
        public IPositionUnit Position { get { return position; } }
        public Vector2D PositionAsVector { get { return new(position.Values[0], position.Values[1]); } }
        public void Move(IMovementUnit movement)
        {
            if (movement == null) return;
            if (almostZeroLength(movement)) return;
            lastMovement = movement;
            position.MoveBy(lastMovement);
            Moved?.Invoke();
        }

        private bool almostZeroLength(IMovementUnit movement) {
            return Math.Abs(movement.Values[0]) < double.Epsilon &&
                Math.Abs(movement.Values[1]) < double.Epsilon;
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

        private MovementManager2D(IPositionUnit position, IMovementUnit lastMovement)
        {
            this.position = new(position.Values[0], position.Values[1]);
            this.lastMovement = lastMovement;
        }

        public IMovementManager Copy()
        {
            return new MovementManager2D(position.Copy(),LastMovement.Clone());
        }

        public void QueueMovement(IMovementUnit movement)
        {
            if (movement == null) 
                movement = new MovementUnit2D(0,0);
            queuedMovements.Add(movement);
        }

        public void MoveQueued()
        {
            Vector2D movementVector = new();
            foreach (var movement in queuedMovements){
                movementVector += new Vector2D(movement.Values[0],movement.Values[1]);
            }
            Move(new MovementUnit2D(movementVector));
            queuedMovements.Clear();
        }
    }
}
