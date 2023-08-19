using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Dmg = 10;
    public float BulletRange;
    private float timer = 0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<AbstractEnemy>().TakeDamage(Dmg);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "LordOfTheflies")
        {
            collision.GetComponent<LordOfTheflies>().takeDamage(Dmg);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PIckup")
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        if(timer > BulletRange) 
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
