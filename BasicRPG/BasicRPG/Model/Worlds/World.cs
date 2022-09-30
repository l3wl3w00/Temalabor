using BasicRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Worlds
{
    public abstract class World : ITickable
    {
        private List<ITickable> tickables;

        public void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
