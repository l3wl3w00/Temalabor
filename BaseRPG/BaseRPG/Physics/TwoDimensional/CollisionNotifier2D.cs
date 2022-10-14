using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional
{
    public class CollisionNotifier2D
    {
        private List<IShape2D> collisionObjects = new List<IShape2D>();
        //if any 2 collisionObjects are colliding, invoke the CollisionOccured event
        public void CheckCollisions() {
            
            foreach (IShape2D shape1 in collisionObjects) {
                foreach (IShape2D shape2 in collisionObjects)
                {
                    if (shape1 == shape2) continue;
                    var shifterShape1 = shape1.Shifted(shape1.GlobalPosition);
                    var shifterShape2 = shape2.Shifted(shape2.GlobalPosition);
                    if (shifterShape1.IsColliding(shifterShape2)) {
                        NotifyCollision(shape1.Owner, shape2.Owner);
                    }
                }
            }
            collisionObjects.RemoveAll(s => !s.Owner.Exists);
        }

        public void AddToObservedShapes(IShape2D shape) {
            collisionObjects.Add(shape);
        }

        public void NotifyCollision(IGameObject g1, IGameObject g2) {
            g1.OnCollision(g2);
        }

    }
}
