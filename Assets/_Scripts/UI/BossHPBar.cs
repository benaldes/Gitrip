using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private CanvasGroup menu;
    [SerializeField] private Slider slider;
    private bool Show = false;

    void Update()
    {
        if(Show && menu.alpha < 1)
        { menu.alpha += Time.deltaTime/4; }
        else if(!Show && menu.alpha > 0)
        { menu.alpha -= Time.deltaTime; }  
    }
    public void BossIsBurn(Component component, object data)
    {
        Show = true;
        if(data is int)
        {
            slider.maxValue = (int)data;
            slider.value = (int)data;
        }
        
    }
    public void UpdateHP(Component component, object data)
    { if (data is int) { slider.value = (int)data; } }
    public void BossIsDead(Component component, object data)
    {
        Debug.LogError("boss  is  dead");
        Show = false;
    }

}
