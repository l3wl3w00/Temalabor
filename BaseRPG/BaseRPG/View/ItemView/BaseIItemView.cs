using BaseRPG.Model.Tickable.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView
{
    public abstract class BaseItemView
    {
        public Item Item { get; }
        public abstract void Render();
    }
}
