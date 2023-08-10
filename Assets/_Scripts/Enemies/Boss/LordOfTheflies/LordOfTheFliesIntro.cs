using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordOfTheFliesIntro : StateMachineBehaviour
{
    [SerializeField] private LordOfTheflies _bossScript;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Rigidbody2D _rbBoss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = GameObject.FindGameObjectWithTag("LordOfTheflies");
        _rbBoss = _boss.GetComponent<Rigidbody2D>();
        _bossScript = _boss.GetComponent<LordOfTheflies>();
        _rbBoss.bodyType = RigidbodyType2D.Static;
        _bossScript.Invincible = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rbBoss.bodyType = RigidbodyType2D.Dynamic;
        _bossScript.Invincible = false;
        _bossScript.IntroEnter();
    }
    

}
