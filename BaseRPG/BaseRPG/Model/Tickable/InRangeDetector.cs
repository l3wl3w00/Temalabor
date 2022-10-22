using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable
{
    public class InRangeDetector : IGameObject
    {
        public event Action<IGameObject> OnInRange;
        public event Action<IGameObject> OnExitedRange;
        public GameObjectContainer gameObjectsInRange = new();
        // Exists for the duration of its owner
        public bool Exists {get;set;}

        public void OnCollision(IGameObject gameObject)
        {
            gameObjectsInRange.Add(gameObject);
            OnInRange?.Invoke(gameObject);
        }
        void IGameObject.OnCollisionExit(IGameObject gameObject)
        {
            gameObjectsInRange.Remove(gameObject);
            OnExitedRange?.Invoke(gameObject);
        }

        public void OnTick(double delta)
        {
            
        }

        public void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
