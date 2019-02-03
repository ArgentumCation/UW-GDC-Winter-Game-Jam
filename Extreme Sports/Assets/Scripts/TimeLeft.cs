using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    private float SecondsLeft = 3 * 60f;
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        SecondsLeft -= Time.deltaTime;
        int minutes = (int)(SecondsLeft / 60);
        int seconds = (int)(SecondsLeft - (minutes * 60));
        String time = "" + minutes+":";
        if (seconds < 10)
        {
            time += "0";
        }

        time += seconds;
        text.text = time;
        if (SecondsLeft <= 0)
        {
            GameManager.GameOver();
        }
    }
}
