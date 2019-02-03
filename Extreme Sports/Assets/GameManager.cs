using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TODO: calculate in this file
    public static String winner;
    //TODO: Handle Sudden Death
    public static void GameOver()
    {
        print("Game Over");
        SceneManager.LoadScene("GameOver");
        
    }

}