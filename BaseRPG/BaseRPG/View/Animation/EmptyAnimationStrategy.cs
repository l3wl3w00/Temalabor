using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    internal class EmptyAnimationStrategy : IAnimationStrategy
    {
        private Vector2D relativePosition;
        private readonly IImageRenderer imageRenderer;

        public EmptyAnimationStrategy(Vector2D relativePosition, IImageRenderer imageRenderer)
        {
            this.relativePosition = relativePosition;
            this.imageRenderer = imageRenderer;
        }

        public event Action<IAnimationStrategy> OnAnimationCompleted;

        public void Animate(DrawingArgs animationArgs)
        {
            imageRenderer.Render(animationArgs );
        }

        public IImageRenderer GetConfiguredImageRenderer(DrawingArgs animationArgs)
        {
            throw new NotImplementedException();
        }
    }
}
