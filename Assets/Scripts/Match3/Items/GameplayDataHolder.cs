using System;
using System.Collections.Generic;
using Match3.Items;
using MInject.Runtime.Service;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gameplay/Gameplay Data Holder", fileName = "GameplayPrefabsHolder")]
public class GameplayDataHolder : ScriptableObject, IService
{
    [Serializable]
    public class ItemTypeGameObjectList
    {
        public List<ItemType>  itemTypes;
        public GameObject gameObject;
    }
    public List<ItemTypeGameObjectList> ItemList;

    [Serializable]
    public class ItemTypeDataList
    {
        public ItemType itemType;
        public ItemData itemData;
    }
    public List<ItemTypeDataList> ItemDataList;
    
    public bool TryGetPrefab(ItemType itemType, out GameObject prefab)
    {
        foreach (var entry in ItemList)
        {
            if (entry.itemTypes != null && entry.itemTypes.Contains(itemType))
            {
                prefab = entry.gameObject;
                return true;
            }
        }

        prefab = null;
        return false;
    }
    
    public bool TryGetData<T>(ItemType itemType, out T itemData) where T : ItemData
    {
        itemData = null;
        foreach (var entry in ItemDataList)
        {
            if (entry.itemType.Equals(itemType))
            {
                itemData = entry.itemData as T;
                return true;
            }
        }

        itemData = null;
        return false;
    }
    
    private void LogException(string error)
    {
        var formattedError = $"EXCEPTION! {nameof(GameplayDataHolder)}: {error}";
        Debug.LogException(new Exception(formattedError));
    }

}
