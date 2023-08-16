using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntro : StateMachineBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerScript = animator.GetComponentInParent<PlayerScript>();
        _playerScript.PlayerIntro();
    }

   
}
