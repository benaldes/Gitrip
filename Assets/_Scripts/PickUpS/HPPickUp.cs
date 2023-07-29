using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickUp : MonoBehaviour
{
    private PlayerScript _playerScript;
    private AudioSource _hpPickUpSound;

    private void Awake()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _hpPickUpSound = GameObject.Find("HP").GetComponent<AudioSource>();
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
        _hpPickUpSound.Play();
        _playerScript.HP += 30;

    }
}
