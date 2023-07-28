using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _dmg;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] private int _ammoCostForShot;
    [SerializeField] private int _bulletSpread;
    [SerializeField] private int _numberOfBullets;
    private float _gunShotTimer;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _gunShotPoint;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioClip _gunShotSound;

    private Rigidbody2D _currentWeaponRigidbody2D;
    private PlayerScript _playerScript;
    private AudioSource _audioSource;

    public Vector3 test;

    private void Awake()
    {
        _playerScript = _player.GetComponent<PlayerScript>();
        _currentWeaponRigidbody2D = _gun.GetComponent<Rigidbody2D>();
        _audioSource = _gun.GetComponent<AudioSource>();
        _camera = Camera.FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        test = _gun.transform.right;
        GunRotation();
        if (Input.GetButton("Fire1")) GunShot();
        _gunShotTimer += Time.deltaTime;
    }
    private void GunRotation()
    {
        _currentWeaponRigidbody2D.position = _player.transform.position + new Vector3(0.5f, 0);
        Vector3 MousePositon = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookmouse = MousePositon - _gun.transform.position;
        float look = Mathf.Atan2(lookmouse.y, lookmouse.x) * Mathf.Rad2Deg;
        _currentWeaponRigidbody2D.rotation = look;
        if ((look > 90 && look < 180) || (look > -180 && look < -90))
        {
            _gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            _gun.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
    public void GunShot()
    {
        if (_gunShotTimer > _fireRate && _playerScript.AmmoCount >= _ammoCostForShot) { }
        else return;
        _gunShotTimer = 0;
        _audioSource.clip = _gunShotSound;
        _audioSource.Play();
        _playerScript.AmmoCount -= _ammoCostForShot;
        /*
        if(_numberOfBullets%2 == 0)
        {
            float spreadStart = _bulletSpread/2;
            for (int i = _numberOfBullets; i >= 0; i -= 2)
            {
                GameObject shot = Instantiate(_bullet, _gunShotPoint.transform.position, _gun.transform.rotation);
                Rigidbody2D shotrigidbody2D = shot.GetComponent<Rigidbody2D>();
                shotrigidbody2D.AddForce(_gun.transform.right * _bulletSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {

        }
        */
        
        GameObject bullet = Instantiate(_bullet, _gunShotPoint.transform.position , _gun.transform.rotation);
        Rigidbody2D bulletrig = bullet.GetComponent<Rigidbody2D>();
        //_gunShotPoint.transform.eulerAngles.z = 45f;
        bulletrig.AddForce(_gunShotPoint.transform.right * _bulletSpeed  , ForceMode2D.Impulse);
        
        

    }
}
