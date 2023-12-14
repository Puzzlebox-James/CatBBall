using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    
    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    public int ScoreInt { get; private set; }

    
    public void Scored()
    {
        ScoreInt += 3;
        if (Meters.Instance.Friskiness < 3 && Meters.Instance.Pace > 6)
            ScoreInt += 3;
        
        scoreText.text = ScoreInt.ToString();
    }


    private void Update()
    {
        if (ScoreInt > HighScore.HighScoreInt)
            HighScore.HighScoreInt = ScoreInt;
        highScoreText.text = HighScore.HighScoreInt.ToString();
    }
}
