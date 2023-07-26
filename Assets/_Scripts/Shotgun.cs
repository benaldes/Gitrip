using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletForce = 1.0f;
    //public AmmoCount ammoCount;
    public AudioSource GunShot;
    public PlayerScript player;
    public GameObject PlayerTransform;
    public GameObject FirePoint;
    private Vector2 MousePositon;
    public Camera cam;
    public Rigidbody2D Gun;
    public SpriteRenderer ShotgunSprite;


    private float _attackSpeedTimer = 0f;
    [SerializeField] private PlayerScript _playerScript;
    void Update()
    {
        if (Input.GetButton("Fire1") && _playerScript.AmmoCount > 0 && _attackSpeedTimer > player.ActualAttackSpeed)
        {
            _attackSpeedTimer = 0f;
            Shot();
        }
        _attackSpeedTimer += Time.deltaTime;
        Revolver();
        

    }
    private void Shot()
    {
        GunShot.Play();
        //Ammo.Ammo -= 5;
        GameObject bullet = Instantiate(Bullet, FirePoint.transform.position, FirePoint.transform.rotation);
        Rigidbody2D bulletrig = bullet.GetComponent<Rigidbody2D>();
        bulletrig.AddForce(FirePoint.transform.right * BulletForce, ForceMode2D.Impulse);

    }
    private void Revolver()
    {
        gameObject.transform.position = PlayerTransform.transform.position + new Vector3(0.1f, 0);
        MousePositon = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookmouse = MousePositon - Gun.position;
        float look = Mathf.Atan2(lookmouse.y, lookmouse.x) * Mathf.Rad2Deg;
        Gun.rotation = look;
        if ((look > 90 && look < 180) || (look > -180 && look < -90))
        {
            ShotgunSprite.flipY = true;
        }
        else
        {
            ShotgunSprite.flipY = false;
        }
    }
}
