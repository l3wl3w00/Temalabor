using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IInitializationStrategy
    {
        void Initialize(Controller controller, IPhysicsFactory physicsFactory);
    }
}
