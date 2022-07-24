using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Maze {
    public class MapGenerator
    {
        System.Random randomEngine;
        public int nRows;
        public int nColumns;
        


        public MapGenerator(int rows, int columns) 
        {
            randomEngine = new System.Random();

            nRows = rows;
            nColumns = columns;

        }



        // Returns a new Map, after randomizing its maze layout and contents.
        public Map GenerateMap() 
        {
            Map map = new Map(nRows, nColumns);
            GenerateCells(map);
            return map;
        }
        
        // Helper for recursive GenerateCells.
        public void GenerateCells(Map map) 
        {
            //GenerateCells(map, 0, 0, new bool[nRows, nColumns]);
            GenerateCells(map, nRows/2, nColumns/2, new bool[nRows, nColumns]);
        }

        // Recursive implementation of random DFS.
        // Will generate the map's maze.
        public void GenerateCells(Map map, int x, int y, bool[,] visited) 
        {

            visited[x,y] = true;

            Tuple<int, int>[] directions = 
            { 
                Tuple.Create(0 ,  1), 
                Tuple.Create(0 , -1), 
                Tuple.Create(1 ,  0), 
                Tuple.Create(-1,  0) 
            };

            randomEngine.Shuffle(directions);

            foreach (var dir in directions) {
                if (x + dir.Item1 < 0 || y + dir.Item2 < 0 || x + dir.Item1 >= map.cells.GetLength(0) || y + dir.Item2 >= map.cells.GetLength(1)) continue;
                if (!visited[x + dir.Item1, y + dir.Item2]) {
                    if (dir.Item1 == 1) map.cells[x, y].hasRightWall = false;
                    if (dir.Item1 == -1) map.cells[x - 1, y].hasRightWall = false;

                    if (dir.Item2 == 1) map.cells[x, y].hasTopWall = false;
                    if (dir.Item2 == -1) map.cells[x, y - 1].hasTopWall = false;

                    GenerateCells(map, x + dir.Item1, y + dir.Item2, visited);
                }
            }

        }

    }
}