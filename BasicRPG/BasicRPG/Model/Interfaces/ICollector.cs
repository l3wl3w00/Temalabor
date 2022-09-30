
namespace BasicRPG.Model.Interfaces
{
    public interface ICollector
    {
        sealed void Collect(ICollectible collectible)
        {
            this.OnCollect(collectible);
            collectible.OnCollect(this);
        }

        void OnCollect(ICollectible collectible);
    }
}
