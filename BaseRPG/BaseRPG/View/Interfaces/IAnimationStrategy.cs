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
    public interface IAnimationStrategy
    {
        event Action<IAnimationStrategy> OnAnimationCompleted;
        void Animate(DrawingArgs animationArgs);
    }
}
