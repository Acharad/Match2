namespace Game.Core.Item
{
    public interface IItemUIDataLoader
    {
        public bool TryGetItemUIData(ItemData itemData, out ItemUIData itemUIData);
        public bool TryGetPrefab(ItemType type, out ItemBase itemBase);
    }
}