using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Game
{
    public class Game
    {
        private Game() { }
        private static List<World> worlds;
        private World currentWorld;
        public Hero Hero { get {
                return currentWorld.Hero;
            }
        }

        public void Initialize() { 
            
        }
        public static class Singleton
        {
            private static Game instance;
            public static Game Instance
            {
                get
                {
                    if (instance == null) instance = new Game();
                    return instance;
                }
            }
        }
    }
}
