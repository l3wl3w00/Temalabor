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
        public DefaultImageRenderer(string imageName, IImageProvider imageProvider)
        {
            Image = imageProvider.GetByFilename(imageName);
            var size = imageProvider.GetSizeByFilename(imageName);
            middleOffset = new(size.Item1 / 2.0, size.Item2 / 2.0);
        }
        public DefaultImageRenderer(ICanvasImage image, Vector2D middleOffset)
        {
            this.Image = image;
            this.middleOffset = middleOffset;
        }
        private DefaultImageRenderer() { }

        public DefaultImageRenderer(ICanvasImage background, Tuple<double, double> sizeOfImage)
            : this(background, new Vector2D(sizeOfImage.Item1 / 2.0, sizeOfImage.Item2 / 2.0))
        {

        }

        public Vector2D MiddleOffset { get => middleOffset; }
        public ICanvasImage Image { set => image.Source = value; }

        public Angle ImageRotation => Angle.FromRadians(rotation);

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

        //public void SetImageRotation(double angle, Vector2D around)
        //{
        //    image.TransformMatrix = Matrix3x2.CreateRotation(
        //            (float)(angle),
        //            new((float)around.X, (float)around.Y));

        //}
        public void SetImageRotation(double angle)
        {
            rotation = angle;
            image.TransformMatrix = Matrix3x2.CreateRotation(
                        (float)(angle),
                        new((float)MiddleOffset.X, (float)MiddleOffset.Y));

            //SetImageRotation(angle, MiddleOffset);

        }

        public void SetImageScale(double xScale, double yScale)
        {
            image.TransformMatrix = Matrix3x2.CreateScale((float)xScale, (float)yScale);
        }
    }
}
