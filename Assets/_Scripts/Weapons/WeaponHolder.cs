using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public int M_Ammo = 25;
    public int L_Ammo = 100;
    private int _weaponInHand = 0;
    private PlayerScript _playerScript;
    public DataUnityEvent GunShot;

    private void Awake()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    private void Start()
    {
        SwitchWeapons();
    }
    void Update()
    {
        if (_playerScript._playerIsDead) Destroy(gameObject);
        int formerWeapon = _weaponInHand;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_weaponInHand >= transform.childCount - 1) { _weaponInHand = 0; }
            else { _weaponInHand++; }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_weaponInHand <= 0) { _weaponInHand = transform.childCount - 1; }
            else { _weaponInHand--; }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { _weaponInHand = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _weaponInHand = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _weaponInHand = 2; }
        if (formerWeapon != _weaponInHand) { SwitchWeapons(); }

    }
    private void SwitchWeapons()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == _weaponInHand) { weapon.gameObject.SetActive(true); }
            else { weapon.gameObject.SetActive(false); }
            i++;
        }
    }
    public void GunFired()
    {
        GunShot.Invoke(this,this);
    }
    public void PickUpAmmo(Component component, object data)
    {
        int[] ammo = data as int[];
        if (ammo[0] == 1)  M_Ammo += ammo[1];
        else if (ammo[0] == 2) L_Ammo += ammo[1];
        GunFired();
    }
}
