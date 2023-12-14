using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        TyperManager.Instance.OnBallPickedUpAnimation += BallPickUpAnimationPlay;
        TyperManager.Instance.OnBallReachedHurtPlayer += BallThrowableAnimationStateSet;
        TyperManager.Instance.OnNormalBallThrow += BallThrowNormalAnimationPlay;
        TyperManager.Instance.OnHurtBallThrowAnimation += BallThrownHurtAnimationPlay;
        TyperManager.Instance.OnBallRunBackSpawned += BallRunbackCrouchAnimationPlay;
        TyperManager.Instance.OnHealedAnimations += HealedAnimationPlay;
    }

    private void OnDisable()
    {
        TyperManager.Instance.OnBallPickedUpAnimation -= BallPickUpAnimationPlay;
        TyperManager.Instance.OnBallReachedHurtPlayer -= BallThrowableAnimationStateSet;
        TyperManager.Instance.OnNormalBallThrow -= BallThrowNormalAnimationPlay;
        TyperManager.Instance.OnHurtBallThrowAnimation -= BallThrownHurtAnimationPlay;
        TyperManager.Instance.OnBallRunBackSpawned += BallRunbackCrouchAnimationPlay;
        TyperManager.Instance.OnHealedAnimations -= HealedAnimationPlay;
    }



    private void BallPickUpAnimationPlay()
    {
        _playerAnimator.SetBool("BallThrowable", true);
        _playerAnimator.ResetTrigger("BallReachedPlayer");
        _playerAnimator.SetTrigger("BallReachedPlayer");
    }

    private void BallThrowableAnimationStateSet()
    {
        _playerAnimator.SetBool("BallThrowable", true);
    }

    private void BallThrowNormalAnimationPlay()
    {
        _playerAnimator.SetBool("BallThrowable", false);
        _playerAnimator.ResetTrigger("BallLaunched");
        _playerAnimator.SetTrigger("BallLaunched");
    }

    private void BallThrownHurtAnimationPlay()
    {
        _playerAnimator.SetBool("BallThrowable", false);
        _playerAnimator.ResetTrigger("BallLaunchedHurt");
        _playerAnimator.SetTrigger("BallLaunchedHurt");
    }


    private void BallRunbackCrouchAnimationPlay(Typer.typerWordType junk)
    {
        _playerAnimator.ResetTrigger("BallReachedGround");
        _playerAnimator.SetTrigger("BallReachedGround");
    }


    private void HealedAnimationPlay()
    {
        _playerAnimator.ResetTrigger("HurtCured");
        _playerAnimator.SetTrigger("HurtCured");
    }

}
