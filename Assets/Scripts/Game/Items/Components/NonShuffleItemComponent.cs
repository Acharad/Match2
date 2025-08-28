namespace Game.Items.Components
{
    public class NonShuffleItemComponent : ItemShufflerComponent
    {
        public override bool CanShuffle()
        {
            return false;
        }
    }
}