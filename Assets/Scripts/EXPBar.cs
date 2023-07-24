using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI LevelText;
    public PlayerMovment Player;
    public GameObject levelUP;
    public LeveLUP lvlScript;

    public void SetExpSlider()
    {
        slider.maxValue = Player.ExperienceToLevelUp;
        slider.value = Player.Experience;
        if(slider.value >= slider.maxValue ) 
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        Player.Level++;
        Player.ExperienceToLevelUp *= 2f;
        LevelText.text = ("lvl: " +  Player.Level);
        SetExpSlider();
        levelUP.SetActive(true);
        lvlScript.LevelUp();
    }
}
