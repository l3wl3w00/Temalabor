using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IAnimationStrategy
    {
        event Action<IAnimationStrategy> OnAnimationCompleted;
        Transform2DEffect GetImage(DrawingArgs animationArgs, Matrix3x2 initialMatrix = new());
    }
}
