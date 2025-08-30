using Game.Core.Item;
using Game.Items.Components;

namespace Game.Items
{
    public class BoxItem : ItemBase
    {
        public override void Init(ItemData itemData, ItemUIData uiData)
        {
            base.Init(itemData , uiData);

            ChangeSprite(_itemData.LayerCount - 1);
            CanFall = false;
            
            
        }

        public override void InitComponents()
        {
            AddComponent<IItemShuffleComponent>(new NonShuffleItemComponent());
        }

        public override void TryExecute()
        {
            _itemData.LayerCount--;
            if (_itemData.LayerCount <= 0)
                DestroyItem();
            else
                ChangeSprite(_itemData.LayerCount - 1);
        }

        public override void TryExecuteByNearMatch()
        {
            TryExecute();
        }
    }
}