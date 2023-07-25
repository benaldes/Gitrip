using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;
     


    public void SetHPSlider(int HP,int MaxHP)
    {
        slider.value = HP;
        slider.maxValue = MaxHP;
        
    }
}