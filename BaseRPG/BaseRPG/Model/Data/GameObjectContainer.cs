using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class GameObjectContainer
    {
        private List<IGameObject> gameObjects = new List<IGameObject>();
        public List<IGameObject> All { get { return gameObjects; } }
        public List<Enemy> Enemies { get { return GetGameObjects<Enemy>("Enemy"); } }

        private Hero hero;
        public Hero Hero {
            get { return hero; }
            set {
                Remove(hero);
                hero = value;
                Add(hero);
            }
        }

        public List<T> GetGameObjects<T>(string name)
        {
            Dictionary<string, List<IGameObject>> dict = new Dictionary<string, List<IGameObject>>();
            All.ForEach(g => g.Separate(dict));
            if (!dict.ContainsKey(name)) return new List<T>();
            return dict[name].Select(e => (T)e).ToList();
        }
        public void Add(IGameObject gameObject) {
            gameObjects.Add(gameObject);
        }
        public void Remove(IGameObject gameObject){
            gameObjects.Remove(gameObject);
        }
    }
}
