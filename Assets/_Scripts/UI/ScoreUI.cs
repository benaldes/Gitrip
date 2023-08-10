using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;

    public void UpdateScore(Component component, object data)
    {
        PlayerScript player = component as PlayerScript;
        ScoreText.text = player.Experience.ToString();
    }
}
