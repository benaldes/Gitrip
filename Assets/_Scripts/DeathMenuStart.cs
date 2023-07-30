using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuStart : MonoBehaviour
{
    public CanvasGroup menu;
    private void Start()
    {
        
    }
    void Update()
    {
        if(menu.alpha < 1)
        {
            menu.alpha += Time.deltaTime;
        }
        else Time.timeScale = 0f;
    }
}
