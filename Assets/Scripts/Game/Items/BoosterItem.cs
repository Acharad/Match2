using Game.Core.Item;
using Game.Items.Components;
using Game.Items.Components.ExplodeComponent;

namespace Game.Items
{
    public class BoosterItem : ItemBase
    {
        public override void Init(ItemData itemData, ItemUIData uiData)
        {
            base.Init(itemData, uiData);
            
            ChangeSprite(_itemData.LayerCount - 1);
            
            CanTapped = true;
        }

        public override void InitComponents()
        {
            itemFallComponent.SetItem(this);
            AddComponent<IItemShuffleComponent>(new CanShuffleItemComponent());
        }

        public override void TryExecute()
        {
            //todo
            base.TryExecute();
            TryGetItemComponent<IItemExplodeComponent>().TryExplode();
        }


        public override MatchType GetMatchType()
        {
            return MatchType.Booster;
        }
    }
}