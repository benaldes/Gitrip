using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordOfTheFliesIntro : StateMachineBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private GameObject _player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = GameObject.FindGameObjectWithTag("LordOfTheflies").gameObject;
        _cam = GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>();
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _cam.Follow = _boss.transform;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _boss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        _boss.GetComponent<LordOfTheflies>().Invincible = true;

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _cam.Follow = _player.transform;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _boss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        LordOfTheflies script = _boss.GetComponent<LordOfTheflies>();
        script.Invincible = false;
        script.IntroEnter();


    }

}
