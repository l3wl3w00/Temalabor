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
        private WorldCatalogue worldCatalogue = new();
        private World currentWorld;
        private ItemCatalogue itemCatalogue = new();
        public event Action<string,World> CurrentWorldChanged;
        public Game() {

        }

        public World CurrentWorld { get { return currentWorld; } set { currentWorld = value; } }
        public Hero Hero { get {
                return currentWorld.Hero;
            }
            set {
                currentWorld.Hero = value;
            }
        }

        public WorldCatalogue WorldCatalogue { get => worldCatalogue;  }
        public ItemCatalogue ItemCatalogue { get => itemCatalogue; }

        public void ChangeWorld(string name) {
            CurrentWorld = worldCatalogue[name].Create();
            CurrentWorldChanged?.Invoke(name, CurrentWorld);
        }

    }
}
