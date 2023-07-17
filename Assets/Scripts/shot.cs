using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletForce = 1.0f;
    public int Ammo = 10;
    public AmmoCount ammoCount;
    public AudioSource GunShot;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Ammo > 0)    
        {
            Shot();
        }
       
    }
    private void Shot()
    {
        GunShot.Play();
        Ammo -= 1;
        ammoCount.updateAmmo();
        GameObject bullet = Instantiate(Bullet, gameObject.transform.position,gameObject.transform.rotation);
        Rigidbody2D bulletrig = bullet.GetComponent<Rigidbody2D>();
        bulletrig.AddForce(gameObject.transform.right * BulletForce, ForceMode2D.Impulse);

    }
}
