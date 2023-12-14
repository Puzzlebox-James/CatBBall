using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _HighScoreNumb;

    [SerializeField] private float startHoldTimeRequired = 3f;

    [SerializeField] private Light2D _spotlight1;
    [SerializeField] private Light2D _spotlight2;
    [SerializeField] private Light2D _spotlight3;

    [SerializeField] private Image _panel1;
    [SerializeField] private Image _panel2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F))
            StartCoroutine(FillStartMeter());

        _HighScoreNumb.text = HighScore.HighScoreInt.ToString();
    }

    private IEnumerator FillStartMeter()
    {
        var i = 0f;
        while (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.F))
        {
            i += Time.deltaTime;

            _panel1.CrossFadeAlpha(0, startHoldTimeRequired, false);
            _panel2.CrossFadeAlpha(0, startHoldTimeRequired, false);

            if (i > startHoldTimeRequired * .3f)
                _spotlight1.enabled = true;
            if (i > startHoldTimeRequired * .5f)
                _spotlight2.enabled = true;
            if (i > startHoldTimeRequired * .7f)
                _spotlight3.enabled = true;

            if (i > startHoldTimeRequired)
                SceneManager.LoadScene("MainScene");
            yield return null;
        }
        yield break;
    }
}
