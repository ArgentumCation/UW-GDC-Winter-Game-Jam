using UnityEngine;

public class rock : MonoBehaviour
{
    // Start is called before the first frame update
    public int despawnHits;
    private int hits = 0;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        audioSource.Play();

        if (other.gameObject.CompareTag("earth"))
        {
            Destroy(gameObject);
            return;
        }
        
        hits++;
        if (hits > despawnHits)
        {
            Destroy(gameObject);
            return;
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
