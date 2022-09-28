using BaseRPG.Model.Data;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;


namespace BaseRPG.Model.Game
{
    public class Game
    {
        private WorldCatalogue worldCatalogue = new WorldCatalogue();
        private World currentWorld;
        private ItemCatalogue itemCatalogue = new ItemCatalogue();

        private Game(){}

        public World CurrentWorld { get { return currentWorld; } }
        public Hero Hero { get {
                return currentWorld.Hero;
            }
        }

        private void Initialize() {
            itemCatalogue.Initialize();
            worldCatalogue.Initialize();

            currentWorld = worldCatalogue["Empty"].Create(); ;
            currentWorld.Initialize();
        }

        public static class Singleton
        {
            private static Game instance;
            public static Game Instance { get{
                    if (instance == null)
                    {
                        instance = new Game();
                        instance.Initialize();
                    }
                    return instance;
                } 
            }
        }
    }
}
