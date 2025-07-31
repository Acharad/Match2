using System;
using UnityEngine;

namespace Match3.Items
{
    [Serializable]
    public class ItemData : ScriptableObject
    {
        public const string NamePrefix = "SO_LevelContentData_";
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Sprite MiniIcon { get; private set; }
    }
}