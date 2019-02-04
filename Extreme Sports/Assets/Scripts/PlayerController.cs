using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


public enum Element
{
    fire,
    earth,
    water
}

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Team
{
    Red,
    Blue
}

public class PlayerController : MonoBehaviour
{
    public Team team;

    private float forwardMove = 0;
    private float rotationMove = 0;
    public float speed;
    public float angularSpeed;
    private Transform bodyTransform;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    public List<BodyController> bodies;

    public int bodyIndex;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (BodyController b in bodies)
        {
            b.Init(this);
        }

        bodyIndex = -1;
        ChangeBody();
        
        if (team == Team.Red)
            GameManager.RedController = this;
        else
            GameManager.BlueController = this;
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
        if (!GameManager.CanMove)
            return;
        
        forwardMove = Input.GetAxisRaw(team + "Vertical");
        rotationMove = Input.GetAxisRaw(team + "Horizontal");

        rb2d.velocity +=
            ((Vector2) bodyTransform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime * -1;
        if (Input.GetButtonDown(team + "Switch"))
        {
            ChangeBody();
        }

        //Fire
        if (Input.GetButtonDown(team + "Fire"))
        {
            bodies[bodyIndex].Fire();
        }
    }


    public void Kill(BodyController b)
    {
        int index = bodies.IndexOf(b);
        if (bodies.Count == 1)
        {
            GameManager.winner = team.ToString();
            GameManager.GameOver();
            return;
        }


        bodies.RemoveAt(index);
        if (index == bodyIndex)
        {
            bodyIndex--;
            ChangeBody();
        }
        else if (index < bodyIndex)
        {
            bodyIndex--;
        }
        audioSource.Play();    
        b.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        b.gameObject.transform.Find("Quad").gameObject.SetActive(false);
        Destroy(b.gameObject,3f);
        
    }
}