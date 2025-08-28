using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Item
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ItemUIData/" + nameof(ItemUIData), fileName = "SO_ItemUIData_")]
    public class ItemUIData : ScriptableObject
    {
        public ItemBase ItemBase;
        public List<Sprite> ItemSprites;
    }
}