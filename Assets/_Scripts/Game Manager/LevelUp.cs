using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject LevelUpPanal;

    [SerializeField] private TextMeshProUGUI LevelUpText;
    [SerializeField] PlayerScript _playerScript;
    public void Levelup()
    {
        _playerScript.Level++;
        _playerScript.ExperienceToLevelUp *= 2f;
        LevelUpText.text = ("lvl: " + _playerScript.Level);

        LevelUpPanal.SetActive(true);
        //lvlScript.LevelUp();
    }
}
