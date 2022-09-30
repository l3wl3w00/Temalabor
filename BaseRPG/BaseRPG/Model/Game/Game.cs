using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;


namespace BaseRPG.Model.Game
{
    public class Game
    {
        private WorldCatalogue worldCatalogue;
        private World currentWorld;
        private ItemCatalogue itemCatalogue = new ItemCatalogue();
        private IPhysicsFactory physicsFactory;
        public Game(IPhysicsFactory physicsFactory) {
            this.physicsFactory = physicsFactory;
            worldCatalogue = new WorldCatalogue(physicsFactory);
        }

        public World CurrentWorld { get { return currentWorld; } }
        public Hero Hero { get {
                return currentWorld.Hero;
            }
        }

        public IPhysicsFactory PhysicsFactory { get { return physicsFactory; } }
        public void Initialize() {
            
            itemCatalogue.Initialize();
            worldCatalogue.Initialize();
            currentWorld = worldCatalogue["Empty"].Create(); ;
        }

    //    public static class Singleton
    //    {
    //        private static Game instance;
    //        public static Game Instance { get{
    //                if (instance == null)
    //                {
    //                    instance = new Game();
    //                    instance.Initialize();
    //                }
    //                return instance;
    //            } 
    //        }
    //    }
    }
}
