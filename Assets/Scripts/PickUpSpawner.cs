using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public float spawnTimer = 1f;
    public float SpawnRadios = 5f;
    public GameObject[] Boxs;

    void Start()
    {
        StartCoroutine(PickupSpawner());
    }
    IEnumerator PickupSpawner()
    {
        Vector2 spawnPos = GameObject.Find("Human").transform.position;
        Vector2 spawnDir = Random.insideUnitCircle * SpawnRadios;

        if (spawnDir.x < 3 && spawnDir.x > -3)
        {
            spawnDir.x = Random.Range(3, 7);
            if (Random.value > 0.5)
                spawnDir.x *= -1;
        }
        if (spawnDir.y < 3 && spawnDir.y > -3)
        {
            spawnDir.y = Random.Range(3, 7);
            if (Random.value > 0.5)
                spawnDir.y *= -1;
        }

        spawnPos += spawnDir;

        Instantiate(Boxs[Random.Range(0,Boxs.Length)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        StartCoroutine(PickupSpawner());
    }
}
