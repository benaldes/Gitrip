using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector3 Diraction;
    public DataUnityEvent GamePaused;
    public  void PlayerInputFunc()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Diraction = new Vector3 (horizontal, vertical);
       if (Input.GetKeyDown(KeyCode.Escape)) GamePaused.Invoke(this,this);
    }
}
