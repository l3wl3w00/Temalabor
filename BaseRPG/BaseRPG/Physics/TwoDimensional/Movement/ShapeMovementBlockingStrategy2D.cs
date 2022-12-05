using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class ShapeMovementBlockingStrategy2D : IMovementBlockingStrategy
    {
        private readonly IShape2D shape;
        private readonly CollisionNotifier2D collisionNotifier2D;
        public ShapeMovementBlockingStrategy2D(IShape2D shape, CollisionNotifier2D collisionNotifier2D)
        {
            this.shape = shape;
            this.collisionNotifier2D = collisionNotifier2D;
        }

        public IMovementUnit GenerateMovement(IMovementUnit movement, IPositionUnit position)
        {
            if (!(shape.Owner is Hero)) return movement;
            Vector2D movementVector = MovementUnit2D.ToVector(movement);
            //angle to the y axis

            var rays = shape.CastRays(movementVector.Normalize(),3);
            var ray2 = rays.Rays[1];
            var ray = new NormalRay(shape.ShiftedByPos.Middle,movementVector.Normalize());
            if (!ray.Origin.Equals(ray2.Origin, 0.00001)) {
            
            }
            double shortestDistance = movementVector.Length;
            //var shapes = collisionNotifier2D.ShapesCollidingWith(shape.ShiftedByPos.Shifted(MovementUnit2D.ToVector(movement)));
            var shapes = collisionNotifier2D.Shapes;
            foreach (var s in shapes)
            {

                if (shape.Owner == s.Owner) continue;
                if (!shape.Owner.CanCollide(s.Owner))
                {
                    //return new MovementUnit2D(-1*(s.GlobalPosition - shape.GlobalPosition).Normalize());
                    var other = s.ShiftedByPos.ToPolygon2D();
                    //var intersections = rays.Intersections(other);
                    //var intersection = intersections[1];//.ClosestIntersectionDistance();
                    var intersection = ray.Intersect(other);

                    var closest = intersection.ClosestInFront();
                    if (closest != null)
                    {

                        var length = (intersection.Ray.Origin - closest.Value).Length;
                        if (length < shortestDistance)
                        {
                            shortestDistance = length;
                            Console.WriteLine($"new shortest distance: {shortestDistance}");
                        }
                    }
                    //var intersection = rays.Intersections(s.ShiftedByPos.ToPolygon2D()).ClosestIntersectionDistance();
                    //if (intersection < shortestDistance) {
                    //    shortestDistance = intersection;
                    //}

                }
            }
            if (shortestDistance < movementVector.Length-0.000001) {
                shortestDistance -= movementVector.Length * 0.1;
            }
            return movement.WithLength(shortestDistance );
        }

        
    }
}
