using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public shot Revolver;
    public AmmoCount ammoCount;
    public AudioSource ammoPickUpSound;

    private void Start()
    {
        Revolver = GameObject.FindGameObjectWithTag("Revolver").GetComponent<shot>();
        ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<AmmoCount>();
        ammoPickUpSound = GameObject.Find("AmmoCount").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
         {
            PickUp();
            ammoCount.updateAmmo();
            Destroy(gameObject);
         }
        
    }
    public void PickUp()
    {
        ammoPickUpSound.Play();
        Revolver.Ammo += 5;
    }
}
