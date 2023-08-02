using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public CanvasGroup menu;
    public Slider slider;
    public bool Show = false;
    private void Start()
    {
        slider.GetComponent<Slider>();
        menu = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if(Show && menu.alpha < 1)
        {
            menu.alpha += Time.deltaTime;
        }
        else if(!Show && menu.alpha > 0)
        {
            menu.alpha -= Time.deltaTime;
        }
        
    }
    public void BossIsBurn(int MaxHP)
    {
        slider.maxValue = MaxHP;
        slider.value = MaxHP;
    }
    public void UpdateHP(int HP)
    {
        slider.value = HP;
    }

}
