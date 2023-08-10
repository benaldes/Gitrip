using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Dmg = 10;
    public float BulletRange;

    [SerializeField] private PlayerScript _playerScript;

    private float timer = 0;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" )
        {
            _playerScript.PlayerTakeDamage(Dmg);
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
