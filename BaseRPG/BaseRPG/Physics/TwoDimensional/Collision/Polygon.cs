using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public class Polygon : IShape2D
    {

        private Polygon2D polygon;
        private double angle;
        private readonly IGameObject owner;
        private readonly IMovementManager movementManager;

        public Polygon(IGameObject owner, IMovementManager movementManager,List<Point2D> vertices)
        {
            polygon = new Polygon2D(vertices);
            this.owner = owner;
            this.movementManager = movementManager;
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

        public IGameObject Owner => owner;

        public IMovementManager MovementManager => movementManager;

        public Vector2D GlobalPosition => new(movementManager.Position.Values[0], movementManager.Position.Values[1]);

        public void Rotate(double angle)
        {
            this.angle += angle;
            polygon = polygon.RotateAround(Angle.FromRadians(angle), new(0,0));
        }

        public bool CollidesWith(Vector2D point)
        {
            throw new NotImplementedException();
        }
        public bool IsColliding(IShape2D s2)
        {
            return IsColliding(s2.ToPolygon());
        }
        private bool IsColliding(Polygon2D polygon2) {
            if(Polygon2D.ArePolygonVerticesColliding(this.ToPolygon(), polygon2)) return true;
            if(AreEgesColliding(polygon2)) return true;
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

        public IShape2D Shifted(Vector2D shift)
        {
            return Shifted(shift.X,shift.Y);
        }
        public IShape2D Shifted(params double[] values)
        {
            //List<Point2D> vertices = new List<Point2D>();
            //polygon.Vertices.ToList().ForEach(v => vertices.Add(v));
            //var newPolygon = new Polygon2D(vertices);
            return new Polygon(Owner, movementManager.Copy(), polygon.TranslateBy(new(values[0], values[1])).Vertices.ToList());
        }

        public void SetRotation(double newAngle)
        {
            var deltaAngle = newAngle - angle;
            angle = newAngle;
            polygon = polygon.RotateAround(Angle.FromRadians(deltaAngle), new(0,0));
        }
    }
}
