using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Collision
{
    public interface ICollisionDetector<T>:IExisting
    {
        void OnCollision(ICollisionDetector<T> other, double delta);
        virtual void OnCollisionExit(ICollisionDetector<T> gameObject) { }
    }
}
