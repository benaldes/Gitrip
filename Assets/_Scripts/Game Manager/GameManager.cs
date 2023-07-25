using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerScript _playerScript;

    [SerializeField] private LevelUp _levelUpScript;
    [SerializeField] private Slider _HPBar;
    [SerializeField] private Slider _ExpBar;

    private void Awake()
    {
        _levelUpScript = GetComponent<LevelUp>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        if (_playerScript != null)
            Instance = this;
        else 
            Destroy(gameObject);
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
            //LevelUp();
        }
    }
    
}
