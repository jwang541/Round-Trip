using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Maze {
    public class MapTester : MonoBehaviour
    {
        public GameManager gm;

        public int nRows;
        public int nColumns;

        public GameObject tile1;
        public GameObject tile2;
        public GameObject tile3;
        public GameObject tile4;
        public GameObject blocked;
        public GameObject start;
        public GameObject end;
        public GameObject open;

        List<GameObject> cells;
        Map map;

        void Awake() {
            cells = new List<GameObject>();
            map = new Map();
        }

        void Start() {
            GenerateMap();
        }

        public void GenerateMap() {
            foreach (var c in cells) Destroy(c);
            cells.Clear();

            MapGenerator generator = new MapGenerator(nRows, nColumns);
            map = generator.GenerateMap();

            for (int i = 0; i < map.cells.GetLength(0); i++) {
                for (int j = 0; j < map.cells.GetLength(1); j++) {

                    if (map.cells[i,j].hasRightWall && map.cells[i,j].hasTopWall) 
                        cells.Add(Instantiate(tile4, new Vector3(i, j, 0), Quaternion.identity, transform));
                    else if (map.cells[i,j].hasRightWall) 
                        cells.Add(Instantiate(tile3, new Vector3(i, j, 0), Quaternion.identity, transform));
                    else if (map.cells[i,j].hasTopWall) 
                        cells.Add(Instantiate(tile2, new Vector3(i, j, 0), Quaternion.identity, transform));
                    else 
                        cells.Add(Instantiate(tile1, new Vector3(i, j, 0), Quaternion.identity, transform));
                }
            }

            for (int i = 0; i < map.cells.GetLength(0); i++) {
                cells.Add(Instantiate(blocked, new Vector3(i, -1, 0), Quaternion.identity, transform));
                cells.Add(Instantiate(blocked, new Vector3(i, map.cells.GetLength(1), 0), Quaternion.identity, transform));
            }
            for (int j = 0; j < map.cells.GetLength(1); j++) {
                cells.Add(Instantiate(blocked, new Vector3(-1, j, 0), Quaternion.identity, transform));
                cells.Add(Instantiate(blocked, new Vector3(map.cells.GetLength(0), j, 0), Quaternion.identity, transform));
            }

            List<Tuple<int, int>> paths = SinglePath(0, 0, nRows - 1, nColumns - 1);
            if (gm.target) paths.Reverse();

            List<OpenPath> opens = new List<OpenPath>();
            foreach (var p in paths) {
                GameObject openpath = Instantiate(open, new Vector2(p.Item1, p.Item2), Quaternion.identity, transform);
                cells.Add(openpath);

                OpenPath op = openpath.GetComponent<OpenPath>();
                op.x = p.Item1;
                op.y = p.Item2;
                opens.Add(op);
            }
            for (int i = 0; i < paths.Count; i++) opens[i].delay = i;
            for (int i = 0; i < paths.Count - 1; i++) opens[i].next = opens[i + 1];
            for (int i = 1; i < paths.Count; i++) opens[i].prev = opens[i - 1];

        }

        public List<Tuple<int, int>> SinglePath(int x1, int y1, int x2, int y2) {
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            SinglePath(x1, y1, x2, y2, new bool[nRows, nColumns], path);

            bool[,] onPath = new bool[nRows, nColumns];
            foreach (var c in path) {
                onPath[c.Item1, c.Item2] = true;
            }
            for (int i = 0; i < nRows; i++) {
                for (int j = 0; j < nColumns; j++) {
                    if (!onPath[i, j]) 
                        cells.Add(Instantiate(blocked, new Vector2(i, j), Quaternion.identity, transform));
                }
            }

            return path;
        }
        public bool SinglePath(int x, int y, int xf, int yf, bool[,] visited, List<Tuple<int, int>> p) {
            if (visited[x, y]) return false;
            if (x == xf && y == yf) {
                p.Add(Tuple.Create(x, y));
                return true;
            }
            visited[x, y] = true;
            
            bool onPath = false;
            
            Tuple<int, int>[] directions = 
            { 
                Tuple.Create(0 ,  1), 
                Tuple.Create(0 , -1), 
                Tuple.Create(1 ,  0), 
                Tuple.Create(-1,  0) 
            };

            foreach (var dir in directions) {
                if (x + dir.Item1 < 0 || y + dir.Item2 < 0 || x + dir.Item1 >= map.cells.GetLength(0) || y + dir.Item2 >= map.cells.GetLength(1)) continue;
                if (dir.Item1 == 1 && map.cells[x, y].hasRightWall == false) 
                    if (SinglePath(x + dir.Item1, y + dir.Item2, xf, yf, visited, p)) onPath = true;
                if (dir.Item1 == -1 && map.cells[x - 1, y].hasRightWall == false) 
                    if (SinglePath(x + dir.Item1, y + dir.Item2, xf, yf, visited, p)) onPath = true;
                if (dir.Item2 == 1 && map.cells[x, y].hasTopWall == false) 
                    if (SinglePath(x + dir.Item1, y + dir.Item2, xf, yf, visited, p)) onPath = true;
                if (dir.Item2 == -1 && map.cells[x, y - 1].hasTopWall == false) 
                    if (SinglePath(x + dir.Item1, y + dir.Item2, xf, yf, visited, p)) onPath = true;
            }
            if (onPath) {
                p.Add(Tuple.Create(x, y));
            }

            return onPath;
        }

    }

}