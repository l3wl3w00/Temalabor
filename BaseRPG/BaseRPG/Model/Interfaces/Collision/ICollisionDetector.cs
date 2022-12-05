using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Collision
{
    public interface ICollisionDetector:IExisting
    {
        void OnCollision(ICollisionDetector other, double delta);
        virtual void OnCollisionExit(ICollisionDetector gameObject) { }

        bool CanCollide(ICollisionDetector other);

    }
}
