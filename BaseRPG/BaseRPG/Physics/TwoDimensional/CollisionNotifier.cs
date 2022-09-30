using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional
{
    public class CollisionNotifier
    {
        private List<Shape2D> collisionObjects = new List<Shape2D>();
        //if any 2 collisionObjects are colliding, invoke the CollisionOccured event
        public void CheckCollisions() {
            foreach (Shape2D shape1 in collisionObjects) {
                foreach (Shape2D shape2 in collisionObjects)
                {
                    if (shape1 == shape2) continue;
                    Collision.Collision collision = shape1.CollisionWith(shape2);
                    if (collision.IsColliding) {
                        NotifyCollision(shape1.Owner, shape2.Owner);
                    }
                }
            }
        }

        public void AddToObservedShapes(Shape2D shape) {
            collisionObjects.Add(shape);
        }

        public void NotifyCollision(IGameObject g1, IGameObject g2) {
            g1.OnCollision(g2);
            g2.OnCollision(g1);
        }
    }
}
