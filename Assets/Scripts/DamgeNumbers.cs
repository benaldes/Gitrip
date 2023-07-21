using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeNumbers : MonoBehaviour
{
    public float DestroyTime = 1f;
    //public Vector3 offset = new Vector3(0,0.1f,0);
    void Start()
    {
        Destroy(gameObject,DestroyTime);
        //transform.position += offset;
    }

    public void Offset(Vector3 offset)
    {
        transform.position += offset;
    }
}
