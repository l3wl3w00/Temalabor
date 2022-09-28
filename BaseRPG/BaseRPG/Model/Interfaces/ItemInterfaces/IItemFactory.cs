using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRPG.Model.Tickable.Item;
namespace BaseRPG.Model.Interfaces.ItemInterfaces
{
    public interface IItemFactory
    {
        Item Create();
    }
}
