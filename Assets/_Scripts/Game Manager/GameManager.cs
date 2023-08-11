using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject LevelUpPanal;

    [SerializeField] private LevelUpPanal _levelUpScript;
    [SerializeField] private Slider _ExpBar;
    [SerializeField] private GameObject _clock;
    [SerializeField] private GameObject _player;

    
    private PlayerScript _playerScript;


 
    private void Start()
    {
        Time.timeScale = 1.0f;
        _playerScript = _player.GetComponent<PlayerScript>();
    }
    public void Levelup()
    {
        _playerScript.Level++;
        _playerScript.ExperienceToLevelUp *= 2;        

        LevelUpPanal.SetActive(true);
        _levelUpScript.LevelUp();
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public  void NextLevel(Component component, object data)
    {
        if(SceneManager.sceneCount+1 > SceneManager.GetActiveScene().buildIndex)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
