using System.Collections.Generic;


namespace BaseRPG.Model.Interfaces
{
    public interface IGameObject : ITickable
    {
        void OnCollision(IGameObject gameObject) { }
        // Puts itself in the appropriate list defined by the key of the dictionary
        abstract void Separate(Dictionary<string, List<IGameObject>> dict);
    }
}
