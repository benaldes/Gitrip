using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FatMen : AbstractEnemy
{
    public int Damage = 30;
    public float ExplosionRadios = 2;
    public float ExplosionDelay = 1;
    public int ExpGain = 2;

    private bool _isDead = false;

    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explode;
    [SerializeField] private GameObject _explosion;
    private void Start()
    {
        _playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
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
        _animator.SetTrigger("Death");
        Destroy(_collider2D);
        _playerScript.Experience += ExpGain;
        Destroy(gameObject,0.19f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explode());
        }
    }
    private IEnumerator Explode()
    {
        _animator.SetTrigger("Explode");
        _explosion.SetActive(true);
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(ExplosionDelay);
        _explosion.SetActive(true);
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, ExplosionRadios, _playerLayers);
        _playerScript.PlayerTakeDamage(Damage);
        Destroy(_collider2D);
        _audioSource.clip = _explode;
        _audioSource.Play();
        yield return new WaitForSeconds(1);
        Die();
    }


}
