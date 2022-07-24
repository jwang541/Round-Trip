using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    GameManager gm;
    public Text help;
    public GameObject pause;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        help.enabled = false;        
        pause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (gm.state == GameManager.State.Ongoing) {
                gm.state = GameManager.State.Paused;
                help.enabled = true;
                pause.SetActive(true);
            } else if (gm.state == GameManager.State.Paused) {
                gm.state = GameManager.State.Ongoing;
                help.enabled = false;
                pause.SetActive(false);
            }
        }   
    }
}
