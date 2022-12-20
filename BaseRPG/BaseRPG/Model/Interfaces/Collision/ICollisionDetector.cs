using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity;
using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collision
{
    public interface ICollisionDetector:IExisting
    {
        virtual void OnCollision(ICollisionDetector other, double delta) {
            InteractionFactory.Instance.CreateCollisionInteraction(this, other).OnCollide(delta);
        }
        virtual void OnCollisionExit(ICollisionDetector gameObject) 
        {
        
        }
        bool CanCollide(ICollisionDetector other);
        bool CanBeOver { get => true; }
        void SeletBySkillTargetability(LinkedList<Unit> targetableUnits, LinkedList<ICollisionDetector> targetableOther) { }
        void SeletByInteractionTargetability(LinkedList<GameObject> targetableGameObjects, LinkedList<ICollisionDetector> targetableOther) { }
    }
}
