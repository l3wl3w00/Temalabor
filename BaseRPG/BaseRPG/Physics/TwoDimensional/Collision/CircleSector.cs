using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public class CircleSector : IShape2D
    {
        private Vector2D center;
        private double radius;
        private double angleRange;
        private double facingAngle;

        public Vector2D Middle { get { return center; } }

        public IGameObject Owner => throw new NotImplementedException();

        public IMovementManager MovementManager => throw new NotImplementedException();

        public Vector2D GlobalPosition => throw new NotImplementedException();

        public bool CollidesWith(Vector2D point)
        {
            if((point - center).Length <= radius)
                if((point - center).AngleTo(Vector2D.FromPolar(1, Angle.FromRadians(facingAngle))) 
                    < Angle.FromRadians(angleRange/2))
                    return true;
            return false;
        }

        public bool IsColliding(IShape2D r2)
        {
            throw new NotImplementedException();
        }

        public void Rotate(double angle)
        {
            throw new NotImplementedException();
        }

        public IShape2D Shifted(Vector2D shift)
        {
            throw new NotImplementedException();
        }

        public IShape2D Shifted(params double[] values)
        {
            throw new NotImplementedException();
        }

        public Polygon2D ToPolygon()
        {
            throw new NotImplementedException();
        }
    }
}
