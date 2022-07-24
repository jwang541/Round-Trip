using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    Text label;

    void Start()
    {
        label = GetComponent<Text>();
    }

    void Update()
    {
        label.text = "HI-SCORE: " + PlayerPrefs.GetInt("rush_highscore");
    }
}
