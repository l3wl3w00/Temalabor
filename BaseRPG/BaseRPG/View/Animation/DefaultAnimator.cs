using BaseRPG.Controller.Utility;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace BaseRPG.View.Animation
{
    public class DefaultAnimator : Animator
    {

        private AnimationLifeCycle<TransformationAnimation2D> transformationAnimation;
        private AnimationLifeCycle<ImageSequenceAnimation> sequenceAnimation;
        private readonly bool cancelAnimations;

        public DefaultAnimator(
            TransformationAnimation2D defaultTransformAnimation,
            ImageSequenceAnimation defaultImageSequenceAnimation,
            bool cancelAnimations = false)
        {
            transformationAnimation = new(defaultTransformAnimation);
            sequenceAnimation = new(defaultImageSequenceAnimation);
            this.cancelAnimations = cancelAnimations;
            Start(defaultTransformAnimation);
            Start(defaultImageSequenceAnimation);
        }
        public override void Animate(DrawingArgs animationArgs)
        {
            transformationAnimation.ResetIfQueued();
            sequenceAnimation.ResetIfQueued();

            Transform2DEffect transform2DEffect = new Transform2DEffect 
            {
                Source = sequenceAnimation.CurrentAnimation.CalculateImage(animationArgs.Delta),
                TransformMatrix = transformationAnimation.CurrentAnimation.GetImage(animationArgs) 
            };

            animationArgs.DrawingSession.DrawImage(transform2DEffect
                , (float)(animationArgs.PositionOnScreen.X)
                , (float)(animationArgs.PositionOnScreen.Y));
        }
        public override Tuple<double, double> Size
        {
            get
            {
                var scaleFactor = transformationAnimation.CurrentAnimation.LastTransformation.GetDeterminant();
                return new(
                    sequenceAnimation.CurrentAnimation.CurrentImageSize.Item1* scaleFactor,
                    sequenceAnimation.CurrentAnimation.CurrentImageSize.Item2* scaleFactor);
            }
        }
        public override void Start(TransformationAnimation2D animation) =>
            transformationAnimation.Start(animation, cancelAnimations);

        public override void ResetTransformation() =>
            transformationAnimation.Reset();

        public override void Start(ImageSequenceAnimation animation) =>
            sequenceAnimation.Start(animation, cancelAnimations);

        public override void ResetImageSequence() =>
            sequenceAnimation.Reset();
    }
}
