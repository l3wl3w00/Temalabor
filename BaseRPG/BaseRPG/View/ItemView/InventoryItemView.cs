using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;


namespace BaseRPG.View.ItemView
{
    public class InventoryItemView : BaseItemView
    {
        public InventoryItemView(Item item)
        {
        }

        public override Vector2D ObservedPosition => throw new NotImplementedException();

        protected override Item ObservedItem => throw new NotImplementedException();

        public override void OnRender(DrawingArgs drawingArgs)
        {
            throw new NotImplementedException();
        }
    }
}
