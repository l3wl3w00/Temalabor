using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.WorldView
{
    public class WorldView
    {
        private World world;
        private IImageRenderer backgroundImageRenderer;
        private Camera2D camera;
        private List<IDrawable> drawables = new List<IDrawable>();
        public WorldView(World world, ICanvasImage background, Tuple<double, double> sizeOfImage, Camera2D camera)
        {
            this.world = world;
            this.backgroundImageRenderer = new DefaultImageRenderer(background, sizeOfImage);
            this.camera = camera;
        }

        public Camera2D CurrentCamera { get { return camera; } set { camera = value; } }

        public void AddView(IDrawable gameObjectView) {
            lock (drawables) {
                drawables.Add(gameObjectView);
            }
            
        }
        public void Render(DrawingArgs drawingArgs) {
            var backgoundPos = camera.CalculatePositionOnScreen(new(0,0));
            drawingArgs.PositionOnScreen = backgoundPos;
            backgroundImageRenderer.Render(drawingArgs);
            lock (drawables) {
                drawables.ForEach(d => d.Render(drawingArgs, camera));
                drawables.RemoveAll(d => !d.Exists);
            }
            
        }

    }
}
