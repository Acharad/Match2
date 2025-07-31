using UnityEngine;

namespace Match3.Items
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ItemData/" + nameof(CubeItemData), fileName = NamePrefix + "CubeItem")]
    public class CubeItemData : ItemData
    {
        [field: SerializeField] public ItemColor ItemColor { get; private set; }
    }
}