using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
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
        private List<Collision> collisions = new List<Collision>();
        //if any 2 collisionObjects are colliding, invoke the CollisionOccured event
        public void CheckCollisions() {
           
            foreach (IShape2D shape1 in collisionObjects) {
                foreach (IShape2D shape2 in collisionObjects)
                {
                    if (shape1 == shape2) continue;
                    var shiftedShape1 = shape1.Shifted(shape1.GlobalPosition);
                    var shiftedShape2 = shape2.Shifted(shape2.GlobalPosition);
                    if (shiftedShape1.IsColliding(shiftedShape2)) {
                        if(!CollisionExists(shiftedShape1.Owner,shiftedShape2.Owner))
                            collisions.Add(new Collision(shape1, shape2));
                        NotifyCollision(shape1.Owner, shape2.Owner);
                    }
                }
            }

            collisions.RemoveAll(c => !c.CheckColliding());
            collisionObjects.RemoveAll(s => !s.Owner.Exists);
        }

        public void AddToObservedShapes(IShape2D shape) {
            collisionObjects.Add(shape);
        }

        public void NotifyCollision(ICollisionDetector<IGameObject> g1, ICollisionDetector<IGameObject> g2) {
            g1.OnCollision(g2);
        }

        public bool CollisionExists(ICollisionDetector<IGameObject> g1, ICollisionDetector<IGameObject> g2) {
            foreach (var col in collisions)
            {
                if (col.Shape1.Owner == g1 && col.Shape2.Owner == g2) return true; 
                if (col.Shape1.Owner == g2 && col.Shape2.Owner == g1) return true;
            }
            return false;
        }
        private class Collision {
            public Collision(IShape2D shape1, IShape2D shape2)
            {
                Shape1 = shape1;
                Shape2 = shape2;
            }

            public IShape2D Shape1 { get; set; }
            public IShape2D Shape2 { get; set; }
            public bool CheckColliding()
            {
                var shiftedShape1 = Shape1.Shifted(Shape1.GlobalPosition);
                var shiftedShape2 = Shape2.Shifted(Shape2.GlobalPosition);
                var result = shiftedShape1.IsColliding(shiftedShape2);
                if (!Shape1.Owner.Exists) result = false;
                if (!Shape2.Owner.Exists) result = false;
                if (!result)
                {
                    Shape1.Owner.OnCollisionExit(Shape2.Owner);
                    Shape2.Owner.OnCollisionExit(Shape1.Owner);
                }
                return result;
            }
        }
    }
}
