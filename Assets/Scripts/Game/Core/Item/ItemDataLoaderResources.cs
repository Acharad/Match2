using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Item
{
    public class ItemDataLoaderResources : IItemUIDataLoader
    {
        private Dictionary<string, ItemUIData> _itemsByName;
        private Dictionary<ItemType, ItemBase> _prefabsByType;
        
        public ItemDataLoaderResources()
        {
            LoadAllItems();
        }
        
        private void LoadAllItems()
        {
            _itemsByName = new Dictionary<string, ItemUIData>();
            _prefabsByType = new Dictionary<ItemType, ItemBase>();

            var allItems = Resources.LoadAll<ItemUIData>("Items");

            foreach (var item in allItems)
            {
                _itemsByName[item.name] = item;
                
                var parts = item.name.Split('_');
                if (parts.Length < 3) continue;
                if (!Enum.TryParse(parts[2], out ItemType parsedType)) continue;

                _prefabsByType.TryAdd(parsedType, item.ItemBase);
            }

            Debug.Log($"{nameof(ItemDataLoaderResources)} Loaded {allItems.Length} item(s).");
        }
        
        public bool TryGetItemUIData(ItemData itemData, out ItemUIData itemUIData)
        {
            var itemFileName = $"SO_ItemUIData_{itemData.ItemType}_{itemData.Color}";

            if (_itemsByName.TryGetValue(itemFileName, out itemUIData))
                return true;

            Debug.LogWarning($"{nameof(ItemDataLoaderResources)} Not found by ItemData: {itemFileName}");
            return false;
        }
        
        public bool TryGetPrefab(ItemType type, out ItemBase itemBase)
        {
            if (_prefabsByType.TryGetValue(type, out itemBase))
                return true;
            
            Debug.LogWarning($"{nameof(ItemDataLoaderResources)} Prefab not found by ItemType: {type}");
            return false;
        }
    }
}
