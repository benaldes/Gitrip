using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public int HP = 100;
    public int PlayerDamage = 10;
    public float speed = 500;
    public float AttackRange = 0.5f;
    public float KnockbackForce = 5;
    public float meleeAttackSpeed = 1;
    public float meleeAttackTimer = 0;
    public float WalkSoundintrvel = 2f;
   

    public Animator animator;
    public HPBar HPbar;
    public GameObject meleeAtackShow;
    public Vector3 direction;
    public Camera cam;
    public LayerMask EnemeyLayers;
    public GameObject Guntransform;
    public Rigidbody2D Gun;
    public AudioSource MeleeAttackSound;
    public AudioSource PlayerGetHitSound;
    public AudioSource PlayerDeathSound;
    public AudioSource PlayerGrassWalk;

    private float walksoundtimer;
    private Vector2 Move;
    private bool PlayerDeathSoundBool = true;
    private Vector2 MousePositon;
    [SerializeField] private Rigidbody2D player;
    

    
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        
        Move = new Vector2(Horizontal, Vertical);
        
        if(Horizontal != 0 || Vertical != 0) 
        {
            direction = new Vector3(Horizontal, Vertical,0);
            animator.SetFloat("Horizontal", Horizontal);
            animator.SetFloat("Vertical", Vertical);
            if(walksoundtimer > WalkSoundintrvel)
            {
                PlayerGrassWalk.Play();
                walksoundtimer = 0;
            }
            else
            {
                walksoundtimer += Time.deltaTime;
            }
           


        }
        animator.SetFloat("speed", Horizontal * Horizontal + Vertical * Vertical);

        if(Input.GetKeyDown(KeyCode.Space)) 
        {           
            MeleeAttack();     
        }
        if(HP <= 0 && PlayerDeathSoundBool) 
        {
            PlayerDeathSound.Play();
            PlayerDeathSoundBool = false;
        }
        HPbar.SetHPSlider(HP);
        meleeAttackTimer += Time.deltaTime;
        Revolver();
    }
    
    void FixedUpdate()
    {
        player.AddForce(Move * speed);
    }
    private void Revolver()
    {
        Gun.position = player.position + new Vector2(0.5f,0);
        MousePositon = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookmouse = MousePositon - Gun.position;
        float look = Mathf.Atan2(lookmouse.y, lookmouse.x) * Mathf.Rad2Deg;
        Gun.rotation = look;
        if((look > 90 &&  look < 180) || (look > -180 && look < -90))
        {
            Guntransform.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            Guntransform.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
    public void MeleeAttack()
    {
        if (meleeAttackTimer > meleeAttackSpeed)
        {
            meleeAttackTimer = 0;
        }
        else
        {
            return;
        }
        MeleeAttackSound.Play();   
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(transform.position + direction, AttackRange, EnemeyLayers);
        Instantiate(meleeAtackShow, transform.position + direction, transform.rotation);

        foreach(Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(PlayerDamage);
            enemy.GetComponent<Enemy>().Knockback(direction , KnockbackForce);
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + direction, AttackRange);
    }
    public void PlayerTakeDamage(int Damage)
    {
        PlayerGetHitSound.Play();                                                                 
        HP -= Damage;
        
    }
    
}
