using BaseRPG.View.Interfaces;
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
        public ImageSequenceAnimation(IImageProvider imageProvider, IEnumerator<string> imageNames, double timeBetween = 0.1)
        {
            
            this.imageProvider = imageProvider;
            this.imageNames = imageNames;
            animationTimer = new AnimationTimer(timeBetween,true);
            animationTimer.Elapsed += 
            ()=>
            {
                bool moved = imageNames.MoveNext();
                if (moved) lastValidImage = imageNames.Current;
                else { imageNames.Reset(); imageNames.MoveNext(); OnAnimationCompleted?.Invoke(this); }
            };
        }

        public ICanvasImage CalculateImage(double delta)
        {
            animationTimer.Tick(delta);
            if (imageNames.Current == null)
                return imageProvider.GetByFilename(lastValidImage);
            return imageProvider.GetByFilename(imageNames.Current);
        }

        public static ImageSequenceAnimation SingleImage(IImageProvider imageProvider, string image, double timeBetween = 0.1) {

            return new ImageSequenceAnimation(imageProvider, new LoopingEnumerator<string>(new List<string> { image }), timeBetween);
        }

    }
}
