using System.Collections.Generic;
using Game.Core.Item;
using Game.Items.Components;

namespace Game.Items
{
    public class CubeItem : ItemBase
    {
        public override void Init(ItemData itemData, ItemUIData uiData)
        {
            base.Init(itemData, uiData);
            
            ChangeSprite(_itemData.LayerCount - 1);

            CanTapped = true;
            
            itemFallComponent.SetItem(this);
            ItemShufflerComponent = new CanShuffleItemComponent();
        }

        private void VisualizeHint(int value, List<int> hintThresholds)
        {
            var spriteIndex = 0;
            
            for (var i = 0; i < hintThresholds.Count; i++)
            {
                if (value >= hintThresholds[i])
                {
                    spriteIndex = i; 
                }
                else
                {
                    break;
                }
            }
            
            ChangeSprite(spriteIndex);
        }
        
        public override void ShowHint(int groupCount, List<int> hintThresholds)
        {
            VisualizeHint(groupCount, hintThresholds);
        }
    }
}
