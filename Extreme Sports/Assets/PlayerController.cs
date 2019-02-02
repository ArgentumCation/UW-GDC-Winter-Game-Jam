using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Element
{
    fire,
    earth,
    water
}

public enum Team
{
    red,
    blue
}

public class PlayerController : MonoBehaviour
{
    public Team team;

    private float forwardMove = 0f;
    private float rotationMove = 0f;
    public float speed;
    public float angularSpeed;
    private Transform bodyTransform;
    private Rigidbody2D rb2d;

    public List<BodyController> bodies;

    public int bodyIndex;

    // Start is called before the first frame update
    void Start()
    {
        foreach (BodyController b in bodies)
        {
            b.Init(this);
        }

        ChangeBody();
    }

    private void ChangeBody()
    {
        bodyIndex = (bodyIndex + 1) % bodies.Count;
        rb2d = bodies[bodyIndex].getRigidBody();
        bodyTransform = rb2d.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        forwardMove = Input.GetAxisRaw("Vertical");
        rotationMove = Input.GetAxisRaw("Horizontal");


        rb2d.velocity +=
            ((Vector2) bodyTransform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime * -1;
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z) ||
            Input.GetMouseButtonDown(1))
        {
            ChangeBody();
        }

        //Fire
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) ||
            Input.GetMouseButtonDown(0))
        {
            bodies[bodyIndex].Fire();
        }
    }


    public void Kill(BodyController b)
    {
        //print(bodies.Count + ", " + bodyIndex);
        //BodyController b = bodies[bodyIndex];
        //print(2);
        if (bodies.Count == 1)
        {
            //Todo: Game Over/Restart
            return;
        }

        int index = bodies.IndexOf(b);
        bodies.RemoveAt(index);
        if (index == bodyIndex)
        {
            bodyIndex--;
            ChangeBody();
        }

        if (index < bodyIndex)
        {
            bodyIndex--;
        }

        Destroy(b.gameObject);
    }
}