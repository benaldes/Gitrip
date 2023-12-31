using System.Collections;
using TMPro;
using UnityEngine;



public class PlayerScript : MonoBehaviour
{
    #region Player Stats
    public int MaxHP = 100;
    public int HP = 100;
    public int PlayerDamage = 10;
    public int WalkSpeed = 50;
    public float AttackSpeed = 10; 
    public int DodgeChance = 0;
    public int Level = 1;
    public int Experience = 0;
    public int ExperienceToLevelUp = 10;
    [HideInInspector] public float ActualAttackSpeed;
    [HideInInspector] public bool _playerIsDead = false;
    [HideInInspector] public bool _invincible = false;
    #endregion
    #region Player Scrip
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private AudioSource _PlayerAudio;
    [SerializeField] private SpriteRenderer _PlayerSpriteRenderer;
    [SerializeField] private AudioClip _playerDeathSound;
    [SerializeField] private AudioClip _PlayerGetHitSound;
    [SerializeField] private AudioClip _playerDodgeSound;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private GameObject _WeaponHolder;
    #endregion
    [SerializeField] private GameObject AboveHeadDamageText;
    public DataUnityEvent PlayerTakeDmgEvent;
    public DataUnityEvent PlayerGetExp;
    public DataUnityEvent PlayerIsBorn;
    public DataUnityEvent PlayerIsDead;


    private int _expHolder = 0;

    private void Awake()
    {
        _PlayerAudio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PlayerIsBorn.Invoke(this, this);
    }
    void Update()
    {
        if (HP > 0)
        {
            UpdateStats();
            _playerInput.PlayerInputFunc();
        }
        else if (!_playerIsDead) StartCoroutine(Death());
        if (Experience != _expHolder)
        {
            _expHolder = Experience;
            PlayerGetExp.Invoke(this, ExperienceToLevelUp); 
        }

    }
    private IEnumerator Death()
    {
        _playerIsDead = true;
        _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
        _PlayerAudio.clip = _playerDeathSound;
        _PlayerAudio.Play();
        Destroy(_WeaponHolder);
        _playerAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(0.40f);
        PlayerIsDead.Invoke(this, this);
    }
    public void PlayerIntro()
    {
        _WeaponHolder.SetActive(true);
        _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
    private void UpdateStats()
    {

        if (HP > MaxHP) { HP = MaxHP; }
        ActualAttackSpeed = 5f / AttackSpeed;
    }
    public Vector3 GetDiraction()
    {
        return _playerInput.Diraction;
    }
    public void PlayerTakeDamage(int Damage)
    {
        if (_invincible) { return; }
        /*if (DodgeCheck())
        {
            return;
        }*/

        if (HP > 0)
        {
            _PlayerAudio.clip = _PlayerGetHitSound;
            _PlayerAudio.Play();
            HP -= Damage;
            PlayerTakeDmgEvent.Invoke(this, HP);
        }
    }
    /*private bool DodgeCheck()
    {
        float index = Random.Range(0, 101);
        if (index < DodgeChance)
        {
            DodgeShowText();
            _PlayerAudio.clip = _playerDodgeSound;
            _PlayerAudio.Play();
            return true;
        }

        else return false;

    }
    public void DodgeShowText()
    {
        var dodgeText = Instantiate(AboveHeadDamageText, transform.position, Quaternion.identity);
        dodgeText.GetComponent<TextMeshPro>().text = "Dodge";
    }*/
    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(3, 9, true);
        _PlayerSpriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(5);
        _PlayerSpriteRenderer.color = Color.white;
        Physics2D.IgnoreLayerCollision(3, 9, false);
    }
}

