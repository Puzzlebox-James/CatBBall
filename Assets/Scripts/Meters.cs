using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
    [SerializeField] private float paceDepletionRate;
    [SerializeField] private float friskinessIncreaseRate;
    
    [SerializeField] public float PaceMakeBonus { get; set; }
    [SerializeField] public float FriskMakeBonus { get; set; }
    
    public static Meters Instance;
    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }


    private float _pace = 3f;
    public float Pace
    {
        get => _pace;
        set
        {
            if (value < 2)
                value = 2;
            if (value > 13)
                value = 13;
            _pace = value;
        }
    }
    public void ChangePace(float amount)
    {
        Pace += amount;
    }

    
    private float _friskiness;
    public float Friskiness
    {
        get => _friskiness;
        set
        {
            if (value < 0)
                value = 0;
            if (value > 10)
                value = 10;
            _friskiness = value;
        }
    }
    public void ChangeFriskiness(float amount)
    {
        Friskiness += amount;
    }

    private void FixedUpdate()
    {
        ChangePace(paceDepletionRate);
        ChangeFriskiness(friskinessIncreaseRate);
    }
    
}
