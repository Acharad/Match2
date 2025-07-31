using System;
using System.Collections.Generic;
using MInject.Runtime;
using UnityEngine;

namespace Match3.Items
{
	public class Board : MonoBehaviour
	{
		public int Rows = 9;
		public int Cols = 9;
		public const int MinimumMatchCount = 2;
		public const int MinimumSpecialItemCellCount = 5;
		public const int MinimumRocketItemCellCount = 5;
		public const int MinimumBombItemCellCount = 7;

		public Transform CellsParent;
		public Transform ItemsParent;

		[SerializeField] private Cell CellPrefab;

		public Cell[,] Cells;
	
		public void Prepare(LevelData levelData)
		{
			Rows = levelData.Rows;
			Cols = levelData.Cols;
			
			Cells = new Cell[levelData.Rows, levelData.Cols];
			
			CreateCells();
			PrepareCells();
		}
		
		private void CreateCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					var cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
					Cells[x, y] = cell;
				}
			}
		}

		private void PrepareCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					Cells[x, y].Prepare(x, y, this);
				}
			}
		}
	}
}
