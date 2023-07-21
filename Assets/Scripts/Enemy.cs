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
    public SpriteRenderer Sprite;

    private Vector3 _currentPoint;
    private bool _isDead = false;


    void Start()
    {
        thisEnemy = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        _currentPoint = transform.position;
    }
    private void Update()
    {
         Timer += Time.deltaTime;
         DirectionSwitch();
    }
    private void DirectionSwitch()
    {
        if (transform.position.x > _currentPoint.x)
        {
            Sprite.flipX = true;
        }
        else if (transform.position.x < _currentPoint.x)
        {
            Sprite.flipX = false;
        }
        _currentPoint.x = transform.position.x;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DamageTimer < Timer)
        {
            player.PlayerTakeDamage(EnemeyDamage);
            Timer = 0f;
        }
    }

    public void takeDamage(int dmg)
    {
        if (!_isDead) DamageNumbers(dmg);
        HP -= dmg;
        if( HP <= 0 )
        {
            _isDead = true;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        DeathAnimator.SetBool("Dead", _isDead);
        Destroy(collider2D);
        yield return new WaitForSeconds(0.19f);
        Destroy(gameObject);
    }
    public void Knockback(Vector2 direction , float Knockbackforce)
    {
        thisEnemy.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }
    public void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(DamageNambersText,transform.position,Quaternion.identity,transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();
    }

}
