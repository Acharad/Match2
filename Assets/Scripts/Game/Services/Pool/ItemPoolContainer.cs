using System;
using Game.Core.Item;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Services.Pool
{
    public class ItemPoolContainer
    {
        private ItemBase _pooledObjectItemBase;
        private Transform _pooledItemTransform;
        public ItemPool Pool { get; private set; }
        
        public void CreatePool(int initialCapacity, bool preWarm, ItemBase pooledObjectItemBase, Transform poolTransform)
        {
            Pool = new ItemPool(CreatePooledObject, GetFromPool, ReleaseToPool,
                DestroyPooledObject, defaultCapacity: initialCapacity);
            
            _pooledObjectItemBase = pooledObjectItemBase;
            _pooledItemTransform = poolTransform;
            
            if(preWarm)
                Pool.PreWarm(initialCapacity);
        }
        
        private ItemBase CreatePooledObject()
        {
            var pooledObj = Object.Instantiate(_pooledObjectItemBase, Vector3.zero,
                _pooledObjectItemBase.transform.rotation, _pooledItemTransform);


            pooledObj.OnItemDestroyed += Release;
            return pooledObj;
        }

        private void Release(ItemBase itemBase)
        {
            Pool.Release(itemBase);
        }

        private void GetFromPool(ItemBase pooledObj)
        {
            if (pooledObj == null || pooledObj.gameObject == null)
            {
                Debug.LogException(new Exception($"EXCEPTION! PooledObj or PooledObj.gameObject is null! " +
                                                 $"You may have destroyed it instead of releasing it to pool at some point in the past..."));
                return;
            }

            pooledObj.gameObject.SetActive(true);
        }

        private void ReleaseToPool(ItemBase pooledObj)
        {
            if (pooledObj == null || pooledObj.gameObject == null)
                return;

            pooledObj.gameObject.SetActive(false);
            
            pooledObj.transform.SetParent(_pooledItemTransform);
        }

        private void DestroyPooledObject(ItemBase pooledObj)
        {
            if (pooledObj == null || pooledObj.gameObject == null)
                return;

            pooledObj.OnItemDestroyed -= Release;
            Object.Destroy(pooledObj.gameObject);
        }
    }
}