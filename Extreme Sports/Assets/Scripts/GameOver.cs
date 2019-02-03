using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text text;
    private float waitTime = 2f;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameManager.winner + " team wins!";
    }

    // Update is called once per frame
    void Update()
    {

        waitTime -= Time.deltaTime;
        if (Input.anyKeyDown && waitTime <= 0)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
