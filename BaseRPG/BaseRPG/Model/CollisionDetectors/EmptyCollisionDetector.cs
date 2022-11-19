using BaseRPG.Model.Interfaces.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.CollisionDetectors
{
    public class EmptyCollisionDetector<T> : ICollisionDetector<T>
    {
        private bool exists = true;
        public bool Exists { 
            get => exists;
            set {
                if (!value)
                    OnCeaseToExist?.Invoke();
                exists = value;
            } 
        }

        public event Action OnCeaseToExist;


        public void OnCollision(ICollisionDetector<T> other, double delta)
        {

        }
    }
}
