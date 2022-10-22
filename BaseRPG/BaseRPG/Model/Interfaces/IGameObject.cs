using System.Collections.Generic;


namespace BaseRPG.Model.Interfaces
{
    public interface IGameObject : ITickable, ISeparable
    {
        bool Exists { get; }
        void OnCollision(IGameObject gameObject);
        virtual void OnCollisionExit(IGameObject gameObject) {
        }
    }
}
