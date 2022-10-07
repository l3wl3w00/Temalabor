using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class AnimationTimer
    {
        private double secondsSinceStarted;
        private double maxSeconds;

        public double SecondsSinceStarted { get => secondsSinceStarted; }
        public double MaxSeconds { get => maxSeconds;  }

        public event Action Elapsed;
        public AnimationTimer(double maxSeconds)
        {
            this.secondsSinceStarted = 0;
            this.maxSeconds = maxSeconds;
        }
        public void Tick(double secondsPassed) {
            secondsSinceStarted += secondsPassed;
            if (SecondsSinceStarted >= MaxSeconds) {
                Elapsed?.Invoke();
            }
        }
        
    }
}
