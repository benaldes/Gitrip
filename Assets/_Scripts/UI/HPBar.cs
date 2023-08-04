using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Slider _slider;
    private PlayerScript _playerScript;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _slider.value = _playerScript.HP;
        _slider.maxValue = _playerScript.MaxHP;
    }
    public void SetHPBar(Component component, float HP)
    {
        _slider.value = HP;
        _slider.maxValue = HP;
    }
}
