using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace BaseRPG.View.EntityView
{
    public class ShapeView:IDrawable
    {
        private IShape2D shape;
        private IDrawable drawable;

        public ShapeView(IShape2D shape, IDrawable drawable)
        {
            this.shape = shape;
            this.drawable = drawable;
        }

        public bool Exists => drawable.Exists;

        public Vector2D ObservedPosition => drawable.ObservedPosition;

        public void OnRender(DrawingArgs drawingArgs)
        {
            IEnumerable<Point2D> vertices = shape.ToPolygon().Vertices;
            Vector2[] verticesArray = vertices.Select(v => new Vector2((float)v.X, (float)v.Y)).ToArray();
            
            drawingArgs.Args.DrawingSession.FillGeometry(
                CanvasGeometry.CreatePolygon(drawingArgs.Sender, verticesArray),
                new Vector2(
                    (float)(drawingArgs.PositionOnScreen.X ),
                    (float)(drawingArgs.PositionOnScreen.Y )),
                Color.FromArgb(100,255,0,0));
        }
    }
}
