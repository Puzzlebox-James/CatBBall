using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    public static BallCreator Instance;
    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }
    
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private Ball ballPrefab;

    private Ball activeBall;

    private void Start()
    {
        SpawnBall();
    }
    
    
    public void SpawnBall()
    {
        if (activeBall == null)
        {
            var spawnedBall = Instantiate(ballPrefab);
            activeBall = spawnedBall;
            ballThrower.Ball = spawnedBall;
        }
    }
}
