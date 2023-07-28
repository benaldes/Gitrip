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
    #endregion
    #region refrence
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _gunShotPoint;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioClip _gunShotSound;
    private Rigidbody2D _currentWeaponRigidbody2D;
    private PlayerScript _playerScript;
    private AudioSource _audioSource;
    #endregion

    private float _gunShotTimer;
    public Vector3 test;
    public List<int> angleList = new List<int>();
    public List<int> testint = new List<int>();
    public int check = 0;

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
        angleList.Clear();
        testint.Clear();
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
                testint.Add(i);
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
            GameObject shot = Instantiate(_bullet, _gunShotPoint.transform.position, _gun.transform.rotation);
            Rigidbody2D shotrigidbody2D = shot.GetComponent<Rigidbody2D>();
            shotrigidbody2D.AddForce(_gunShotPoint.transform.right * _bulletSpeed, ForceMode2D.Impulse);
        }

    }
}
