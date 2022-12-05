using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Worlds;

namespace BaseRPG.Model.Interfaces.ItemInterfaces
{
    public interface IItemFactory
    {
        public event Action<Item> OnItemCreated;
        Item Create(World world);
    }
}
