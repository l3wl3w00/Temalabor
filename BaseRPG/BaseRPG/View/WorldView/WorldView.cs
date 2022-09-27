using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.EntityView;
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
        private List<TickableView> drawables;
        public WorldView(World world)
        {
            this.world = world;
            foreach (ITickable tickable in this.world.Tickables) {
                drawables.Add(new TickableView(tickable));
            }
        }
        public void Render() {
            drawables.ForEach(d => d.Render());
        }
    }
}
