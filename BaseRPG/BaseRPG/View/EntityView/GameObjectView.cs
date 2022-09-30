using Windows.Foundation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using BaseRPG.Model.Interfaces.Movement;
using Windows.Storage;

namespace BaseRPG.View.EntityView
{
    public abstract class GameObjectView
    {
        private ICanvasImage image;
        public GameObjectView(ICanvasImage image)
        {
            this.image = image;
        }
        public abstract void Render(CanvasDrawEventArgs args,Camera camera);
        protected void DrawPicture(CanvasDrawEventArgs args, Camera camera, IPositionUnit globalPosition) {
            
            
            //args.DrawingSession.DrawImage(image);
            args.DrawingSession.FillRectangle(new Rect(globalPosition.Values[0], globalPosition.Values[1],40,40),Color.FromArgb(255,255,0,0));
        }
    }
}
