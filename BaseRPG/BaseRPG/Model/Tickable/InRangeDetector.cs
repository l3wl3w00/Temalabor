using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable
{
    public class InRangeDetector : IGameObject, ICollisionDetector<IGameObject>
    {
        public event Action<ICollisionDetector<IGameObject>> OnInRange;
        public event Action<ICollisionDetector<IGameObject>> OnExitedRange;
        public event Action OnCeaseToExist;

        public List<ICollisionDetector<IGameObject>> objectsInRange = new();
        // Exists for the duration of its owner
        public bool Exists {get;set;}

        public void OnCollision(ICollisionDetector<IGameObject> gameObject)
        {
            if (!objectsInRange.Contains(gameObject))
            {
                gameObject.OnCeaseToExist += () => objectsInRange.Remove(gameObject);
                objectsInRange.Add(gameObject);
            }
            OnInRange?.Invoke(gameObject);
        }

        public void OnTick(double delta)
        {
            objectsInRange.RemoveAll(o => !o.Exists);
        }

        public void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
        public bool IsInRange(object obj) {
            return objectsInRange.Contains(obj);
        }

        public void OnCollisionExit(ICollisionDetector<IGameObject> gameObject)
        {
            objectsInRange.Remove(gameObject);
            OnExitedRange?.Invoke(gameObject);
        }
    }
}
