using System;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static bool CanMove = true;
    
    //TODO: calculate in this file
    public static String winner;
    //TODO: Handle Sudden Death
    public static void GameOver()
    {
        Console.WriteLine("Game Over");
        SceneManager.LoadScene("GameOver");
        
    }

}