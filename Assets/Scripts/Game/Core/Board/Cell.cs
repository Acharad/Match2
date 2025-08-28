using System.Collections.Generic;
using Game.Core;
using Game.Core.Board;
using Game.Core.Item;
using UnityEngine;

public class Cell : MonoBehaviour
{ 
    [HideInInspector] public int X;
    [HideInInspector] public int Y;

    public float CellOffset;
    public Dictionary<Direction, Cell> Neighbours { get; private set; }

    public Cell CellBelow;
    public bool IsGeneratorCell;
    
    private ItemBase _itemBase;
    public ItemBase ItemBase
    {
        get
        {
            return _itemBase;
        }
        set
        {
            if (_itemBase == value) return;
				
            var oldItem = _itemBase;
            _itemBase = value;
				
            if (oldItem != null && Equals(oldItem.Cell, this))
            {
                oldItem.Cell = null;
            }
            if (value != null)
            {
                value.Cell = this;
            }
        }
    }
    
    public void Prepare(int x, int y, Board board)
    {
        X = x;
        Y = y;
        
        IsGeneratorCell = Y == board.Rows - 1;
        
        var offsetX = (board.Cols - 1) * CellOffset * 0.5f;
        var offsetY = (board.Rows - 1) * CellOffset * 0.5f;

        transform.localPosition = new Vector3(
            x * CellOffset - offsetX,
            y * CellOffset - offsetY,
            0f
        );
        SetNeighbours(board);
    }
    
    private void SetNeighbours(Board board)
    {
        Neighbours = new Dictionary<Direction, Cell>();
        
        var left = board.GetNeighbourWithDirection(this, Direction.Left);
        var right = board.GetNeighbourWithDirection(this, Direction.Right);
        var up = board.GetNeighbourWithDirection(this, Direction.Up);
        var down = board.GetNeighbourWithDirection(this, Direction.Down);

        if (left != null) Neighbours.Add(Direction.Left, left);
        if (right != null) Neighbours.Add(Direction.Right,right);
        if (up != null) Neighbours.Add(Direction.Up,up);
        if (down != null)
        {
            Neighbours.Add(Direction.Down,down);
            CellBelow = down;
        }
    }

    public Cell GetBottomAvailableCell()
    {
        var bottomCell = this;
        while (bottomCell.CellBelow != null && !bottomCell.CellBelow.HasItem())
        {
            bottomCell = bottomCell.CellBelow;
        }

        return bottomCell;
    }

    public bool HasItem()
    {
        return ItemBase != null;
    }
}
