using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    private float timer = 0;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //collision.GetComponent<AudioSource>().Play();
            collision.GetComponent<Enemy>().takeDamage(_playerScript.PlayerDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "FatMen")
        {
            //collision.GetComponent<AudioSource>().Play();
            collision.GetComponent<FatMen>().takeDamage(_playerScript.PlayerDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Sklislime")
        {
            //collision.GetComponent<AudioSource>().Play();
            collision.GetComponent<Sklislime>().takeDamage(_playerScript.PlayerDamage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PIckup")
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        if(timer > 3) 
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
