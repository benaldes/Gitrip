using TMPro;
using UnityEngine;


public class LordOfTheflies : AbstractEnemy
{
    public int MaxHP = 500;
    public int HP = 500;
    public bool _isDead = false;
    public bool Invincible = false;

    [SerializeField] private GameObject _trophy;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private EnemyGun _Gun;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _phaseTwoTrans;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private Clock _clock;
    public DataUnityEvent StartBossFight;
    public DataUnityEvent FightIsHit;
    public DataUnityEvent BossIsDead;


    
    private Vector3 _currentPoint;

    void Start()
    {
        _currentPoint = transform.position;
        StartBossFight.Invoke(this, HP);
    }
    public override void TakeDamage(int damage)
    {
        if (!_isDead) DamageNumbers(damage);
        hp -= damage;
        if (hp <= 0)
        {
            BossIsDead.Invoke(this, this);
            _isDead = true;
            Die();
        }
    }
    public override void Die()
    {
        Instantiate(_trophy, gameObject.transform.position, Quaternion.identity);
        _clock._bossFight = false;
        Destroy(gameObject);
    }

    void Update()
    {
        DirectionSwitch();
        if (HP <= MaxHP / 2 && _Gun._phase == 0) Phase2Trans();


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
    public void takeDamage(int dmg)
    {
        if(Invincible) { return; }
        if (!_isDead) DamageNumbers(dmg);
        HP -= dmg;
        FightIsHit.Invoke(this,HP);
        if (HP <= 0)
        {
            BossIsDead.Invoke(this, this);
            _isDead = true;
            Die();
        }
    }
    public  void IntroEnter()
    {
        _Gun.enabled = true;
        _Gun._phase = 0;
    }
    
    private void Phase2Trans()
    {
        _audioSource.clip = _phaseTwoTrans;
        _audioSource.Play();
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
        _playerScript._invincible = true;
        Invincible = true;
        _Gun._phase = -1;
        _animator.SetTrigger("PhaseTwo");
    }
    public void PhaseTwo()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _playerScript._invincible = false;
        Invincible = false;
        _Gun._phase = 1;
    }
}
