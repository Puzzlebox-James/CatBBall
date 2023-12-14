using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timeInSeconds;

    [SerializeField] private GameObject _endPanelGO;
    
    
    void Update()
    {
        timeInSeconds -= Time.deltaTime;
        timerText.text = "<mspace=0.45em>" + TimeSpan.FromSeconds(timeInSeconds).ToString(@"mm\:ss") + "</mspace>";


        if (timeInSeconds < 0)
        {
            _endPanelGO.SetActive(true);

            if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H))
                StartCoroutine(GoToTitleRoutine());

            timerText.text = "00:00";
            
        } else if (Score.Instance.ScoreInt > HighScore.HighScoreInt)
                HighScore.HighScoreInt = Score.Instance.ScoreInt;
    }


    private IEnumerator GoToTitleRoutine()
    {
        var i = 1f;
        while (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.H))
        {
            i -= Time.deltaTime;
            if (i < 0)
                SceneManager.LoadScene("TitleScene");
            yield return null;
        }
        yield break;
    }
}
