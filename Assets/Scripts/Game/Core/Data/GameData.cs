using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        public int MinMatchCount = 2;
        public List<int> HintThresholds;
    }
}
