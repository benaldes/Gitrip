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
   
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private AudioClip _meleeAttackSound;
    [SerializeField] private GameObject MeleeAttackSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LayerMask EnemeyLayers;
    [SerializeField] private Camera _Camera;
    
    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
  
    public void CombatUpdate()
    {
        meleeAttackTimer += Time.deltaTime;  
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
    
}
