using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int PlayersInside;
    private bool clean;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!clean)
        {
            clean = true;
            PlayersInside = 0;
        }
        
        if (other.CompareTag("Players"))
        {
            PlayersInside++;
        }
    }
    
    private void Update()
    {
        clean = false;
    }
}
