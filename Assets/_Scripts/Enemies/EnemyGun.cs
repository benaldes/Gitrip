using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public int _phase = -1;
    #region  Gun Stats
    [SerializeField] private int _dmg = 10;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private int _bulletSpeedMin = 10;
    [SerializeField] private int _bulletSpeedMax = 20;
    [SerializeField] private int _ammoCostForShot;
    [SerializeField] private int _bulletSpread = 10;
    [SerializeField] private int _numberOfBullets = 1;
    [SerializeField] private float _bulletRange = 3;
    [SerializeField] private bool _isWeapon = true;
    #endregion
    #region refrence
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _gunShotPoint;
    [SerializeField] private bool _hasSound = false;
    [SerializeField] private AudioClip _gunShotSound;
    [SerializeField] private bool _hasSprite = false;
    [SerializeField] private SpriteRenderer _gunRenderer;

    private GameObject _player;
    private LordOfTheflies _lordOfThefliesScript;
    private Camera _camera;
    private Rigidbody2D _currentWeaponRigidbody2D;
    private AudioSource _audioSource;
    private List<int> angleList = new List<int>();
    private int _gunMode;
    #endregion
    #region Timers
    private float _switchGunModeTime = 0;
    private float _gunMode1Timer = 0;
    private float _gunMode2Timer = 0;
    #endregion

    private void Start()
    {
        _lordOfThefliesScript = _enemy.GetComponent<LordOfTheflies>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentWeaponRigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.FindAnyObjectByType<Camera>();
        
    }
    void Update()
    {
        GunRotation();
        SwitchGunMode();
        
        _gunMode1Timer += Time.deltaTime;
        _gunMode2Timer += Time.deltaTime;
        _switchGunModeTime += Time.deltaTime;
        

    }
    private void GunRotation()
    {
        _currentWeaponRigidbody2D.position = _enemy.transform.position;
        Vector3 lookDirction = _enemy.transform.position - transform.position;
        float look = Mathf.Atan2(lookDirction.y, lookDirction.x) * Mathf.Rad2Deg;
        _currentWeaponRigidbody2D.rotation = look;       
    }

    private void SwitchGunMode()
    {
        if(_switchGunModeTime > 5) 
        {
            _switchGunModeTime = 0;
            while (true)
            {
                int x = _gunMode;
                _gunMode = Random.Range(0, 2);
                if (x != _gunMode) break;
            }
        }
        if (_phase == 0) phaseOne();
        else if (_phase == 1) phaseTwo();
    }
    private void phaseOne()
    {
        switch (_gunMode)
        {
            case 0:
                GunsShotMode1Random();
                break;
            case 1:
                GunsShotMode2Sphere();
            break;
        }
            
               
    }
    public void phaseTwo()
    {
        GunsShotMode1Random();
        GunsShotMode2Sphere();
    }
    private void GunsShotMode1Random()
    {
        
            if (_gunMode1Timer > Random.Range(0.4f, 0.5f)) { }
            else return;
            _gunMode1Timer = 0;

            for (int i = Random.Range(1, 4); i > 0; i--)
            {
                _gunShotPoint.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));
                GameObject shot = Instantiate(_bullet, _gunShotPoint.transform.position, _gunShotPoint.transform.rotation);
                Rigidbody2D shotrigidbody2D = shot.GetComponent<Rigidbody2D>();
                EnemyBullet bullet = shot.GetComponent<EnemyBullet>();
                shotrigidbody2D.AddForce(_gunShotPoint.transform.right * Random.Range(_bulletSpeedMin, _bulletSpeedMax + 1), ForceMode2D.Impulse);
                bullet.Dmg = _dmg;
                bullet.BulletRange = _bulletRange;
            }
        
        
    }
    private void GunsShotMode2Sphere() 
    {
        if (_gunMode2Timer > 1) { }
        else return;
        _gunMode2Timer = 0;
        for(int i = 0; i<360;i+=10)
        {
            _gunShotPoint.transform.localRotation = Quaternion.Euler(0, 0, i);
            GameObject shot = Instantiate(_bullet, _gunShotPoint.transform.position, _gunShotPoint.transform.rotation);
            Rigidbody2D shotrigidbody2D = shot.GetComponent<Rigidbody2D>();
            EnemyBullet bullet = shot.GetComponent<EnemyBullet>();
            shotrigidbody2D.AddForce(_gunShotPoint.transform.right * Random.Range(_bulletSpeedMin, _bulletSpeedMax + 1), ForceMode2D.Impulse);
            bullet.Dmg = _dmg;
            bullet.BulletRange = _bulletRange;
        }
    }

    
}
