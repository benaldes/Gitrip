using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 Diraction;

    private PlayerScript _playerScript;

    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
    public  void PlayerInputFunc()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Diraction = new Vector3 (horizontal, vertical);

        if(Input.GetKeyDown(KeyCode.Escape)) _playerScript.PauseMenu();       
        
    }
}
