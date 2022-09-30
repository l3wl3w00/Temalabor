using BasicRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Tickable.Item
{
    public class Item : ICollectible, ITickable
    {

        public void OnCollect(ICollector collector)
        {
            throw new NotImplementedException();
        }

        public void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
