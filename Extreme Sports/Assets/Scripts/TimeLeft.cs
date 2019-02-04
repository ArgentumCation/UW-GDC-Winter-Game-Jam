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
        if (GameManager.CanMove)
            SecondsLeft -= Time.deltaTime;
        
        int minutes = (int) SecondsLeft / 60;
        int seconds = (int) SecondsLeft % 60;
        text.text = String.Format("{0}:{1:D2}", minutes, seconds);
        if (SecondsLeft <= 0)
        {
            GameManager.winner = "Neither";
            GameManager.GameOver();
        }
    }
}
