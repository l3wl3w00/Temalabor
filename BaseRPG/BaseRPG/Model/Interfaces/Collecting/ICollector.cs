namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollector
    {
        public sealed void Collect(ICollectible collectible)
        {
            OnCollect(collectible);
            collectible.OnCollect(this);
        }

        public void OnCollect(ICollectible collectible);
    }
}
