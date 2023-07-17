using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float PlayerRadiosNotToSpawn = 1f, spawnTimer = 3f;
    public GameObject Enemy;
    public Vector2 NoSpawnRadios = new Vector2 (3, 3);
    public Collider2D SpawnArea;
    public GameObject[] EnemiesList;
    
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    
    IEnumerator EnemySpawn()
    {
        SpawnEnemy(SpawnArea, EnemiesList);
        yield return new WaitForSeconds(spawnTimer);
        StartCoroutine(EnemySpawn());
    }

    public void SpawnEnemy(Collider2D SpawnAbleAreaCollider, GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(SpawnAbleAreaCollider);
            GameObject spawnEnemies = Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
    public Vector2 GetRandomSpawnPosition(Collider2D spawnAbleAreaCollider)
    {
        Vector2 spawnPositon = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int attemptMax = 200;

        List<int> LayersToNotSpawn = new List<int>() { LayerMask.NameToLayer("Water"), LayerMask.NameToLayer("object"), LayerMask.NameToLayer("Player") };
        int PlayerLayer = LayerMask.NameToLayer("Player");
        
        while (attemptCount < attemptMax && !isSpawnPosValid)
        {
            spawnPositon = GetRandomPointInCollider(spawnAbleAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPositon,2f);
            Collider2D[] bigColliderForPlayer = Physics2D.OverlapCircleAll(spawnPositon, PlayerRadiosNotToSpawn);

            bool isInvalidCollison = false;
            foreach (Collider2D collider in colliders)
            {
                foreach(int layer in  LayersToNotSpawn)
                {
                    if (collider.gameObject.layer == layer)
                    {
                        isInvalidCollison = true;
                        break;
                    }
                }
                
            }
            foreach (Collider2D collider in bigColliderForPlayer)
            {
                if (collider.gameObject.layer == PlayerLayer)
                {
                    isInvalidCollison = true;
                    break;
                }
            }

            if (!isInvalidCollison)
            {
                isSpawnPosValid = true;
            }
            attemptCount++;
        }
        if(!isSpawnPosValid)
        {
            Debug.LogWarning("so spawn point found");
        }

        return spawnPositon;

    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 3f)
    {   
        Bounds bounds = collider.bounds;
        Vector2 minBounds = new Vector2(bounds.min.x + offset, bounds.min.y + offset);
        Vector2 maxBounds = new Vector2(bounds.max.x - offset, bounds.max.y - offset);
            
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);

    }
    
    

}
