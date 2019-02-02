using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string element;

    public string team;

    private float forwardMove = 0f;
    private float rotationMove = 0f;
    public float speed = 1.0f;
    public float angularSpeed = 1.0f;
    public float mu = .1f;
    private Transform transform;
    private Vector2 lastVelocity;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        lastVelocity = Vector2.zero;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        forwardMove = Input.GetAxisRaw("Vertical");
        rotationMove = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        print(rb2d.velocity);
        rb2d.velocity += ((Vector2) transform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime;
    }
}
