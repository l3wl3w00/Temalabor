using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
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
        private ICollisionDetector<IGameObject> owner;
        private IMovementManager movementManager;

        public Polygon(ICollisionDetector<IGameObject> owner, IMovementManager movementManager, IEnumerable<Point2D> vertices)
        {
            polygon = new Polygon2D(vertices);
            this.owner = owner;
            this.MovementManager = movementManager;
            if (movementManager != null)
                movementManager.Moved += () =>
                {
                    Angle angle = new Vector2D(movementManager.LastMovement.Values[0], movementManager.LastMovement.Values[1]).SignedAngleTo(new(0, 1), true);
                    SetRotation(angle.Radians);
                };
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

        public ICollisionDetector<IGameObject> Owner
        {
            get { return owner; }
            set { owner = value; }
        }


        public Vector2D GlobalPosition => new(MovementManager.Position.Values[0], MovementManager.Position.Values[1]);

        public IMovementManager MovementManager { get => movementManager; set => movementManager = value; }

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
            return IsColliding(s2.ToPolygon2D());
        }
        private bool IsColliding(Polygon2D polygon2) {
            if(Polygon2D.ArePolygonVerticesColliding(this.ToPolygon2D(), polygon2)) return true;
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
        public Polygon2D ToPolygon2D()
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
            return new Polygon(Owner, MovementManager.Copy(), polygon.TranslateBy(new(values[0], values[1])).Vertices.ToList());
        }

        public void SetRotation(double newAngle)
        {
            var deltaAngle = newAngle - angle;
            angle = newAngle;
            polygon = polygon.RotateAround(Angle.FromRadians(deltaAngle), new(0,0));
        }

        public Polygon ToPolygon()
        {
            return this;
        }

        public bool IsCollidingCircle(Circle circleSector)
        {
            throw new NotImplementedException();
        }
        public static Polygon Circle(
            ICollisionDetector<IGameObject> owner, 
            IMovementManager movementManager,
            Vector2D center,
            double radius,
            int numberOfVertices = 20) {
            return new Circle(owner, movementManager,center, radius).ToPolygon(numberOfVertices);
        }
        public static List<Point2D> Rectangle(Vector2D center, double width, double height) {
            return new List<Point2D> { 
                new(center.X-width/2,center.Y-height/2),
                new(center.X-width/2,center.Y+height/2),
                new(center.X+width/2,center.Y+height/2),
                new(center.X+width/2,center.Y-height/2)};
        }
    }
}
