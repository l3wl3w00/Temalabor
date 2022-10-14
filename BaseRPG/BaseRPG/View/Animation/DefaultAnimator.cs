using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;

namespace BaseRPG.View.Animation
{
    public class DefaultAnimator : IAnimator
    {

        private IAnimationStrategy currentStrategy;
        private IAnimationStrategy defaultStrategy;
        private readonly IImageProvider imageProvider;
        private readonly string imageName;
        private readonly bool cancelAnimations;
        private bool resetQueued;
        public DefaultAnimator(IAnimationStrategy defaultStrategy, IImageProvider imageProvider,
            string imageName, bool cancelAnimations = false)
        {
            this.defaultStrategy = defaultStrategy;
            this.imageProvider = imageProvider;
            this.imageName = imageName;
            this.cancelAnimations = cancelAnimations;
            Start(defaultStrategy);
        }
        public void Animate(DrawingArgs animationArgs)
        {
            if (resetQueued) Reset();
            Tuple<double, double> imageSize = imageProvider.GetSizeByFilename(imageName);
            Matrix3x2 initialMatrix = Matrix3x2.CreateTranslation(-(float)imageSize.Item1/2,-(float)imageSize.Item2/2); 
            Transform2DEffect transform2DEffect = currentStrategy.GetImage(animationArgs, initialMatrix);

            transform2DEffect.Source = imageProvider.GetByFilename(imageName);
            animationArgs.Args.DrawingSession.DrawImage(transform2DEffect
                , (float)(animationArgs.PositionOnScreen.X)
                , (float)(animationArgs.PositionOnScreen.Y));
            //currentStrategy.Animate(animationArgs);
        }

        public void Start(IAnimationStrategy newStrategy)
        {
           
            if (!cancelAnimations)
                //only change the animation if current = default OR new = default
                if (!(currentStrategy == defaultStrategy || newStrategy == defaultStrategy))
                    return;
            
            currentStrategy = newStrategy;

            currentStrategy.OnAnimationCompleted += (a) => { resetQueued = true; };
        }
        public void Reset()
        {
            currentStrategy = defaultStrategy;
            resetQueued = false;
        }
    }
}
