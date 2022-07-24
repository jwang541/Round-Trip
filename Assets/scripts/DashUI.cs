using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    GameManager gm;

    public Text label;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        label.text = "" + (int)gm.player.GetComponent<PlayerMovement>().dashes;
    }
}
