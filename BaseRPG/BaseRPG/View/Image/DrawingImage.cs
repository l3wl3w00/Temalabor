using BaseRPG.View.Animation;
using BaseRPG.View.Interfaces;
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
            drawingArgs.DrawingSession.DrawImage(imageProvider.GetByFilename(imagePath));
        }
        public Tuple<double, double> Size {
            get {
                return imageProvider.GetSizeByFilename(imagePath);
            }
        }
    }
}
