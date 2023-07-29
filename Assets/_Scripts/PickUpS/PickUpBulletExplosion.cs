using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpBulletExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    private PlayerScript _playerScript;
    private AudioSource _audioSource;
    private Gun _explosion;

    private void Awake()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _audioSource = GetComponent<AudioSource>();
        _explosion = GetComponent<Gun>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(PickUp());
        }

    }
    private IEnumerator PickUp()
    {
        
        _audioSource.Play();
        _explosion.GunShot();
        Destroy(GetComponent<Collider2D>());
        Destroy(_sprite);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
