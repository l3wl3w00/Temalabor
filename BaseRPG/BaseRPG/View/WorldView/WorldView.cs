using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.EntityView;
using MathNet.Spatial.Euclidean;
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
        private Camera camera = new Camera(new Vector2D(0,0), 100 );
        private List<GameObjectView> drawables = new List<GameObjectView>();
        public WorldView(World world)
        {
            this.world = world;
            foreach (var enemy in world.GameObjectContainer.Enemies) {
                AddView(new EnemyView(enemy));
            }
            drawables.Add(new HeroView(world.Hero));
        }
        public void AddView(GameObjectView gameObjectView) {
            drawables.Add(gameObjectView);
        }
        public void Render(CanvasDrawEventArgs args) {
            drawables.ForEach(d => d.Render(args,camera));
        }

    }
}
