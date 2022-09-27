
namespace BaseRPG.Model.Interfaces
{
    public interface ICollector
    {
        public sealed void Collect(ICollectible collectible)
        {
            this.OnCollect(collectible);
            collectible.OnCollect(this);
        }

        public void OnCollect(ICollectible collectible);
    }
}
