using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRunBack : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }

    // Awake getComponent's the animator attached this prefab
    private Animator animator;
    private static readonly int WalkingSpeed = Animator.StringToHash("walkingSpeed");

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.AddForce(new Vector2(-Meters.Instance.Pace, .5f));
    }

    private void Update()
    {
        animator.SetFloat(WalkingSpeed, -Rigidbody2D.velocity.x / 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Respawn"))
        {
            Destroy(this.gameObject);
            if (TyperManager.Instance.IsHurt)
            {
                // waiting for stuff animation stuff?
                Debug.Log("We should wait");
                // TyperManager.Instance.OnChange?.Invoke();
                // TyperManager.Instance.OnBallPickedUp?.Invoke(Typer.typerWordType.hurtWords);
                return;
            }
            BallCreator.Instance.SpawnBall();
            TyperManager.Instance.OnChange?.Invoke();
            TyperManager.Instance.OnBallPickedUp?.Invoke(Typer.typerWordType.loveWords);
        }
    }
}
