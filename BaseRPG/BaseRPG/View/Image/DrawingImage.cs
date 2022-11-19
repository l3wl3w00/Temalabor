using BaseRPG.View.Animation;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class DrawingImage
    {
        private string imagePath;
        private IImageProvider imageProvider;

        public DrawingImage(string imagePath, IImageProvider imageProvider)
        {
            this.imagePath = imagePath;
            this.imageProvider = imageProvider;
        }

        public void Draw(DrawingArgs drawingArgs) {
            Vector2D pos = drawingArgs.PositionOnScreen;
            drawingArgs.DrawingSession.DrawImage(imageProvider.GetByFilename(imagePath),new((float)pos.X,(float)pos.Y));
        }
        public Tuple<double, double> Size {
            get {
                return imageProvider.GetSizeByFilename(imagePath);
            }
        }
    }
}
