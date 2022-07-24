using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitMap : MonoBehaviour
{
    Maze.MapTester mt;
    Camera cm;

    public void Start() {
        mt = GameObject.FindGameObjectWithTag("GameController").GetComponent<Maze.MapTester>();
        cm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Update() {
        cm.transform.position = new Vector3(mt.nRows / 2.0f - 0.5f, mt.nColumns / 2.0f - 0.5f, -10);
        cm.orthographicSize = 4.0f * (mt.nColumns + 2) / 7.0f;
    }
    
}
