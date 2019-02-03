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
        if (team == Team.blue)
        {
            forwardMove = Input.GetAxisRaw("BlueVertical");
            rotationMove = Input.GetAxisRaw("BlueHorizontal");
        }
        else
        {
            
            forwardMove = Input.GetAxisRaw("RedVertical");
            rotationMove = Input.GetAxisRaw("RedHorizontal");
        }

        rb2d.velocity +=
            ((Vector2) bodyTransform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime * -1;
        if ((team == Team.blue && (Input.GetButtonDown("BlueSwitch"))) ||(team == Team.red && (Input.GetButtonDown("RedSwitch"))))
        {
            ChangeBody();
        }

        //Fire
        if ((team == Team.blue && (Input.GetButtonDown("BlueFire"))) ||(team == Team.red && (Input.GetButtonDown("RedFire"))))
        {
            bodies[bodyIndex].Fire();
        }
    }


    public void Kill(BodyController b)
    {
        //print(bodies.Count + ", " + bodyIndex);
        //BodyController b = bodies[bodyIndex];
        //print(2);
        int index = bodies.IndexOf(b);
        if (bodies.Count == 1)
        {

            if (team == Team.red)
            {
                GameManager.winner = "Blue";
            }
            else
            {
                GameManager.winner = "Red";
            }
            GameManager.GameOver();
            return;
        }

        
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