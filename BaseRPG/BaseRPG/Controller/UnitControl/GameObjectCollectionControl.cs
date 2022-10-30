
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View.EntityView;
using BaseRPG.View.WorldView;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace BaseRPG.Controller.UnitControl
{
    //TODO unit test this
    public class GameObjectCollectionControl
    {
        private object _lock = new object();
        private struct GameObjectData
        {

            private readonly FullGameObject2D fullGameObject;
            private readonly World world;
            private readonly WorldView worldView;
            private readonly CollisionNotifier2D collisionNotifier;
            private object syncObject;
            public object SyncObject => syncObject;
            public GameObjectData(FullGameObject2D fullGameObject, WorldView worldView, World world, CollisionNotifier2D collisionNotifier)
            {
                this.fullGameObject = fullGameObject;
                this.worldView = worldView;
                this.world = world;
                this.collisionNotifier = collisionNotifier;
                syncObject = new();

            }
            public void Add()
            {
                fullGameObject?.AddTo(worldView,world,collisionNotifier);
            }

        }
        private ConcurrentBag<GameObjectData> gameObjectsQueue = new ConcurrentBag<GameObjectData>();
        
        
        public void QueueForAdd(World world,WorldView worldView,CollisionNotifier2D collisionNotifier, FullGameObject2D fullGameObject)
        {
            GameObjectData gameObjectData = new GameObjectData(fullGameObject, worldView, world, collisionNotifier);
            lock (_lock) {
                gameObjectsQueue.Add(gameObjectData);
            }
            OnAddQueueCalled?.Invoke();

        }
        public void AddQueued() {
            lock (_lock)
            {
                foreach (var g in gameObjectsQueue)
                {
                    OnAddCalled?.Invoke();
                    g.Add();
                }
                gameObjectsQueue.Clear();
            }
        }
        public event Action OnAddCalled;
        public event Action OnAddQueueCalled;
        public int Count => gameObjectsQueue.Count;
    }
}
