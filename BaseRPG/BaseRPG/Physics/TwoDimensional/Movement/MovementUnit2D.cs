using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class MovementUnit2D : IMovementUnit
    {
        private Vector2D movement;

        public MovementUnit2D(Vector2D movement)
        {
            this.movement = movement;
        }

        public MovementUnit2D(double v1, double v2)
        {
            this.movement = new Vector2D(v1, v2);
        }

        public double[] Values => new double[] { movement.X, movement.Y};

        public IMovementUnit Scaled(double scalar)
        {
            return new MovementUnit2D(movement * scalar);
        }
    }
}
