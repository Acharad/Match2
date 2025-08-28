using System.Collections.Generic;
using Game.Core.Item;
using Game.Core.Level;
using Game.Managers;
using UnityEngine;

namespace Game.Services.Pool
{
    public class ItemPoolCreator
    {
        private LevelData _levelData;
        private IItemUIDataLoader _loader;

        private Dictionary<ItemType, ItemPoolContainer> _poolContainersDic = new();

        public void Init(LevelData levelData, IItemUIDataLoader loader)
        {
            _levelData = levelData;
            _loader = loader;
            
            CreateLevelItemPools();
        }
        
        private Dictionary<ItemType, int> FindDifferentItemsInLevel()
        {
            var levelItemDic = new Dictionary<ItemType, int>();
        
            foreach (var itemData in _levelData.LevelItemData)
            {
                if (itemData.ItemType == ItemType.None) continue;
                levelItemDic.TryAdd(itemData.ItemType, 0);

                levelItemDic[itemData.ItemType]++;
            }

            return levelItemDic;
        }

        private void CreateLevelItemPools()
        {
            var itemsInLevel = FindDifferentItemsInLevel();
            
            foreach (var (itemType, count) in itemsInLevel)
            {
                GetOrCreatePool(itemType, count);
            }
        }

        public ItemPool GetOrCreatePool(ItemType itemType, int count = 1)
        {
            if (_poolContainersDic.TryGetValue(itemType, out var poolContainer))
            {
                return poolContainer.Pool;
            }

            var poolParent = new GameObject($"Pool_{itemType}").transform;
            var newItemPoolContainer = new ItemPoolContainer();
            _loader.TryGetPrefab(itemType, out var itemBasePrefab);
                
            newItemPoolContainer.CreatePool(count, true, itemBasePrefab, poolParent);
                
            _poolContainersDic.Add(itemType, newItemPoolContainer);

            return newItemPoolContainer.Pool;
        }
    }
}