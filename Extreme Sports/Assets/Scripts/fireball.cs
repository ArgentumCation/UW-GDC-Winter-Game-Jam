using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public float despawnTime;
    private float elapsedTime = 0f;


    // Update is called once per frame
    private void Update()
    {
        if (elapsedTime > despawnTime)
        {
            Destroy(gameObject);
        }
        elapsedTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        
        //print("collide");
        Destroy(gameObject);
    }
}
