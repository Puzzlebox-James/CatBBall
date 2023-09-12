using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private BallRunBack ballReturnerPrefab;
    
    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }
    public bool IsLaunched { get; set; }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("BallDestroyer9000"))
        {
            Miss();
        }

        if (other.CompareTag("Make"))
        {
            Make();
        }
    }

    public IEnumerator StuckCheck()
    {
        while (IsLaunched)
        {
            var currentPos = _rigidbody2D.position;
            yield return new WaitForSeconds(1);
            if (currentPos == _rigidbody2D.position)
            {
                Miss();
            }
        }
    }

    private void Miss()
    {
        SpawnRunBacker();
    }

    private void Make()
    {
        SpawnRunBacker();
        Meters.Instance.Pace += Meters.Instance.PaceMakeBonus;
        Meters.Instance.Friskiness += Meters.Instance.FriskMakeBonus;
        
        Score.Instance.Scored();
    }

    
    private void SpawnRunBacker()
    {
        Destroy(this.gameObject);
        var ballRunBacker = Instantiate(ballReturnerPrefab, transform.position, Quaternion.identity);
        ballRunBacker.Rigidbody2D.velocity = Rigidbody2D.velocity;
        
        if(TyperManager.Instance.IsHurt) return;
        TyperManager.Instance.OnChange?.Invoke();
        TyperManager.Instance.OnBallRunBackSpawned?.Invoke(Typer.typerWordType.pspsps);
    }
}
