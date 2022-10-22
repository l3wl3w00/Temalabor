using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class CenteredImageProvider:IImageProvider
    {
        private IImageProvider imageProvider;

        public CenteredImageProvider(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }
        public ICanvasImage GetByFilename(string fileName)
        {
            var transform = new Transform2DEffect();
            Tuple<double, double> size = imageProvider.GetSizeByFilename(fileName);
            transform.TransformMatrix = Matrix3x2.CreateTranslation(-(float)size.Item1/2, -(float)size.Item2/2);
            transform.Source = imageProvider.GetByFilename(fileName);
            return transform;
        }

        public Tuple<double, double> GetSizeByFilename(string fileName)
        {
            return imageProvider.GetSizeByFilename(fileName);
        }
    }
}
