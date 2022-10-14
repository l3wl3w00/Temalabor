using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    internal class EmptyAnimationStrategy : IAnimationStrategy
    {
        private Vector2D relativePosition;

        public EmptyAnimationStrategy(Vector2D relativePosition)
        {
            this.relativePosition = relativePosition;
        }

        public event Action<IAnimationStrategy> OnAnimationCompleted;


        public Transform2DEffect GetImage(DrawingArgs animationArgs)
        {
            throw new NotImplementedException();
        }

        public Transform2DEffect GetImage(DrawingArgs animationArgs, Matrix3x2 initialMatrix = default)
        {
            throw new NotImplementedException();
        }
    }
}
