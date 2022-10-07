using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class SwordSwingAnimationStrategy : IAnimationStrategy
    {
        public event Action<SwordSwingAnimationStrategy> OnAnimationAlmostEnding;
        private bool hasInvokedAnimationAlmostEnding = false;

        private readonly Angle angleRange;
        private readonly Angle startingAngle;
        private Angle currentAngle;
        public Angle GetAngle() { return currentAngle; }
        private FacingPointAnimationStrategy facingPointAnimationStrategy;
        private PositionTracker positionTracker;

        private bool isOver = false;
        
        private AnimationTimer timer;

        private List<IImageRenderer> imageRenderers = new();

        public SwordSwingAnimationStrategy(
            IImageRenderer imageRenderer,
            Angle angleRange,
            Angle startingAngle,
            double seconds)
        {

            this.angleRange = angleRange;
            this.startingAngle = startingAngle;
            this.currentAngle = startingAngle;
            initTimers(seconds, seconds/4);


            positionTracker = new(Vector2D.FromPolar(100, startingAngle));
            facingPointAnimationStrategy = new FacingPointAnimationStrategy(positionTracker,imageRenderer,100);
            positionTracker.Position = Vector2D.FromPolar(200, startingAngle);
            OnAnimationAlmostEnding += a => hasInvokedAnimationAlmostEnding = true;
        }

        public Angle GetCurrentAngle() { 
            return currentAngle;
        }
        public event Action<IAnimationStrategy> OnAnimationCompleted;

        public void Animate(DrawingArgs animationArgs)
        {

            if (!isOver)
            {
                positionTracker.Position = nextPosition(animationArgs.PositionOnScreen);
                setStateForAnimation();
                
            }
            facingPointAnimationStrategy.Animate(animationArgs);
            timer.Tick(animationArgs.Delta);
            if (timer.SecondsSinceStarted / timer.MaxSeconds >= 0.1)
                if (!hasInvokedAnimationAlmostEnding)
                    OnAnimationAlmostEnding?.Invoke(this);

        }
        private void setStateForAnimation() {
            
            facingPointAnimationStrategy.FirstPointOffset = Vector2D.FromPolar(
                (CalculateMovementAngle(timer.SecondsSinceStarted) + angleRange.Radians / 2) * 30,
                startingAngle);
            facingPointAnimationStrategy.DistanceOffsetTowardsPointer = 100 - (CalculateMovementAngle(timer.SecondsSinceStarted) + angleRange.Radians / 2) * 20;
        }
        private Vector2D nextPosition(Vector2D positionOnScreen) {
            currentAngle = startingAngle - Angle.FromRadians(CalculateMovementAngle(timer.SecondsSinceStarted));
            return Vector2D.FromPolar(200, currentAngle) + positionOnScreen;
        }
        private void initTimers(double seconds, double secondsAfterOver)
        {
            timer = new(seconds);
            timer.Elapsed += () => {
                isOver = true;
                timer = new(secondsAfterOver);
                timer.Elapsed += () =>
                OnAnimationCompleted?.Invoke(this);
            };
        }
        // Summary:
        //     The current angle is calculated by a sine function of time.
        //     The animation starts in one direction (charge-up of the swing),
        //     and gradually slows down as switches directions once, then the swing happens.
        //     The animation stops after the current angle reaches the first positive minimum
        //     of the function, so it only switches directions once
        //     the function for the angle is in A/2 * sin( (c(m)*x(t))^d ) form, where
        //     A is defined by the angle that the sword swing should have (angleRange)
        //     c(m) is the coefficient that dictates the speed of the animation, to make sure it ends in time
        //     m is the first positive local minimum of the sin(x(t)^d) function
        //     x is just a linear function of t: x = t*3/2*pi. This is because the sin function has the first positive minimum at 3/2
        //     d is the exponent which serves as a way to alter the speed distribution of the animation
        //     (higher d will result in the angle speed being slower when the sword is close to the side of the given angle range)
        //     
        //     
        // Parameters:
        //   t:
        //     the current time. Same as t in the upper formula
        //   sinExponent:
        //      same as d in the upper formula.
        //   fistMinimumX:
        //      same as m in the upper formula. The first positive local minimum of the sin( (t * 3/2 * pi) ^ sinExponent ) function.
        private double CalculateMovementAngle(double t, double sinExponent ,double firstMinimumX) {

            double oneAndAHalfPi = Math.PI * (3.0 / 2.0);
            double sinParam = Math.Pow(InnerSinCoefficient(firstMinimumX) * timer.SecondsSinceStarted * oneAndAHalfPi, sinExponent);
            return Math.Sin(sinParam) * angleRange.Radians/2;
        
        }
        // Summary:
        //     The default parameterization for CalculateMovementAngle(double t, double sinExponent, double firstMinimumX)
        private double CalculateMovementAngle(double t) {
            return CalculateMovementAngle(t,1.9, 0.4798398);
        }

        private double InnerSinCoefficient( double firstMinimumX)
        {
            return 1/(timer.MaxSeconds/ firstMinimumX);
        }

        public IImageRenderer GetConfiguredImageRenderer(DrawingArgs animationArgs)
        {
            throw new NotImplementedException();
        }
    }
}
