using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : AbstractEnemy
{
    public int EnemeyDamage = 10;
    public int ExpGain = 1;
    
    [SerializeField] private float DamageTimer = 1f;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _deathAnimator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private PlayerScript _player;
    [SerializeField] private Rigidbody2D _thisEnemy;
    private Vector3 _currentPoint;
    private bool _isDead = false;
    private float Timer = 0f;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        _currentPoint = transform.position;
    }
    public override void TakeDamage(int damage)
    {
        if (!_isDead) DamageNumbers(damage);
        hp -= damage;
        if (hp <= 0)
        {
            _isDead = true;
            Die();
        }
    }
    public override void Die()
    {
        _deathAnimator.SetBool("Dead", _isDead);
        Destroy(_collider2D);
        _player.Experience += ExpGain;
        Destroy(gameObject,0.19f);
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
            _sprite.flipX = true;
        }
        else if (transform.position.x < _currentPoint.x)
        {
            _sprite.flipX = false;
        }
        _currentPoint.x = transform.position.x;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && DamageTimer < Timer)
        {
            _player.PlayerTakeDamage(EnemeyDamage);
            Timer = 0f;
        }
    }
    
    public void Knockback(Vector2 direction , float Knockbackforce)
    {
        _thisEnemy.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }
  

}
