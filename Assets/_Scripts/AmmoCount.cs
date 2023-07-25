using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI Text;
    //public TextMeshPro Text;
    public shot Revolver;
    private void Start()
    {
        updateAmmo();

    }
    public void updateAmmo()
    {
        Text.text = "Ammo: " + Revolver.Ammo;
    }
}
