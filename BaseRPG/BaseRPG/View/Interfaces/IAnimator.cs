using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IAnimator
    {
        
        void Start(IAnimationStrategy animationStrategy);
        void Reset();
        void Animate(DrawingArgs animationArgs);

    }
}
