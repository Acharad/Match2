using System;
using Game.Core.Item;

namespace Game.Core.Level
{
    [Serializable]
    public class LevelData
    {
        public int Rows = 9;
        public int Cols = 9;
        
        public ItemData[] LevelItemData;

        public ItemData[] GeneratorData;
    
        public ItemData GetItem(int row, int col)
        {
            return LevelItemData[row * Cols + col];
        }
    }
}