using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public class Collision
    {
        private Shape2D shape1;
        private Shape2D shape2;
        public Collision(Shape2D shape1,Shape2D shape2,bool isColliding)
        {
            IsColliding = isColliding;
            this.shape1 = shape1;
            this.shape2 = shape2;
        }
        public bool IsColliding { get; private set; }
        public event Action<Collision> CollisionOver;
        public void OnTick() {
            if (!shape1.CollisionWith(shape2).IsColliding) {
                IsColliding = false;
                CollisionOver(this);
            } 
        }
    }
}
