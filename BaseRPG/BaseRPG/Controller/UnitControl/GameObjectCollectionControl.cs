
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View.EntityView;
using BaseRPG.View.WorldView;
using System;
using System.Collections.Generic;

namespace BaseRPG.Controller.UnitControl
{
    public class GameObjectCollectionControl
    {


        private struct GameObjectData
        {

            private readonly FullGameObject2D fullGameObject;
            private readonly World world;
            private readonly WorldView worldView;
            private readonly CollisionNotifier2D collisionNotifier;

            public GameObjectData(FullGameObject2D fullGameObject, WorldView worldView, World world, CollisionNotifier2D collisionNotifier)
            {
                this.fullGameObject = fullGameObject;
                this.worldView = worldView;
                this.world = world;
                this.collisionNotifier = collisionNotifier;

            }
            public void Add()
            {
                fullGameObject.AddTo(worldView,world,collisionNotifier);

            }

        }
        private List<GameObjectData> gameObjectsQueue = new List<GameObjectData>();
        
        
        public void QueueForAdd(World world,WorldView worldView,CollisionNotifier2D collisionNotifier, FullGameObject2D fullGameObject)
        {
            lock (gameObjectsQueue) {
                GameObjectData gameObjectData = new GameObjectData(fullGameObject, worldView, world, collisionNotifier);
                gameObjectsQueue.Add(gameObjectData);
            }
            
        }

        public void AddQueued() {
            lock (gameObjectsQueue) {
                gameObjectsQueue.ForEach( g => g.Add() );
                gameObjectsQueue.Clear();
            }
        }
    }
}
