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
    internal class EmptyAnimation : Interfaces.TransformationAnimation2D
    {
        private Vector2D relativePosition;

        public EmptyAnimation(Vector2D relativePosition)
        {
            this.relativePosition = relativePosition;
        }


        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            throw new NotImplementedException();
        }
    }
}
