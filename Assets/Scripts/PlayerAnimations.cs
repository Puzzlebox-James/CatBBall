using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimator;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        TyperManager.Instance.OnBallPickedUp += BallPickUpAnimationPlay;
        //TyperManager.Instance.OnBallRunBackSpawned += SetBubble;
        //TyperManager.Instance.OnFriskBallThrow += SetPainBubble;
        //TyperManager.Instance.OnPainBubbleChange += UpdatePainBubble;
    }

    void OnDisable()
    {
        TyperManager.Instance.OnBallPickedUp -= BallPickUpAnimationPlay;
        //TyperManager.Instance.OnBallRunBackSpawned -= SetBubble;
        //TyperManager.Instance.OnFriskBallThrow -= SetPainBubble;
       //TyperManager.Instance.OnPainBubbleChange -= UpdatePainBubble;
    }


    void BallPickUpAnimationPlay(Typer.typerWordType type)
    {
        _playerAnimator.ResetTrigger("BallThrowable");
        _playerAnimator.SetTrigger("BallThrowable");
    }


}
