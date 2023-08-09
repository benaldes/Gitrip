using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeNumbers : MonoBehaviour
{
    public float DestroyTime = 1f;
    void Start()
    {
        Destroy(gameObject,DestroyTime);
    }
    public void Offset(Vector3 offset)
    {
        transform.position += offset;
    }
}
