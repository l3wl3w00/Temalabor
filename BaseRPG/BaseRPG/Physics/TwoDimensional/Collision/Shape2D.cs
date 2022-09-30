using BaseRPG.Model.Interfaces;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public interface Shape2D
    {
        public IGameObject Owner { get; }
        public Vector2D Middle { get; }
        void RotateAround(double angle, Point2D axis);
        Polygon2D ToPolygon();
        Collision CollisionWith(Shape2D s2);
    }
}
