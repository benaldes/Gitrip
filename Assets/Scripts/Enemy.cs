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
    public Animator DeathAnimator;
    public Collider2D collider2D;
    void Start()
    {
        thisEnemy = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
    }
    private void Update()
    {
         Timer += Time.deltaTime;
        
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
            //Destroy(gameObject);
            
            StartCoroutine(Death());
        }
        
        DamageNumbers();
    }

    private IEnumerator Death()
    {
        DeathAnimator.SetBool("Dead", true);
        Destroy(collider2D);
        yield return new WaitForSeconds(0.19f);
        Destroy(gameObject);
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
