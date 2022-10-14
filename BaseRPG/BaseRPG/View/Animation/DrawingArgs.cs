using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class DrawingArgs
    {
        public CanvasControl Sender { get; }
        public CanvasDrawEventArgs Args { get; }
        public Vector2D PositionOnScreen { get; set; }
        public double Delta { get; }
        public Vector2D MousePositionOnScreen { get; }
        public DrawingArgs(CanvasControl sender, CanvasDrawEventArgs args, double delta, Vector2D mousePositionOnScreen) :
            this(sender, args, delta, new Vector2D(0, 0), mousePositionOnScreen)
        {
            
        }
        public DrawingArgs(CanvasControl sender, CanvasDrawEventArgs args, double delta, Vector2D positionOnScreen,Vector2D mousePositionOnScreen)
        {
            Sender = sender;
            Args = args;
            Delta = delta;
            PositionOnScreen = positionOnScreen;
            MousePositionOnScreen = mousePositionOnScreen;
        }

        internal DrawingArgs Copy()
        {
            return new(Sender,Args,Delta,PositionOnScreen,MousePositionOnScreen);
        }
    }
}
