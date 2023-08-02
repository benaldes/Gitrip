using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class LordOfTheflies : MonoBehaviour
{
    public int MaxHP = 500;
    public int HP = 500;
    public bool _isDead = false;
    public bool Invincible = false;


    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _DamageNambersText;
    [SerializeField] private EnemyGun _Gun;
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private AudioClip _phaseTwoTrans;

    private GameObject _player;
    private Vector3 _currentPoint;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentPoint = transform.position;
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
        if (HP <= 0)
        {
            _isDead = true;
            Death();
        }
    }
    public void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(_DamageNambersText, transform.position, Quaternion.identity, transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    public  void IntroEnter()
    {
        _Gun._phase = 0;
    }
    
    private void Phase2Trans()
    {
        gameObject.GetComponent<AudioSource>().clip = _phaseTwoTrans;
        gameObject.GetComponent<AudioSource>().Play();
        _cam.Follow = gameObject.transform;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _player.GetComponent<PlayerScript>()._invincible = true;
        Invincible = true;
        _Gun._phase = -1;
        _animator.SetTrigger("PhaseTwo");
    }
    public void PhaseTwo()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.GetComponent<PlayerScript>()._invincible = false;
        _cam.Follow = _player.transform;
        Invincible = false;
        _Gun._phase = 1;
    }
}
