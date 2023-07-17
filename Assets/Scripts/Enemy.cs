using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemeyDamage = 10;
    public int HP = 10;

    public float DamageTimer = 1f;
    public float Timer = 0f;

    public PlayerMovment player;
    public Rigidbody2D thisEnemy;
    public GameObject DamageNambersText;
    void Start()
    {
        thisEnemy = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
    }
    private void Update()
    {
         Timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            //thisEnemy.AddForce(new Vector2(2000,2000), ForceMode2D.Force);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hj");
        if (collision.gameObject.tag == "Player" && DamageTimer < Timer )
        {
            player.PlayerTakeDamage(EnemeyDamage);
            Timer = 0f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DamageTimer < Timer)
        {
            player.PlayerTakeDamage(EnemeyDamage);
            Timer = 0f;
        }
    }

    public void takeDamage(int DMG)
    {
        HP -= DMG;
        if( HP <= 0 )
        {
            Destroy(gameObject);
        }
        
        DamageNumbers();
    }
    public void Knockback(Vector2 direction , float Knockbackforce)
    {
        thisEnemy.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }
    public void DamageNumbers()
    {
        var DMGNUM = Instantiate(DamageNambersText,transform.position,Quaternion.identity,transform);
        DMGNUM.GetComponent<TextMeshPro>().text = HP.ToString();
    }
}
