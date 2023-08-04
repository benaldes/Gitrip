using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int EnemeyDamage = 10;
    public int HP = 10;
    public float DamageTimer = 1f;
    public int ExpGain = 1;
    private float Timer = 0f;

    [SerializeField] private GameObject _DamageNambersText;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _deathAnimator;
    private Collider2D _collider2D;
    private PlayerScript _player;
    private Rigidbody2D _thisEnemy;
    private Vector3 _currentPoint;
    private bool _isDead = false;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _thisEnemy = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Start()
    {
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
        _deathAnimator.SetBool("Dead", _isDead);
        Destroy(_collider2D);
        _player.Experience += ExpGain;
        yield return new WaitForSeconds(0.19f);
        Destroy(gameObject);
        
    }
    public void Knockback(Vector2 direction , float Knockbackforce)
    {
        _thisEnemy.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }
    public void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(_DamageNambersText, transform.position,Quaternion.identity,transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();
    }

}
