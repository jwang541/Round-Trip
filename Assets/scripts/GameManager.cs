using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;

    MapTester mt;

    public GameObject player;

    public GameObject endPrefab;
    GameObject end;

    public bool target;

    public float initialKillSpeed;
    public float killSpeed;
    float initialPlayerSpeed;
    float initialPlayerW;

    public int score;
    public float scale;

    public enum State
    {
        Ongoing,
        Stopped,
        Paused
    }
    public State state;
    float refreshTimer = 0;
    public float refreshTime = 5.0f;

    public float startDelay;

    bool started = false;
    public GameObject startPrompt;

    void Start()
    {
        mt = GetComponent<MapTester>();
        end = Instantiate(endPrefab, new Vector2(mt.nRows - 1.05f, mt.nColumns - 1.05f), endPrefab.transform.rotation);

        state = State.Ongoing;

        initialPlayerSpeed = player.GetComponent<PlayerMovement>().baseSpeed;
        initialPlayerW = player.GetComponent<PlayerMovement>().angSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && started == false) {
            started = true;
            state = State.Ongoing;
            startPrompt.SetActive(false);
        }
        if (!started) {
            state = State.Paused;
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            SceneManager.LoadScene("Title");
        }


        killSpeed = initialKillSpeed / (1 + scale * score);
        player.GetComponent<PlayerMovement>().baseSpeed = initialPlayerSpeed * (1 + scale * score);
        player.GetComponent<PlayerMovement>().angSpeed = initialPlayerW * (1 + scale * score);

        float dt = Time.deltaTime;
        if (state == State.Ongoing) {
            if (target) {
                end.transform.position = new Vector3(mt.nRows - 1.05f, mt.nColumns - 1.05f, -0.5f);
            } else {
                end.transform.position = new Vector3(0, 0, -0.5f);
            }
        }
        else if (state == State.Stopped) {
            if (SceneManager.GetActiveScene().name == "Rush") {
                int prev = PlayerPrefs.GetInt("rush_highscore");
                PlayerPrefs.SetInt("rush_highscore", Math.Max(score, prev));
            }

            refreshTimer += dt;
            if (refreshTimer > refreshTime) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else if (state == State.Paused) {

        }
    }

    public MapTester mapTester() { return mt; }
    public bool IsOngoing() { return (state == State.Ongoing); }
    public void Stop() { state = State.Stopped; }


}
