using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sklislime : AbstractEnemy
{
    public int Damage = 30;
    public int ExpGain = 5;
    public float DamageTimer = 1f;
    public float Timer = 0f;
    public float DeathSplitTimer = 1f;

    private bool _isDead = false;
    [SerializeField] private PlayerScript _player;
    [SerializeField] private GameObject _sklislime;
    [SerializeField] private GameObject _splitParticle;
    [SerializeField] private Collider2D _sklislimeCollider2D;
    [SerializeField] private SpriteRenderer _sklislimeSprite;
    [SerializeField] private Rigidbody2D _sklislimeRigidbody2D;
    [SerializeField] private AudioClip _sklislimeSplitSound;
    [SerializeField] private AudioSource _sklislimeAudioSource;

    public override void TakeDamage(int damage)
    {
        if (!_isDead) DamageNumbers(damage);
        hp -= damage;
        if (hp <= 0)
        {
            _isDead = true;
            StartCoroutine(Death());
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Timer += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Timer > DamageTimer)
        {
            _player.PlayerTakeDamage(Damage);

            Timer = 0f;
        }
    }
    private IEnumerator Death()
    {
        _sklislimeAudioSource.clip = _sklislimeSplitSound;
        _sklislimeAudioSource.time = 0.3f;
        _sklislimeAudioSource.Play();
        if (transform.localScale.x ==  1) Size(0.7f,30,20);
        else if (transform.localScale.x == 0.7f) Size(0.5f,20,10);
        _splitParticle.SetActive(true);
        _sklislimeRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        Destroy(_sklislimeSprite); 
        Destroy(_sklislimeCollider2D);
        _player.Experience += ExpGain;
        yield return new WaitForSeconds(DeathSplitTimer);
        Die();
    }
    private void Size(float Size ,int HP,int dmg)
    {
        var split1 = Instantiate(_sklislime, transform.position + new Vector3(1, 0), Quaternion.identity);
        var split2 = Instantiate(_sklislime, transform.position + new Vector3(-1, 0), Quaternion.identity);
        split1.transform.localScale = new Vector3(Size, Size, Size);
        split2.transform.localScale = new Vector3(Size, Size, Size);
        Sklislime splitS1 = split1.GetComponent<Sklislime>();
        Sklislime splitS2 = split2.GetComponent<Sklislime>();
        splitS1.hp = HP;
        splitS2.hp = HP;
        splitS1.Damage = dmg;
        splitS2.Damage = dmg;
    }
}
