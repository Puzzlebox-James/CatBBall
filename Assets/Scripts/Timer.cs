using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timeInSeconds;
    
    
    void Update()
    {
        timeInSeconds -= Time.deltaTime;

        timerText.text = TimeSpan.FromSeconds(timeInSeconds).ToString(@"mm\:ss");

        if (timeInSeconds < 0)
        {
            //do end of game stuff
            timerText.text = "00:00";
        }
    }
}
