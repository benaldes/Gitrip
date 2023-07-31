using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gun : MonoBehaviour
{
    #region  Gun Stats
    [SerializeField] private int _dmg;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] private int _ammoCostForShot;
    [SerializeField] private int _bulletSpread;
    [SerializeField] private int _numberOfBullets;
    [SerializeField] private float _bulletRange = 3;
    [SerializeField] private bool _isWeapon = true;
    #endregion
    #region refrence
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _gunShotPoint;
    [SerializeField] private AudioClip _gunShotSound;
    [SerializeField] private SpriteRenderer _gunRenderer;

    private GameObject _player;
    private Camera _camera;
    private Rigidbody2D _currentWeaponRigidbody2D;
    private PlayerScript _playerScript;
    private AudioSource _audioSource;
    private List<int> angleList = new List<int>();
    #endregion

    private float _gunShotTimer = 10;
    

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerScript = _player.GetComponent<PlayerScript>();
        _currentWeaponRigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        if(_isWeapon)
        {
            GunRotation();
            if (Input.GetButton("Fire1")) GunShot();
        }
        _gunShotTimer += Time.deltaTime;
    }
    private void GunRotation()
    {
        _currentWeaponRigidbody2D.position = _player.transform.position + new Vector3(0.5f, 0);
        Vector3 MousePositon = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookmouse = MousePositon - transform.position;
        float look = Mathf.Atan2(lookmouse.y, lookmouse.x) * Mathf.Rad2Deg;
        _currentWeaponRigidbody2D.rotation = look;
        if ((look > 90 && look < 180) || (look > -180 && look < -90))
        {
            _gunRenderer.flipY = true;
        }
        else
        {
            _gunRenderer.flipY = false;
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
        angleList.Clear();
        if (_numberOfBullets == 1)
        {
            angleList.Add(0);
        }
        else if (_numberOfBullets % 2 == 0)
        {
            int spreadStart = _bulletSpread / 2;
            angleList.Add(spreadStart);
            angleList.Add(-spreadStart);
            for (int i = 1; i < _numberOfBullets / 2; i++)
            {
                angleList.Add(spreadStart + (_bulletSpread * i));
                angleList.Add(-spreadStart - (_bulletSpread * i));
            }
        }
        else if (_numberOfBullets % 2 != 0)
        {
            angleList.Add(0);
            for (int i = 1; i < _numberOfBullets / 2 + 1; i++)
            {
                angleList.Add(_bulletSpread * i);
                angleList.Add(-_bulletSpread * i);
            }
        }

        foreach (int angle in angleList)
        {
            _gunShotPoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
            GameObject shot = Instantiate(_bullet, _gunShotPoint.transform.position, transform.rotation);
            Rigidbody2D shotrigidbody2D = shot.GetComponent<Rigidbody2D>();
            Bullet bullet = shot.GetComponent<Bullet>();
            shotrigidbody2D.AddForce(_gunShotPoint.transform.right * _bulletSpeed, ForceMode2D.Impulse);
            bullet.Dmg = _dmg;
            bullet.BulletRange = _bulletRange;
            
        }

    }
}
