using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{   
    public class ExperienceManager
    {
        public event Action<int> LevelUp;
        private int experienceUntilNextLevel = 10;
        private int level = 1;

        public ExperienceManager(){
            this.LevelUp += OnLevelUp;
        }

        public int Level { get;}
        public int Experience {
            set { 
                experienceUntilNextLevel -= value;
                if (experienceUntilNextLevel < 0) {
                    level += 1;
                    LevelUp(level);
                }
            }
        }

        public void OnLevelUp(int newLevel) {
            experienceUntilNextLevel = newLevel*10;
        }
    }
}
