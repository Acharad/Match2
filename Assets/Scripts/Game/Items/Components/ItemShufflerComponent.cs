namespace Game.Items.Components
{
    public class ItemShufflerComponent : IItemShuffleComponent
    {
        public virtual bool CanShuffle()
        {
            return true;
        }
    }
}