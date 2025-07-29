using System;
using Match3.Items;

[Serializable]
public class LevelData
{
    public int Rows = 9;
    public int Cols = 9;
    
    
    public ItemType[] GridData;
    
    public ItemType GetItem(int row, int col)
    {
        return GridData[row * Cols + col];
    }
}

