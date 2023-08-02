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
    [SerializeField] private Slider _HPBar;
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
    void Update()
    {
        SetHPBar();
        SetExpSlider();
        
    }
    
    private void SetHPBar()
    {
        _HPBar.value = _playerScript.HP;
        _HPBar.maxValue = _playerScript.MaxHP;
    }
    private void SetExpSlider()
    {
        _ExpBar.maxValue = _playerScript.ExperienceToLevelUp;
        _ExpBar.value = _playerScript.Experience;
        if (_ExpBar.value >= _ExpBar.maxValue)
        {

            Levelup();
        }
    }
    public void Levelup()
    {
        _playerScript.Level++;
        _playerScript.ExperienceToLevelUp *= 2f;        

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
