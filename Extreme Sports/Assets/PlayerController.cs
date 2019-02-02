using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Element
{
    fire,earth,water
    
}

public enum Team
{
    red,blue
}
public class PlayerController : MonoBehaviour
{
    public Element element;

    public Team team;

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
    private void Update()
    {
        forwardMove = Input.GetAxisRaw("Vertical");
        rotationMove = Input.GetAxisRaw("Horizontal");
        
     
        rb2d.velocity += ((Vector2) transform.right) * forwardMove * Time.deltaTime * speed;
        rb2d.angularVelocity = rotationMove * angularSpeed * Time.deltaTime;
        //Fire
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) ||
            Input.GetMouseButtonDown(0))
        {
            GameObject bulletInstance =Instantiate(bullet, transform.position + transform.right/1.5f, Quaternion.identity);
            Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
            bulletrb2d.velocity = transform.right.normalized*bulletSpeed;
            
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("court"))
        {
            Destroy(gameObject);
        }
    }


}
