using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public float despawnTime;
    private float elapsedTime = 0f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (elapsedTime > despawnTime)
        {
            Destroy(gameObject);
        }

        elapsedTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //print(audioSource.clip.name);
        audioSource.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 3f);
    }
}