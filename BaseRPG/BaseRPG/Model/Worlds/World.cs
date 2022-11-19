﻿using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class World : ITickable
    {
        private GameObjectContainer gameObjectContainer = new GameObjectContainer();
        private CallbackQueue callbackQueue = new();
        //private IWorldInitializationStrategy initializationStrategy;
        
        public Hero Hero { get { return GameObjectContainer.Hero; } set { GameObjectContainer.Hero = value; } }

        public GameObjectContainer GameObjectContainer { get => gameObjectContainer; set => gameObjectContainer = value; }

        public World(GameObjectContainer gameObjectContainer)
        {
            this.GameObjectContainer = gameObjectContainer;
        }

        //public void Initialize() {
        //    initializationStrategy.Initialize(gameObjectContainer);
        //}

        public void OnTick(double delta)
        {
            callbackQueue.ExecuteAll();
            List<GameObject> all = GameObjectContainer.All;
            lock (all) {
                for (int i = 0; i < all.Count; i++) {
                    GameObject gameObject = all[i];
                    gameObject.OnTick(delta);
                }
                all.RemoveAll(g => !g.Exists);
            }
        }
        public void Add(GameObject gameObject) {
            GameObjectContainer.Add(gameObject);
        }
        public void Remove(GameObject gameObject) {
            GameObjectContainer.Remove(gameObject);
        }
        public void QueueForAdd(GameObject gameObject) {
            callbackQueue.QueueAction(()=>Add(gameObject));
        }
    }
}
