using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;

    public KeyCode turnKey;
    public float baseSpeed;
    public float angSpeed;

    [SerializeField] float speed;
    public float friction;

    Rigidbody2D rb;
    public AudioSource warp;

    TrailRenderer tr;
    float baseWidth;
    float baseTime;

    public float maxDashes;
    public float dashes;
    public float dashRegen;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        speed = baseSpeed;

        baseWidth = tr.startWidth;
        baseTime = tr.time;

        dashes = maxDashes;
    }

    void Update()
    {
        if (!gm.IsOngoing()) {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        float dt = Time.deltaTime;

        rb.velocity = transform.right * speed;
        if (Input.GetKey(turnKey)) {
            rb.rotation += angSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) && dashes >= 1) {
            speed = baseSpeed * 3f;
            warp.Play();

            dashes--;
        }
        dashes = Mathf.Min(dashes + dt * dashRegen * (1 + gm.scale * gm.score), maxDashes);
        speed = baseSpeed + (speed - baseSpeed) * Mathf.Pow(friction, dt);
        
        tr.startWidth = baseWidth * speed / baseSpeed;
        tr.time = baseTime * speed / baseSpeed;
        
    }
}
