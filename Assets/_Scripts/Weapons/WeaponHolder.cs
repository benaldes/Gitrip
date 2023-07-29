using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    private int _weaponInHand = 0;
    private PlayerScript _playerScript;

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
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_weaponInHand >= transform.childCount - 1) { _weaponInHand = 0; }
            else { _weaponInHand++; }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_weaponInHand <= 0) { _weaponInHand = transform.childCount - 1; }
            else { _weaponInHand--; }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) { _weaponInHand = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { _weaponInHand = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { _weaponInHand = 2; }        
        if (formerWeapon != _weaponInHand) { SwitchWeapons(); }

    }
    private void SwitchWeapons()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == _weaponInHand) { weapon.gameObject.SetActive(true); } 
            else { weapon.gameObject.SetActive(false); }
            i++;
        }
    }
}
