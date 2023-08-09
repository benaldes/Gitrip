using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private TextMeshProUGUI _deathMenuScore;

    public void SetPauseMenu(Component component, object data)
    {
        if(_pauseMenu.activeSelf == false) {  _pauseMenu.SetActive(true); }
        else { _pauseMenu.SetActive(false); }   
    }
    public void SetDeathMenu(Component component, object data)
    {
        _deathMenu.SetActive(true);
        PlayerScript player = component as PlayerScript;
        _deathMenuScore.text = player.Experience.ToString();
    }
}
