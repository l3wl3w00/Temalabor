using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollectible
    {
        void OnCollect<T>(ICollector<T> collector)where T : ICollectible;
    }
}
