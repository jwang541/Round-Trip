using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze {
    [Serializable]
    public class Cell
    {
        public bool hasTopWall;
        public bool hasRightWall;


        public Cell() {
            hasTopWall = true;
            hasRightWall = true;
        }

        public Cell(bool topWalled, bool rightWalled) {
            hasTopWall = topWalled;
            hasRightWall = rightWalled;
        }
    }
}