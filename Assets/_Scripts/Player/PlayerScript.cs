using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    public float ActualAttackSpeed;
    public int DodgeChance = 10;
    public int AmmoCount = 10;

    public int Level = 1;
    public int Experience = 0;
    public float ExperienceToLevelUp = 10;

    private bool _playerIsDead = false;
    #endregion
    #region Player Scrip
    private PlayerInput _playerInput;
    private PlayerCombat _playerCombat;
    private PlayerMovement _playerMovement;
    [SerializeField] private AudioClip _playerDeathSound;
    [SerializeField] private AudioClip _PlayerGetHitSound;
    [SerializeField] private AudioClip _playerDodgeSound;
    [SerializeField] private AudioSource _PlayerAudio;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody2D _playerRigidbody2D;
    #endregion

    [SerializeField] private TextMeshProUGUI _AmmoText;
    [SerializeField] private GameObject AboveHeadDamageText;
    [SerializeField] private TextMeshProUGUI StatsText;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject DeathPanel;

    private void Awake()
    {
        _PlayerAudio = GetComponent<AudioSource>();
        _player = GetComponent<GameObject>();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCombat = GetComponent<PlayerCombat>();
        
    }
    void Update()
    {
        if (HP > 0)
        {
            UpdateStats();
            _playerInput.PlayerInputFunc();
            _playerCombat.CombatUpdate();
        }
        else if(!_playerIsDead) StartCoroutine(Death());
  
    }
    private IEnumerator Death()
    {
        _playerIsDead = true;
        _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
        _PlayerAudio.clip = _playerDeathSound;
        _PlayerAudio.Play();
        _playerAnimator.SetTrigger("Death");
        Destroy(_playerCombat.CurrentWeapon);
        yield return new WaitForSeconds(0.40f);
        DeathPanel.SetActive(true);
    }
    private void UpdateStats()
    {
        if (HP > MaxHP) { HP = MaxHP; }
        ActualAttackSpeed = 5f / AttackSpeed;
        _AmmoText.text = AmmoCount.ToString();
        StatsText.text = "Damage: " + PlayerDamage.ToString() +
        "\nSpeed: " + WalkSpeed.ToString() +
        "\nAttack Speed: " + AttackSpeed.ToString() +
        "\nDodge Chance: " + DodgeChance.ToString();
    }
    public Vector3 GetDiraction()
    {
        return _playerInput.Diraction;
    }
    public void MeleeAttack()
    {
        _playerCombat.MeleeAttack();
    }
    public void PauseMenu()
    {
        pauseMenu.SetPauseMenu();
    }
    public void PlayerTakeDamage(int Damage)
    {
        if (DodgeCheck())
        {
            return;
        }
        if (HP > 0)
        {
            _PlayerAudio.clip = _PlayerGetHitSound;
            _PlayerAudio.Play();
            HP -= Damage;
        }
    }
    private bool DodgeCheck()
    {
        float index = Random.Range(0, 101);
        if (index < DodgeChance)
        {
            //DodgeShowText();
            _PlayerAudio.clip = _playerDodgeSound;
            _PlayerAudio.Play();
            return true;
        }

        else return false;

    }
    public void DodgeShowText()
    {
        var dodgeText = Instantiate(AboveHeadDamageText, transform.position, Quaternion.identity, transform);
        dodgeText.GetComponent<TextMeshPro>().text = "Dodge";
    }
    public void GunShot()
    {

        _playerCombat.GunShot();
    }


}

