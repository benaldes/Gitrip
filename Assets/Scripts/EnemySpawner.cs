using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadios = 1f, spawnTimer = 3f;
    public GameObject Enemy;
    public Vector2 NoSpawnRadios = new Vector2 (3, 3);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        Vector2 spawnPos = GameObject.Find("Human").transform.position;
        Vector2 SpawnRadios = Random.insideUnitCircle * spawnRadios;
        
        if (SpawnRadios.x < 3 && SpawnRadios.x > -3)
        { 
            SpawnRadios.x = Random.Range(3,7);
            if (Random.value > 0.5)
                SpawnRadios.x *= -1;
        }
        if (SpawnRadios.y < 3 && SpawnRadios.y > -3)
        {
            SpawnRadios.y = Random.Range(3, 7);
            if (Random.value > 0.5)
                SpawnRadios.y *= -1;
        }
        


        spawnPos += SpawnRadios;
        Instantiate(Enemy, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);
        StartCoroutine(EnemySpawn());
    }
}
