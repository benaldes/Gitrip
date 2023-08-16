using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpBulletExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Gun _explosion;
    [SerializeField] private Collider2D _collider;


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
        Destroy(_collider);
        Destroy(_sprite);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
