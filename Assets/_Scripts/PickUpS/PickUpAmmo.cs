using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public AudioSource ammoPickUpSound;
    [SerializeField] private int _ammo = 50;
    [SerializeField] private int _ammoType = 1;
    public DataUnityEvent AmmoPickUp;

    private void Start()
    {
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
        int[] ammo = {_ammoType, _ammo}; 
        ammoPickUpSound.Play();
        AmmoPickUp.Invoke(this, ammo);
    }
}
