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
    
    private Transform transform;
    private Vector2 lastVelocity;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        lastVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        forwardMove = Input.GetAxisRaw("Horizontal");
        rotationMove = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 currentAcceleration = (rb2d.velocity - lastVelocity)/Time.deltaTime;
        rb2d.velocity += new Vector2(forwardMove,0)*speed*Time.deltaTime;
        rb2d.angularVelocity += rotationMove * angularSpeed * Time.deltaTime;

    }
}
