using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuStart : MonoBehaviour
{
    public CanvasGroup menu;
    void Update()
    {
        if(menu.alpha < 1)
        {
            menu.alpha += Time.deltaTime;
        }
    }
}
