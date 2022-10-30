using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.Interfaces;
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
        private IPositionProvider positionProvider;
        private Color color = Color.FromArgb(100, 255, 0, 0);
        public ShapeView(IShape2D shape, IPositionProvider positionProvider, Color? color = null)
        {
            this.shape = shape;
            this.positionProvider = positionProvider;
            if (color.HasValue)
                this.color = color.Value;
        }

        public bool Exists => shape.Owner.Exists;

        public Vector2D ObservedPosition => positionProvider.Position;

        public void OnRender(DrawingArgs drawingArgs)
        {
            IEnumerable<Point2D> vertices = shape.ToPolygon2D().Vertices;
            Vector2[] verticesArray = vertices.Select(v => new Vector2((float)v.X, (float)v.Y)).ToArray();
            
            drawingArgs.DrawingSession.FillGeometry(
                CanvasGeometry.CreatePolygon(drawingArgs.Sender, verticesArray),
                new Vector2(
                    (float)(drawingArgs.PositionOnScreen.X ),
                    (float)(drawingArgs.PositionOnScreen.Y )),
                color);
        }
    }
}
