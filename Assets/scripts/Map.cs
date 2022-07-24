using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Maze {
    public class Map
    {
        // The maze data is stored as a 2D array of Cells. 
        // Each cell has 2 bools, which determine if a cell is connected to the cell above and to the right of it.
        public Cell[,] cells;

        [SerializeField] int serializeRows;
        [SerializeField] int serializeColumns;
        [SerializeField] List<Package<Cell>> serializableCells;
        
        // Construct a map with x columns and y rows, defaulting to 10x10.
        public Map(int x=10, int y=10) {
            cells = new Cell[x, y];
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    cells[i,j] = new Cell();
                }
            }
        }

    }
}