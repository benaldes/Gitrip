using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeNumbers : MonoBehaviour
{
    public float DestroyTime = 1f;
    void Start()
    {
        Offset();
        Destroy(gameObject,DestroyTime);
    }
    public void Offset()
    {
        Vector3 offset = new Vector3(Random.Range(-0.1f,0.1f), 0);
        transform.position += offset;
    }
}
