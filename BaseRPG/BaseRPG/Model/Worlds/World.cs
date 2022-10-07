using BaseRPG.Model.Data;
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
        //private IWorldInitializationStrategy initializationStrategy;
        
        public Hero Hero { get { return gameObjectContainer.Hero; } set { gameObjectContainer.Hero = value; } }
        public GameObjectContainer GameObjectContainer { get { return gameObjectContainer; } }

        public World(GameObjectContainer gameObjectContainer)
        {
            this.gameObjectContainer = gameObjectContainer;
        }

        //public void Initialize() {
        //    initializationStrategy.Initialize(gameObjectContainer);
        //}

        public void OnTick()
        {
            List<IGameObject> all = gameObjectContainer.All;
            lock (all) {
                for (int i = 0; i < all.Count; i++) {
                    all[i].OnTick();
                }
                all.RemoveAll(g => !g.Exists);
            }
           
            
                
        }
        public void Add(IGameObject gameObject) {
            gameObjectContainer.Add(gameObject);
        }
        public void Remove(IGameObject gameObject) {
            gameObjectContainer.Remove(gameObject);
        }

    }
}
