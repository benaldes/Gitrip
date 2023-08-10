using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSoundIntrvel = 0.4f;
    private float walkSoundTimer = 0f;

    [SerializeField, HideInInspector] private AudioClip GrassWalkSound;
    [SerializeField, HideInInspector] private Animator PlayerAnimator;
    [SerializeField, HideInInspector] private PlayerScript _playerScript;
    [SerializeField, HideInInspector] private Rigidbody2D _playerRigidbody2D;
    [SerializeField, HideInInspector] private AudioSource _playerAudioSource;

    private void FixedUpdate()
    {
        Vector3 diraction = _playerScript.GetDiraction();
        _playerRigidbody2D.AddForce(diraction * _playerScript.WalkSpeed);
        WalkAnimation(diraction);

    }
    private void WalkAnimation(Vector3 diraction) 
    {
        if ( diraction.x != 0 || diraction.y != 0)
        {
            PlayerAnimator.SetFloat("Horizontal", diraction.x);
            PlayerAnimator.SetFloat("Vertical", diraction.y);
            if (walkSoundTimer > walkSoundIntrvel)
            {
                _playerAudioSource.clip = GrassWalkSound;
                _playerAudioSource.Play();
                walkSoundTimer = 0;
            }
            else
            {
                walkSoundTimer += Time.deltaTime;
            }

        }
        PlayerAnimator.SetFloat("speed", diraction.x * diraction.x + diraction.y * diraction.y);
    }

}
