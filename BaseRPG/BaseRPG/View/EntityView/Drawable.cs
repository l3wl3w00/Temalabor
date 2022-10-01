using Windows.Foundation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using BaseRPG.Model.Interfaces.Movement;
using Windows.Storage;

namespace BaseRPG.View.EntityView
{
    public interface Drawable
    {
        //private ICanvasImage image;
        //public GameObjectView(ICanvasImage image)
        //{
        //    this.image = image;
        //}
        void Render(CanvasDrawEventArgs args,Camera camera, CanvasControl sender);
        //protected void DrawPicture(CanvasDrawEventArgs args, Camera camera, IPositionUnit globalPosition) {
        //    BeforeImageDrawn(ref image);
        //    args.DrawingSession.DrawImage(image, (float)globalPosition.Values[0], (float)globalPosition.Values[1]);
        //    args.DrawingSession.FillRectangle(new Rect(globalPosition.Values[0], globalPosition.Values[1],40,40),Color.FromArgb(255,255,0,0));
        //}
        //public virtual void BeforeImageDrawn(ref ICanvasImage image) { }
    }
}
