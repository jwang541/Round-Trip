using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Text label;
    Image bar;

    Vector2 initPos;
    Vector2 initScale;
    public PlayerHealth ph;

    void Start() {
        label = GetComponentInChildren<Text>();
        bar = GetComponentInChildren<Image>();

        initPos = bar.transform.position;
        initScale = bar.transform.localScale;
    }

    void Update()
    {
        bar.transform.localScale = new Vector2(initScale.x, initScale.y * ph.HealthProp());
        //bar.transform.position = new Vector2(initPos.x, initPos.y - bar.transform.localScale.y * 0.5f);
    }
}
