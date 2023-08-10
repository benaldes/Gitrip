using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float PlayerRadiosNotToSpawn = 1f, spawnTimer = 3f;
    public Vector2 NoSpawnRadios = new Vector2 (3, 3);
    public Collider2D SpawnArea;
    public List<EnemyX> Enemies = new List<EnemyX>();
    public int WaveNumber = 1;
    public int WaveCount;
    public float Timer = 0f;
    public float WaveInterval = 10f;
    public int BossWaveInterval = 2;
    public AudioSource Clocksound;
    public void WaveStart() 
    {
        Timer = 0f;
        WaveCount = 10 + WaveNumber * 2;
        WaveNumber++;
        StartCoroutine(EnemySpawnInterval());
    }
    IEnumerator EnemySpawnInterval()
    {

        while(WaveCount > 0)
        {
            EnemySpawn();
            yield return new WaitForSeconds(Random.Range(0.1f,1));
        }
        
    }
    private void EnemySpawn()
    {
        int randEnemyId = Random.Range(0,Enemies.Count);
        int enemyCost = Enemies[randEnemyId].Cost;
        WaveCount -= enemyCost;
        if (WaveCount > 0)
        {
            SpawnEnemy(Enemies[randEnemyId].EnemyPrefab);
        }
        else return;
    }
    public void SpawnEnemy(GameObject enemy)
    {
        
        
        Vector2 spawnPosition = GetRandomSpawnPosition(SpawnArea);
        GameObject spawnEnemies = Instantiate(enemy, spawnPosition, Quaternion.identity);
        
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

    private void ClockSound()
    {
        Clocksound.Play();
    }
    
}

[System.Serializable]
public class EnemyX
{
    public GameObject EnemyPrefab;
    public int Cost;
}
