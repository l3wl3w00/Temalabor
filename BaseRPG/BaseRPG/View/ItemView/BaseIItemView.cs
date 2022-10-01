using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView
{
    public abstract class BaseItemView : Drawable
    {
        private Item item;
        protected BaseItemView(Item item)
        {
            this.item = item;
        }

        public virtual void Render(CanvasDrawEventArgs args, Camera camera, CanvasControl sender)
        {
            throw new NotImplementedException();
        }
    }
}
