using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.EntityView;
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
        private IImageProvider imageProvider;
        private ICanvasImage background;
        private Camera camera = new Camera(new Vector2D(0,0), 100 );
        private List<Drawable> drawables = new List<Drawable>();
        public WorldView(World world,IImageProvider imageProvider)
        {
            this.world = world;
            this.imageProvider = imageProvider;
            foreach (var enemy in world.GameObjectContainer.Enemies) {
                AddView(new EnemyView(enemy));
            }
            drawables.Add(new HeroView(world.Hero, imageProvider.GetByFilename("character1")));
            background = imageProvider.GetByFilename("background");
        }
        public void AddView(Drawable gameObjectView) {
            drawables.Add(gameObjectView);
        }
        public void Render(CanvasControl sender, CanvasDrawEventArgs args) {
            args.DrawingSession.DrawImage(background);
            drawables.ForEach(d => d.Render(args,camera,sender));
        }

    }
}
