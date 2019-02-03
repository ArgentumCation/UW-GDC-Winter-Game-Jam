using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static String winner;
    public static void GameOver()
    {
        print("Game Over");
        SceneManager.LoadScene("GameOver");
        
    }
}