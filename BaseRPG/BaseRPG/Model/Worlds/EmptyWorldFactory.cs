using BaseRPG.Model.Interfaces.WorldInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class EmptyWorldFactory:IWorldFactory
    {
        public World Create()
        {
            return new World(new EmptyWorldInitializationStrategy());
        }
    }
}
