using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpPanal : MonoBehaviour
{
    public PlayerScript Player;
    public List<string> LevelUpStats = new List<string>();
    public HashSet<string> StatsList = new HashSet<string>();
    public TextMeshProUGUI Button1;
    public TextMeshProUGUI Button2;
    public TextMeshProUGUI Button3;
    private List<string> StatsToLevel = new List<string>();
    public AudioSource LevelUpSound;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    public void LevelUp()
    {
        LevelUpSound.Play();
        Time.timeScale = 0f;
        while (StatsList.Count < 3)
        {
            StatsList.Add(LevelUpStats[Random.Range(0, LevelUpStats.Count - 1)]);
        }
        foreach (string s in StatsList)
        {
            StatsToLevel.Add(s);
        }
        Button1.text = StatsToLevel[0];
        Button2.text = StatsToLevel[1];
        Button3.text = StatsToLevel[2];
    }
    public void AddStats(string Choice)
    {
        switch (Choice)
        {
            case "DMG":
                Player.PlayerDamage += 2;
                break;
            case "SPD":
                Player.WalkSpeed += 10;
                break;
            case "HP":
                Player.MaxHP += 10;
                Player.HP += 10;
                break;
            case "AS":
                Player.AttackSpeed += 2;
                break;
            case "DC":
                Player.DodgeChance += 5;
                break;
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Button_1()
    {
        AddStats(Button1.text);
    }
    public void Button_2()
    {
        AddStats(Button2.text);
    }
    public void Button_3()
    {
        AddStats(Button3.text);
    }
}
