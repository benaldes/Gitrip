using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    { _slider = GetComponent<Slider>(); }
    public void SetPlayerHP(Component component, object data)
    {
        if (data is int)
        {
            _slider.value = (int) data;
            _slider.maxValue = (int)data;
        }
    }
    public void UpdateHPBar(Component component, object data)
    { if (data is int) { _slider.value = (int)data; } }
        
}
