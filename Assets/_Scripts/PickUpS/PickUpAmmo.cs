using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    [SerializeField] int _ammo = 50;
    public AudioSource ammoPickUpSound;
    [SerializeField] private PlayerScript _playerScript;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        ammoPickUpSound = GameObject.Find("AmmoCount").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
         {
            PickUp();
            Destroy(gameObject);
         }
        
    }
    public void PickUp()
    {
        ammoPickUpSound.Play();
        _playerScript.AmmoCount += _ammo;
    }
}
