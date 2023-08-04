using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Slider _slider;
    private PlayerScript _playerScript;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _slider = GetComponent<Slider>();
        SetExpSlider();

    }
    public void SetExpSlider()
    {
        _slider.maxValue = _playerScript.ExperienceToLevelUp;
        _slider.value = _playerScript.Experience;

    }
}
