using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool IsGamePaused = false;


    public void SetPauseMenu()
    {      
        if (!IsGamePaused) PauseGame();
        else ResumeGame();   
    }
    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Environment.Exit(0);
    }
}
