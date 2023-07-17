using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickUp : MonoBehaviour
{
    public PlayerMovment Player;
    public AudioSource AmmoPickUp;

    private void Start()
    {
        AmmoPickUp = GameObject.Find("HP").GetComponent<AudioSource>();
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
        AmmoPickUp.Play();
        Player.HP += 30;
    }
}
