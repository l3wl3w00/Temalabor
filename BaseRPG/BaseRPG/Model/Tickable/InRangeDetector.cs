using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable
{
    public class InRangeDetector : GameObject, ICollisionDetector<GameObject>
    {
        public event Action<ICollisionDetector<GameObject>> OnInRange;
        public event Action<ICollisionDetector<GameObject>> OnExitedRange;
        public override event Action OnCeaseToExist;

        public List<ICollisionDetector<GameObject>> objectsInRange = new();

        public InRangeDetector(World currentWorld) : base(currentWorld)
        {
        }

        private bool exists = true;
        // Exists for the duration of its owner
        public override bool Exists { get { return exists; }}
        public void SetExists(bool value) {
            exists = value;
        }
        public void OnCollision(ICollisionDetector<GameObject> gameObject, double delta)
        {
            if (!objectsInRange.Contains(gameObject))
            {
                gameObject.OnCeaseToExist += () => objectsInRange.Remove(gameObject);
                objectsInRange.Add(gameObject);
            }
            OnInRange?.Invoke(gameObject);
        }

        public override void Step(double delta)
        {
            objectsInRange.RemoveAll(o => !o.Exists);
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
        public bool IsInRange(object obj) {
            return objectsInRange.Contains(obj);
        }

        public void OnCollisionExit(ICollisionDetector<GameObject> gameObject)
        {
            objectsInRange.Remove(gameObject);
            OnExitedRange?.Invoke(gameObject);
        }
    }
}
