using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameManager.winner + " team wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
