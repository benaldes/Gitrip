using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public void SpawnBoss()
    {
        Instantiate(Enemies[0]);
    }

    
}
