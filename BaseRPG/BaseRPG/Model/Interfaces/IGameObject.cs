using System.Collections.Generic;


namespace BaseRPG.Model.Interfaces
{
    public interface IGameObject : ITickable
    {
        // Puts itself in the appropriate list defined by the key of the dictionary
        public abstract void Separate(Dictionary<string, List<IGameObject>> dict);
    }
}
