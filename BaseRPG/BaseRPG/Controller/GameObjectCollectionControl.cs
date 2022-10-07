using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.EntityView;
using BaseRPG.View.WorldView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    public class GameObjectCollectionControl
    {
        private struct GameObjectData {
            public IGameObject GameObject { get; }
            public IDrawable View { get; }
            public World World { get; }
            public WorldView WorldView { get; }

            public GameObjectData(IGameObject gameObject, IDrawable drawable, WorldView worldView, World world)
            {
                this.GameObject = gameObject;
                this.View = drawable;
                this.WorldView = worldView;
                this.World = world;
            }
        }

        private IEnumerable<GameObjectData> gameObjectsQueue = new ReadOnlyCollection<GameObjectData>(new List<GameObjectData>());

        
        public void QueueForAdd(World world,WorldView worldView,IGameObject gameObject, IDrawable drawable) {
            gameObjectsQueue = gameObjectsQueue.Append(new GameObjectData(gameObject, drawable, worldView, world));
        }

        public void AddQueued() {
            
            lock (gameObjectsQueue) {
                foreach (GameObjectData g in gameObjectsQueue)
                {
                    IGameObject gameObject = g.GameObject;
                    IDrawable drawable = g.View;
                    World world = g.World;
                    WorldView worldView = g.WorldView;

                    world.Add(gameObject);
                    worldView.AddView(drawable);
                }
                gameObjectsQueue = new ReadOnlyCollection<GameObjectData>(new List<GameObjectData>());
            }
            
            
        }
    }
}
