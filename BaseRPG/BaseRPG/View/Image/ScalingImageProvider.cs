using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class ScalingImageProvider:IImageProvider
    {
        private float scaleFactor;
        private IImageProvider imageProvider;
        public double ScaleFactor { get => scaleFactor; }

        public ScalingImageProvider(float scaleFactor, IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
            this.scaleFactor = scaleFactor; 
        }
        

        public ICanvasImage GetByFilename(string fileName)
        {
            var scaleEffect = new ScaleEffect();
            scaleEffect.Scale = new(scaleFactor, scaleFactor);
            scaleEffect.InterpolationMode = CanvasImageInterpolation.NearestNeighbor;
            scaleEffect.Source = imageProvider.GetByFilename(fileName);
            return scaleEffect;
        }

    }
}
