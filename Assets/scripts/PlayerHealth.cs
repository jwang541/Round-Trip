using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameManager gm;

    public float maxHealth;
    public float health;
    public float hps;

    Rigidbody2D rb;

    int numTriggers = 0;

    public AudioSource pulse;

    public AudioSource shatter;
    bool played = false;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        pulse.volume = 0;
        pulse.Play();
    }

    void Update() {
        if (gm.state != GameManager.State.Ongoing) return;

        float dt = Time.deltaTime;
        if (numTriggers > 0) {
            health = Mathf.Max(health - hps * dt, 0);
            pulse.volume = Mathf.Min(1, pulse.volume + 1.0f * dt);
        } else {
            pulse.volume = Mathf.Max(0, pulse.volume - 1.0f * dt);
        }

        if (health <= 0) {
            pulse.Stop();
            gm.Stop();

            if (!played) {
                shatter.Play();
                played = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "KillArea") {
            numTriggers++;
        }
        Debug.Log("enter");
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "KillArea") {
            numTriggers--;
        }
        Debug.Log("leave");
    }

    public float HealthProp() { return health / maxHealth; }
    public void RestoreHealth(float amount) { health = Mathf.Min(health + amount, maxHealth); }
    
}
