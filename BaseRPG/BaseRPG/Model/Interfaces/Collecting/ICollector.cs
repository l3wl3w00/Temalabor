using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollector<T> where T : ICollectible
    {
        void Collect(T collectible);
    }
}
