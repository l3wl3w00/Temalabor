using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
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
        public CanvasVirtualControl Sender { get; }
        public CanvasRegionsInvalidatedEventArgs Args { get; }
        public Vector2D PositionOnScreen { get; set; }
        public double Delta { get; }
        public Vector2D MousePositionOnScreen { get; }
        public CanvasDrawingSession DrawingSession { get; }

        public DrawingArgs(CanvasVirtualControl sender, 
            CanvasRegionsInvalidatedEventArgs args,
            double delta, Vector2D mousePositionOnScreen,
            CanvasDrawingSession drawingSession) :
            this(sender, args, delta, new Vector2D(0, 0), mousePositionOnScreen, drawingSession)
        {
        }
        public DrawingArgs(CanvasVirtualControl sender, 
            CanvasRegionsInvalidatedEventArgs args,
            double delta, Vector2D positionOnScreen,
            Vector2D mousePositionOnScreen, 
            CanvasDrawingSession drawingSession)
        {
            Sender = sender;
            Args = args;
            Delta = delta;
            PositionOnScreen = positionOnScreen;
            MousePositionOnScreen = mousePositionOnScreen;
            DrawingSession = drawingSession;
        }

        internal DrawingArgs Copy()
        {
            return new(Sender,Args,Delta,PositionOnScreen,MousePositionOnScreen, DrawingSession);
        }
    }
}
