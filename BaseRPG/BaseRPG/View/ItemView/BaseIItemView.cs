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
    public abstract class BaseItemView : GameObjectView
    {
        private Item item;
        protected BaseItemView(Item item):base(null)
        {
            this.item = item;
        }


    }
}
