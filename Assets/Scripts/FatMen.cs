using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FatMen : MonoBehaviour
{
    public int HP = 50;
    public int Damage = 30;
    public float ExplosionRadios = 2;
    public float ExplosionDelay = 1;
    public int ExpGain = 2;

    private bool _isDead = false;

    [SerializeField] private PlayerMovment _playerMovment;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private GameObject _DamageNambersText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explode;
    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        _playerMovment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
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
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(ExplosionDelay);
        _explosion.SetActive(true);
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(transform.position, ExplosionRadios, _playerLayers);
        foreach (Collider2D player in hitplayer)
        {
            player.GetComponent<PlayerMovment>().PlayerTakeDamage(Damage);
        }
        Destroy(_collider2D);
        //Destroy(_rigidbody2D);
        _audioSource.clip = _explode;
        _audioSource.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void Knockback(Vector2 direction, float Knockbackforce)
    {
        _rigidbody2D.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }
    public void takeDamage(int dmg)
    {
        if (!_isDead) DamageNumbers(dmg);
        HP -= dmg;
        if (HP <= 0)
        {
            _isDead = true;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        _animator.SetTrigger("Death");
        Destroy(_collider2D);
        _playerMovment.Experience += ExpGain;
        yield return new WaitForSeconds(0.19f);        
        Destroy(gameObject);
    }
    public void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(_DamageNambersText, transform.position, Quaternion.identity, transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();
    }
}
