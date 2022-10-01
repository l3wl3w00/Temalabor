using BaseRPG.Model.Tickable.Item;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;


namespace BaseRPG.View.ItemView
{
    public class InventoryItemView : BaseItemView
    {
        public InventoryItemView(Item item) : base(item)
        {
        }

        public override void Render(CanvasDrawEventArgs args, Camera camera, CanvasControl sender)
        {
            throw new NotImplementedException();
        }
    }
}
