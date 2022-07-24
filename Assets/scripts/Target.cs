using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gm;
    AudioSource explosion;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        explosion = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && gm != null) {
            gm.target = !gm.target;
            gm.mapTester().GenerateMap();

            gm.player.GetComponent<PlayerHealth>().RestoreHealth(20);
            gm.score++;

            CameraShake.Shake(0.3f, 0.15f);
            explosion.Play();
        }
    }
}
