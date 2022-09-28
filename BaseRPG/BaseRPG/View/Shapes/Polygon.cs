using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Shapes
{
    public class Polygon:Shape2D
    {

        private Polygon2D polygon;
        public Polygon(List<Point2D> vertices)
        {
            polygon = new Polygon2D(vertices);
        }

        public Vector2D Middle
        {
            get
            {
                
                Vector2D sum = new Vector2D(0, 0);
                foreach (Point2D p in polygon.Vertices)
                {
                    var v = new Vector2D(p.X, p.Y);
                    sum += v;
                }
                return sum / polygon.VertexCount;
            }
        }

        public void RotateAround(double angle, Point2D axis)
        {
            polygon.RotateAround(Angle.FromRadians(angle),axis);
        }

        public bool CollidesWith(Vector2D point)
        {
            throw new NotImplementedException();
        }
        public bool CollidesWith(Shape2D s2)
        {

            return Polygon2D.ArePolygonVerticesColliding(s2.ToPolygon(), polygon);
        }

        public Polygon2D ToPolygon()
        {
            return polygon;
        }
    }
}
