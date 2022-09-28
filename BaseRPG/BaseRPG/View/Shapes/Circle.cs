using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Shapes
{
    public class Circle : Shape2D
    {
        private Vector2D center;
        private double radius;
        public Vector2D Middle { get { return center; } }

        public bool CollidesWith(Vector2D point)
        {
            return (center - point).Length <= radius;
        }

        public bool CollidesWith(Shape2D r2)
        {
            throw new NotImplementedException();
        }

        public void RotateAround(double angle, Point2D axis)
        {
            throw new NotImplementedException();
        }

        public Polygon2D ToPolygon()
        {
            throw new NotImplementedException();
        }
    }
}
