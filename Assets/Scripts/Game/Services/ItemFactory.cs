using System;
using Game.Core.Item;
using UnityEngine;
using Game.Services.Pool;

namespace Game.Services
{
    public class ItemFactory
    {
        private IItemUIDataLoader _itemUIDataLoader;
        
        private readonly ItemPoolCreator _itemPoolCreator;

        public event Action<ItemBase> OnItemCreated;
        
        public ItemFactory(IItemUIDataLoader itemUIDataLoader, ItemPoolCreator itemPoolCreator)
        {
            _itemUIDataLoader = itemUIDataLoader;

            _itemPoolCreator = itemPoolCreator;
        }
        
        
        public ItemBase CreateItem(ItemData itemData, Transform parentTransform)
        {
            if (itemData.ItemType == ItemType.None)
            {
                return null;
            }

            var result = _itemUIDataLoader.TryGetItemUIData(itemData , out var itemUIData);

            if (!result || itemUIData == null)
            {
                Debug.LogError("ItemFactory | Given item data doesnt contains in ui data " + itemData);
                return null;
            }

            var itemPool = _itemPoolCreator.GetOrCreatePool(itemData.ItemType);
            
            
            var itemBase = itemPool.Get();
            
            itemBase.transform.SetParent(parentTransform);
            itemBase.Init(itemData, itemUIData);
            
            OnItemCreated?.Invoke(itemBase);
            return itemBase;
        }
    }
}