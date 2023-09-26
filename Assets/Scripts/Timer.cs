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

        timerText.text = "<mspace=0.45em>" + TimeSpan.FromSeconds(timeInSeconds).ToString(@"mm\:ss") + "</mspace>";

        if (timeInSeconds < 0)
        {
            //do end of game stuff
            timerText.text = "00:00";
        }
    }
}
