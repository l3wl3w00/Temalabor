using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class World : ITickable
    {
        private List<ITickable> tickables;

        public IEnumerable<ITickable> Tickables { get;}

        public void OnTick()
        {
            foreach (ITickable t in tickables) 
                t.OnTick();
        }
        public void Add(ITickable tickable) {
            tickables.Add(tickable);
        }
        public void Remove(ITickable tickable) {
            tickables.Remove(tickable);
        }

    }
}
