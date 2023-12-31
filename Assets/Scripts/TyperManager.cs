using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyperManager : MonoBehaviour
{
    public static TyperManager Instance;

    public bool IsHurt { get; set; }
    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }
    
    // Invoked by: BallRunBack, Typer
    public Action<Typer.typerWordType> OnBallPickedUp;

    // Invoked by: BallRunBack
    public Action OnBallPickedUpAnimation;

    // Invoked by: BallRunBack
    public Action OnBallReachedHurtPlayer;

    // Invoked by: Ball
    public Action<Typer.typerWordType> OnBallRunBackSpawned;

    // Invoked by: BallThrower
    public Action<Typer.typerWordType> OnFriskBallThrow;

    // Invoked by: BallThrower
    public Action OnHurtBallThrowAnimation;

    // Invoked by: BallThrower
    public Action OnNormalBallThrow;
    
    // Invoked by: Ball, BallRunBack, BallThrower
    public Action OnChange;

    // Invoked by: Typer
    public Action<int> OnPainBubbleChange;

    // Invoked by: Typer
    public Action OnLoveWordComplete;

    //Invoked by: Typer
    public Action OnHealedAnimations;
}
