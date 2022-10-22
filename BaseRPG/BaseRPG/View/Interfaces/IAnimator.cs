using BaseRPG.View.Animation;
using BaseRPG.View.Animation.ImageSequence;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public abstract class IAnimator
    {
        public abstract Tuple<double, double> Size { get; }
        void Start(TransformationAnimation2D transformationAnimation, ImageSequenceAnimation imageSequenceAnimation)
        {
            Start(transformationAnimation);
            Start(imageSequenceAnimation);
        }
        public abstract void Start(TransformationAnimation2D animation);
        public abstract void Start(ImageSequenceAnimation animation);

        public void ResetAll() {
            ResetImageSequence();
            ResetTransformation();
        }
        public abstract void ResetTransformation();
        public abstract void ResetImageSequence();
        public abstract void Animate(DrawingArgs animationArgs);

    }
}
