using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    GameManager gm;

    public int x;
    public int y;

    public OpenPath next;
    public OpenPath prev;

    public int delay;
    public float timer;

    public GameObject square;
    public GameObject triangle;

    public float secDelay;

    enum Type
    {
        Straight,
        Bent,
    }
    Type type;

    Tuple<int, int>[] dirs = {
        Tuple.Create(1, 0),
        Tuple.Create(0, 1),
        Tuple.Create(-1, 0),
        Tuple.Create(0, -1)
    };
    [SerializeField] int dir = 0;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        square.SetActive(false);
        triangle.SetActive(false);

        // Calculate path type based on prev and next
        if (next == null) {
            dir = prev.dir;
            return;
        }

        if (next.x - x == 1) dir = 0;
        if (next.x - x == - 1) dir = 2;
        if (next.y - y == 1) dir = 1;
        if (next.y - y == - 1) dir = 3;

    }

    void Update() {
        if (!gm.IsOngoing()) return;

        float dt = Time.deltaTime;
        timer += dt;

        if (timer > (delay + 1) * gm.killSpeed + secDelay * gm.killSpeed / gm.initialKillSpeed) {
            square.SetActive(true);
            square.transform.localScale = new Vector2(1, 1);
            triangle.SetActive(false);

        } else if (timer > delay * gm.killSpeed + secDelay * gm.killSpeed / gm.initialKillSpeed) {
            square.SetActive(true);
            
            float progress = (timer - (delay * gm.killSpeed + secDelay * gm.killSpeed / gm.initialKillSpeed)) / gm.killSpeed;
            if (dir == 0) {
                square.transform.localScale = new Vector2(progress, 1);
                square.transform.position = new Vector2(x - 0.5f + progress / 2, y);
            } else if (dir == 1) {
                square.transform.localScale = new Vector2(1, progress);
                square.transform.position = new Vector2(x, y - 0.5f + progress / 2);
            } else if (dir == 2) {
                square.transform.localScale = new Vector2(progress, 1);
                square.transform.position = new Vector2(x + 0.4f - progress / 2, y);
            } else {
                square.transform.localScale = new Vector2(1, progress);
                square.transform.position = new Vector2(x, y + 0.4f - progress / 2);
            }

        }

    }

}
