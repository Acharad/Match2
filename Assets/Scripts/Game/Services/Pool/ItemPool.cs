using System;
using System.Collections.Generic;
using Game.Core.Item;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Pool
{
    public class ItemPool : ObjectPool<ItemBase> 
    {
        private List<ItemBase> pooledObjects = new();

        public ItemPool(Func<ItemBase> createFunc, Action<ItemBase> actionOnGet = null, 
            Action<ItemBase> actionOnRelease = null, Action<ItemBase> actionOnDestroy = null, 
            bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000) : 
            base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
        }
        
        public void PreWarm(int count)
        {
            pooledObjects.Clear();
            for (var i = 0; i < count; i++)
            {
                var item = Get();
                pooledObjects.Add(item);
            }

            foreach (var pooledObject in pooledObjects)
            {
                try
                {
                    Release(pooledObject);
                }
                catch (Exception e)
                {
                    Debug.LogException(new Exception($"EXCEPTION! ObjectPooling->PreWarm: {e}"));
                }
            }
            
            pooledObjects.Clear();
        }
    }
}