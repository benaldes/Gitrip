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
    

    
    private PlayerScript _playerScript;


    private void Awake()
    {
        
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        if (_playerScript != null)
            Instance = this;
        else 
            Destroy(gameObject);
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
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

}
