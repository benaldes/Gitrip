using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool IsGamePaused = false;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        IsGamePaused = true;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
