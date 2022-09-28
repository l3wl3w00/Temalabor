using BaseRPG.Model.Data;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.WorldInterfaces
{
    public interface IWorldInitializationStrategy
    {
        void Initialize(GameObjectContainer gameObjectContainer);
    }
}
