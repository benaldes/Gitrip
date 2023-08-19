using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordOfTheFliesDeathAnim : StateMachineBehaviour
{
    [SerializeField] GameObject _bossReword;
    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(_bossReword);
    }
    

   
}
