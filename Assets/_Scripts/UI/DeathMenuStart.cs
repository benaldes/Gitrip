using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuStart : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;
    [SerializeField] private TextMeshProUGUI _deathScore;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ExitGame()
    {
        Application.Quit();

    }
    
    void Update()
    {
        if(_menu.alpha < 1)
        {
            _menu.alpha += Time.deltaTime;
        }
        else Time.timeScale = 0f;
    }
}
