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
    public float speed;
    public float angularSpeed;
    private Transform transform;
    private Rigidbody2D rb2d;
    public float bulletSpeed;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
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
        //print(rb2d.velocity);
        rb2d.velocity += ((Vector2) transform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime;
        print(Input.inputString);
        //Fire
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) ||
            Input.GetMouseButtonDown(0))
        {
            if (element == "fire")
            {
                
            }
            fireBall();
        }
    }

    void fireBall()
    {
        GameObject bulletInstance =Instantiate(bullet, transform.position + transform.right/3, Quaternion.identity);
        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
        bulletrb2d.velocity = transform.right.normalized*bulletSpeed;
    }
}
