using BaseRPG.Model.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public class Polygon : Shape2D
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

        public IGameObject Owner => throw new NotImplementedException();

        public void RotateAround(double angle, Point2D axis)
        {
            polygon.RotateAround(Angle.FromRadians(angle), axis);
        }

        public bool CollidesWith(Vector2D point)
        {
            throw new NotImplementedException();
        }
        public Collision CollisionWith(Shape2D s2)
        {
            Polygon2D s2Polygon = s2.ToPolygon();
            

            if (Polygon2D.ArePolygonVerticesColliding(s2Polygon, polygon))
                return new Collision(this,s2,true);
            return new Collision(this, s2, false);
        }
        private bool IsColliding(Polygon2D polygon2) {
            if(Polygon2D.ArePolygonVerticesColliding(polygon, polygon2)) return true;
            if(AreEgesColliding(polygon)) return true;
            return false;
        }
        private bool AreEgesColliding(Polygon2D polygon2) {
            foreach (var edge in polygon.Edges)
            {
                foreach (var edge2 in polygon2.Edges)
                {
                    Point2D point2D = new Point2D();
                    if (edge.TryIntersect(edge2, out point2D, Angle.FromDegrees(1)))
                        return true;
                }
            }
            return false;
        }
        public Polygon2D ToPolygon()
        {
            return polygon;
        }
    }
}
