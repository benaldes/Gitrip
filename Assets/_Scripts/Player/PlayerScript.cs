using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Player Stats
    public int MaxHP = 100;
    public int HP = 100;
    public int PlayerDamage = 10;
    public int WalkSpeed = 50;
    public float AttackSpeed = 10;
    public int DodgeChance = 10;

    public int Level = 1;
    public int Experience = 0;
    public float ExperienceToLevelUp = 10;
    #endregion

    private PlayerInput _playerInput;
    private PlayerCollision _playerCollision;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCollision = GetComponent<PlayerCollision>();
        
    }
    void Update()
    {
        
    }
    public Vector3 GetDiraction()
    {
        return _playerInput.Diraction;
    }
}

