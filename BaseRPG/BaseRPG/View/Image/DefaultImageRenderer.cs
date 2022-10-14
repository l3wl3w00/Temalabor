using BaseRPG.View.Animation;
using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class DefaultImageRenderer : IImageRenderer
    {
        private Transform2DEffect image = new();

        private Vector2D middleOffset;
        private double rotation;
        private Tuple<double, double> imageSize;

        public DefaultImageRenderer(string imageName, IImageProvider imageProvider)
        {
            image.Source = imageProvider.GetByFilename(imageName);
            imageSize = imageProvider.GetSizeByFilename(imageName);
            middleOffset = new(ImageSize.Item1 / 2.0, ImageSize.Item2 / 2.0);
        }


        public DefaultImageRenderer(ICanvasImage image, Tuple<double, double> sizeOfImage)
        {
            this.image.Source = image;
            imageSize = sizeOfImage;
            middleOffset = new(ImageSize.Item1 / 2.0, ImageSize.Item2 / 2.0);
        }

        public Vector2D MiddleOffset { get => middleOffset; }
        public ICanvasImage Image { get => image; }

        public Angle ImageRotation => Angle.FromRadians(rotation);

        public Tuple<double, double> ImageSize => imageSize;

        public Vector2 PositionOnScreen(DrawingArgs drawingArgs) {
            return new(
                    (float)(drawingArgs.PositionOnScreen.X - MiddleOffset.X),
                    (float)(drawingArgs.PositionOnScreen.Y - MiddleOffset.Y)
                    );
        }
        public void Render(DrawingArgs drawingArgs)
        {
            drawingArgs.Args.DrawingSession.DrawImage(image, PositionOnScreen(drawingArgs));
        }

        public void SetImageRotation(double angle)
        {
            rotation = angle;
            image.TransformMatrix = Matrix3x2.CreateRotation(
                        (float)(angle),
                        new((float)MiddleOffset.X, (float)MiddleOffset.Y));
        }

        public void SetImageScale(double xScale, double yScale)
        {
            image.TransformMatrix = Matrix3x2.CreateScale((float)xScale, (float)yScale);
        }
    }
}
