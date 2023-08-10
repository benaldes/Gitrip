using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackShow : MonoBehaviour
{
    public float disappear = 0.2f;
    public float timer = 0f;

    void Update()
    {
        if (timer < disappear) 
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
