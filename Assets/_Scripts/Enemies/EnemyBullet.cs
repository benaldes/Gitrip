using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public int Dmg = 10;
    [HideInInspector] public float BulletRange;

    private float timer = 0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" )
        {
            collision.GetComponent<PlayerScript>().PlayerTakeDamage(Dmg);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (timer > BulletRange)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
