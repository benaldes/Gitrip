using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sklislime : MonoBehaviour
{
    public int HP = 20;
    public int Damage = 30;
    public int ExpGain = 5;
    public float DamageTimer = 1f;
    public float Timer = 0f;
    public float DeathSplitTimer = 1f;

    private bool _isDead = false;
    [SerializeField] private PlayerScript _player;
    [SerializeField] private GameObject _DamageNambersText;
    [SerializeField] private GameObject _sklislime;
    [SerializeField] private GameObject _splitParticle;
    [SerializeField] private Collider2D _sklislimeCollider2D;
    [SerializeField] private SpriteRenderer _sklislimeSprite;
    [SerializeField] private Rigidbody2D _sklislimeRigidbody2D;
    [SerializeField] private AudioClip _sklislimeSplitSound;
    [SerializeField] private AudioSource _sklislimeAudioSource;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
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
    public void Knockback(Vector2 direction, float Knockbackforce)
    {
        _sklislimeRigidbody2D.AddForce(direction * Knockbackforce, ForceMode2D.Force);
    }

    public void takeDamage(int dmg)
    {
        if (!_isDead) DamageNumbers(dmg);
        HP -= dmg;
        if (HP <= 0 && !_isDead)
        {
            _isDead = true;
            StartCoroutine(Death());
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
        Destroy(gameObject);
    }
    private void Size(float Size ,int HP,int dmg)
    {
        var split1 = Instantiate(_sklislime, transform.position + new Vector3(1, 0), Quaternion.identity);
        var split2 = Instantiate(_sklislime, transform.position + new Vector3(-1, 0), Quaternion.identity);
        split1.transform.localScale = new Vector3(Size, Size, Size);
        split2.transform.localScale = new Vector3(Size, Size, Size);
        split1.GetComponent<Sklislime>().HP = HP;
        split2.GetComponent<Sklislime>().HP = HP;
        split1.GetComponent<Sklislime>().Damage = dmg;
        split2.GetComponent<Sklislime>().Damage = dmg;
    }
    public void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(_DamageNambersText, transform.position, Quaternion.identity, transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();
    }
}
