using BaseRPG.Controller.Utility;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class AnimationLifeCycle<T> where T : class ,IAnimation<T>
    {
        private Default<T> animation;

        public AnimationLifeCycle(Default<T> animation)
        {
            this.animation = animation;
        }

        private bool resetQueued = false;

        public bool ResetQueued { get => resetQueued; }
        public T CurrentAnimation { get=>animation.CurrentValue; }

        public void Start(T newAnimation, bool cancelAnimation)
        {

            if (!cancelAnimation)
                //only change the animation if current = default OR new = default
                if (!(animation.IsDefault || newAnimation == animation.DefaultValue))
                    return;

            animation.CurrentValue = newAnimation;

            animation.CurrentValue.OnAnimationCompleted += (a) => { resetQueued = true; };
        }
        public void ResetIfQueued() {
            if (resetQueued) Reset();
        }
        public void Reset() {
            animation.Reset();
            resetQueued = false;
        }
    }
}
