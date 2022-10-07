using BaseRPG.View.Interfaces;
using System;

namespace BaseRPG.View.Animation
{
    public class DefaultAnimator : IAnimator
    {

        private IAnimationStrategy currentStrategy;
        private IAnimationStrategy defaultStrategy;
        private readonly bool cancelAnimations;
        private bool resetQueued;
        public DefaultAnimator(IAnimationStrategy defaultStrategy, bool cancelAnimations = false)
        {
            this.defaultStrategy = defaultStrategy;
            this.cancelAnimations = cancelAnimations;
            Start(defaultStrategy);
        }
        public void Animate(DrawingArgs animationArgs)
        {
            if (resetQueued) Reset();
            currentStrategy.Animate(animationArgs);
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
