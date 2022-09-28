using Windows.Foundation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;

namespace BaseRPG.View.EntityView
{
    public abstract class GameObjectView
    {
        private ICanvasImage image;
        public GameObjectView(ICanvasImage image = null)
        {
            this.image = image;
        }
        public abstract void Render(CanvasDrawEventArgs args,Camera camera);
        protected void DrawPicture(CanvasDrawEventArgs args, Camera camera, Vector2D globalPosition) {
            //args.DrawingSession.DrawImage(image);
            args.DrawingSession.FillRectangle(new Rect(globalPosition.X,globalPosition.Y,40,40),Color.FromArgb(255,255,0,0));
        }
    }
}
