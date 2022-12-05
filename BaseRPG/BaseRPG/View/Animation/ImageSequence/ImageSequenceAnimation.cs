using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.ImageSequence
{
    public class ImageSequenceAnimation:IAnimation<ImageSequenceAnimation>
    {
        private IImageProvider imageProvider;
        private IEnumerator<string> imageNames;
        private AnimationTimer animationTimer;
        private string lastValidImage;
        public Tuple<double, double> CurrentImageSize { get => imageProvider.GetSizeByFilename(imageNames.Current); }

        public event Action<ImageSequenceAnimation> OnAnimationCompleted;
        public ImageSequenceAnimation(
            IImageProvider imageProvider, 
            IEnumerator<string> imageNames,
            Action<ImageSequenceAnimation> actionOnAnimationCompleted = null,
            double timeBetween = 0.1)
        {
            
            this.imageProvider = imageProvider;
            this.imageNames = imageNames;
            OnAnimationCompleted += actionOnAnimationCompleted;
            TimeBetween = timeBetween;
        }

        public double TimeBetween
        {
            set 
            {
                animationTimer = new AnimationTimer(value, true);
                animationTimer.Elapsed +=
                () =>
                {
                    bool moved = imageNames.MoveNext();
                    if (moved)
                        lastValidImage = imageNames.Current;
                    else
                    {
                        imageNames.Reset();
                        imageNames.MoveNext();
                        OnAnimationCompleted?.Invoke(this);
                    }
                };
            }
        }

        public ICanvasImage CalculateImage(double delta)
        {
            animationTimer.Tick(delta);
            if (imageNames.Current == null)
                return imageProvider.GetByFilename(lastValidImage);
            return imageProvider.GetByFilename(imageNames.Current);
        }

        public static ImageSequenceAnimation SingleImage(IImageProvider imageProvider, string image, double timeBetween = 0.1) {
            return new ImageSequenceAnimation(imageProvider, new LoopingEnumerator<string>(new List<string> { image }),null, timeBetween);
        }
        public static ImageSequenceAnimation LoopingAnimation(IImageProvider imageProvider, List<string> images, double timeBetween = 0.1)
        {
            return new ImageSequenceAnimation(imageProvider, new LoopingEnumerator<string>(images), null, timeBetween);
        }
        
        public static ImageSequenceAnimation WithDefaultEnumerator(IImageProvider imageProvider, List<string> images, double timeBetween = 0.1)
        {
            List<string>.Enumerator enumerator = images.GetEnumerator();
            enumerator.MoveNext();
            return new ImageSequenceAnimation(imageProvider, enumerator, null, timeBetween);
        }
        public static ImageSequenceAnimation WithTimeFrame(IImageProvider imageProvider, List<string> images, double timeFrame)
        {
            List<string>.Enumerator enumerator = images.GetEnumerator();
            enumerator.MoveNext();
            var timeBetween = timeFrame / (images.Count);
            return new ImageSequenceAnimation(imageProvider, enumerator, null, timeBetween);
        }

        internal static ImageSequenceAnimation SingleImage(DrawingImage image)
        {
            return new ImageSequenceAnimation(image.ImageProvider, new LoopingEnumerator<string>(new List<string> { image.ImageName }), null, 0.1);

        }
    }
}
