using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollector
    {
        void OnCollect(ICollectible collectible) { 
            InteractionFactory.Instance
                .CreateCollectInteraction(this, collectible)
                .Collect();
        }
        void Collect(ICollectible collectible);
    }
}
