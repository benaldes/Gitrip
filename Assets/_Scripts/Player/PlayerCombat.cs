using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float AttackRange = 0.5f;
    public float KnockbackForce = 5;
    public float meleeAttackSpeed = 1;
    public float meleeAttackTimer = 0;
    public float WalkSoundintrvel = 2f;
    public float BulletForce = 1f;
    

    public GameObject CurrentWeapon;
    public GameObject Bullet;

    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private AudioClip _meleeAttackSound;
    [SerializeField] private AudioClip _RevolverGunShotSound;
    [SerializeField] private GameObject MeleeAttackSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LayerMask EnemeyLayers;
    [SerializeField] private Rigidbody2D _CurrentWeaponRigidbody2D;
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject GunShotPoint;

    private float _gunShotTimer;
    
    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
  
    public void CombatUpdate()
    {
        meleeAttackTimer += Time.deltaTime;
        _gunShotTimer += Time.deltaTime;

        Gun();
    }
    public void MeleeAttack()
    {
        if (meleeAttackTimer > meleeAttackSpeed) meleeAttackTimer = 0;
        else return;
        Vector3 direction = _playerScript.GetDiraction();
        _audioSource.clip = _meleeAttackSound;
        _audioSource.Play();
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(transform.position + direction, AttackRange, EnemeyLayers);
        Instantiate(MeleeAttackSprite, transform.position + direction, transform.rotation);

        foreach (Collider2D enemy in hitenemies)
        {
            if (enemy.gameObject.tag == "enemy")
            {
                enemy.GetComponent<Enemy>().takeDamage(_playerScript.PlayerDamage);
                enemy.GetComponent<Enemy>().Knockback(direction, KnockbackForce);
            }
            else if (enemy.gameObject.tag == "FatMen")
            {
                enemy.GetComponent<FatMen>().takeDamage(_playerScript.PlayerDamage);
                enemy.GetComponent<FatMen>().Knockback(direction, KnockbackForce);
            }
            else if (enemy.gameObject.tag == "Sklislime")
            {
                enemy.GetComponent<Sklislime>().takeDamage(_playerScript.PlayerDamage);
                enemy.GetComponent<Sklislime>().Knockback(direction, KnockbackForce);
            }


        }
    }
    private void Gun()
    {
        _CurrentWeaponRigidbody2D.position =  _player.transform.position + new Vector3(0.5f, 0);
        Vector3 MousePositon = _Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookmouse = MousePositon - CurrentWeapon.transform.position;
        float look = Mathf.Atan2(lookmouse.y, lookmouse.x) * Mathf.Rad2Deg;
        _CurrentWeaponRigidbody2D.rotation = look;
        if ((look > 90 && look < 180) || (look > -180 && look < -90))
        {
            CurrentWeapon.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            CurrentWeapon.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
    public void GunShot()
    {
        if (_gunShotTimer > _playerScript.ActualAttackSpeed) { }
        else return;
        _gunShotTimer = 0;
        _audioSource.clip = _RevolverGunShotSound;
        _audioSource.Play();
        _playerScript.AmmoCount -= 1;
        GameObject bullet = Instantiate(Bullet, GunShotPoint.transform.position, CurrentWeapon.transform.rotation);
        Rigidbody2D bulletrig = bullet.GetComponent<Rigidbody2D>();
        bulletrig.AddForce(CurrentWeapon.transform.right * BulletForce, ForceMode2D.Impulse);

    }
}
