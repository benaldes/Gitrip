using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int Dmg = 5;
    public int AttackSpeed = 10;
    public float AttackRange = 0.5f;
    public float KnockbackForce = 5;
    public float attacktime = 0.5f;
    private float ActualAttackSpeed;
    private float meleeAttackTimer;
    private Vector3 direction;

    [SerializeField] private GameObject _axe;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private AudioClip _meleeAttackSound;
    [SerializeField] private LayerMask EnemeyLayers;

    private AudioSource _audioSource;
    

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    private void Update()
    {
        meleeAttackTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) { MeleeAttack(); }
    }
    private void MeleeAttack()
    {
        StartCoroutine(StopAxeAnimation()); 
        ActualAttackSpeed = 5f / AttackSpeed;
        if (meleeAttackTimer > ActualAttackSpeed) meleeAttackTimer = 0;
        else return;
        direction = _playerScript.GetDiraction();
        //_audioSource.clip = _meleeAttackSound;
        //_audioSource.Play();

        _axe.SetActive(true);
        
        if (direction.y == 1) 
        {
            _axe.transform.position = _player.transform.position + new Vector3(0,2,0);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.y == -1) 
        {
            _axe.transform.position = _player.transform.position + new Vector3(0, -2, 0);
            transform.localRotation = Quaternion.Euler(0, 0, -90); 
        }
        else if (direction.x == -1) 
        { _axe.GetComponent<SpriteRenderer>().flipX = true; }
        else if (direction.x == 1) 
        { _axe.GetComponent<SpriteRenderer>().flipX = false; }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<Enemy>().takeDamage(_playerScript.PlayerDamage);
            collision.GetComponent<Enemy>().Knockback(direction, KnockbackForce);
        }
        else if (collision.gameObject.tag == "FatMen")
        {
            collision.GetComponent<FatMen>().takeDamage(_playerScript.PlayerDamage);
            collision.GetComponent<FatMen>().Knockback(direction, KnockbackForce);
        }
        else if (collision.gameObject.tag == "Sklislime")
        {
            collision.GetComponent<Sklislime>().takeDamage(_playerScript.PlayerDamage);
            collision.GetComponent<Sklislime>().Knockback(direction, KnockbackForce);
        }
    }
    private IEnumerator StopAxeAnimation()
    {
        yield return new WaitForSeconds(attacktime);
        _axe.SetActive(false);
    }
}
