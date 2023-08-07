using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _shotgunBulletText;
    [SerializeField] private TextMeshProUGUI _rifleBulletText;

    public void UpdateAmmo(Component component, object data)
    {
        WeaponHolder ammo = component as WeaponHolder;
        _shotgunBulletText.text = ammo.M_Ammo.ToString();
        _rifleBulletText.text = ammo.L_Ammo.ToString();
    }
    
}

