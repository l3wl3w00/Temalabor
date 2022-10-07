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
        public DrawingArgs(CanvasControl sender, CanvasDrawEventArgs args, double delta) :
            this(sender, args, delta, new Vector2D(0,0))
        {
            
        }
        public DrawingArgs(CanvasControl sender, CanvasDrawEventArgs args, double delta, Vector2D positionOnScreen)
        {
            Sender = sender;
            Args = args;
            Delta = delta;
            PositionOnScreen = positionOnScreen;
        }

        internal DrawingArgs Copy()
        {
            return new(Sender,Args,Delta,PositionOnScreen);
        }
    }
}
