using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
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


    private void OnTriggerExit2D(Collider2D other)
    {
        //print("collide");
        Destroy(gameObject);
    }
}
