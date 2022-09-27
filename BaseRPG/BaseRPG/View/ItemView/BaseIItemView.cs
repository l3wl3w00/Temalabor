using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView
{
    public abstract class BaseItemView : TickableView
    {
        protected BaseItemView(ITickable tickable) : base(tickable)
        {
        }

        public Item Item { get; }
        public abstract void Render();
    }
}
