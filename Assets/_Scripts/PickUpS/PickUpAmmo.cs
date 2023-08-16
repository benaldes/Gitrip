using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public AudioSource ammoPickUpSound;
    public int _ammo = 50;
    public int _ammoType = 1;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    public DataUnityEvent AmmoPickUp;


    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
         {
            PickUp();
            Destroy(_spriteRenderer);
            Destroy(_collider);
            Destroy(gameObject, 1);
        }
        
    }
    public void PickUp()
    {
        int[] ammo = { _ammoType, _ammo }; ;
        ammoPickUpSound.Play();
        AmmoPickUp.Invoke(this, ammo);
    }
}
