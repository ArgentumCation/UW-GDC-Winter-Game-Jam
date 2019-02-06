using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBGColor : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera camera;
    private Color color;
    void Awake()
    {
        camera = GetComponent<Camera>();
        if (GameManager.winner.Equals("Red"))
        {
            color = new Color(173,62,80);
        }
        else if (GameManager.winner.Equals("Blue"))
        {
            color = new Color(0,125,199);
        }
        else
        {
            color = new Color(80,87,107);
        }
        camera.backgroundColor = color/255f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
