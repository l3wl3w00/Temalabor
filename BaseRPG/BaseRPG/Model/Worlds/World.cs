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
        private IWorldInitializationStrategy initializationStrategy;
        
        public Hero Hero { get { return gameObjectContainer.Hero; } }
        public GameObjectContainer GameObjectContainer { get { return gameObjectContainer; } }

        public World(IWorldInitializationStrategy initializationStrategy)
        {
            this.initializationStrategy = initializationStrategy;
        }

        public void Initialize() {
            initializationStrategy.Initialize(gameObjectContainer);
        }

        public void OnTick()
        {
            foreach (IGameObject t in gameObjectContainer.All) 
                t.OnTick();
        }
        public void Add(IGameObject gameObject) {
            gameObjectContainer.Add(gameObject);
        }
        public void Remove(IGameObject gameObject) {
            gameObjectContainer.Remove(gameObject);
        }

    }
}
