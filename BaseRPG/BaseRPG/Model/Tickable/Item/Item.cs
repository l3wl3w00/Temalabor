using BaseRPG.Model.Interfaces.Collecting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item
{
    public class Item : ICollectible
    {

        public void OnCollect(ICollector collector)
        {
            throw new NotImplementedException();
        }

    }
}
