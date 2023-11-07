using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Image[] sliderImagesArray;

    [SerializeField] private float powerScaler;
    [SerializeField] private AnimationCurve curve;


    private Ball _ball;
    public Ball Ball
    {
        get => _ball;
        set
        {
            // check for stuff?
            _ball = value;
        }
    }


    private void Start()
    {
        sliderImagesArray = slider.GetComponentsInChildren<Image>();
    }


    void Update()
    {
        if (_ball == null)
        {
            slider.value = 0;
        }

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (_ball == null) return;
        if (_ball.IsLaunched) return;
        StartCoroutine(DeterminePower());
    }


    
    private IEnumerator DeterminePower()
    {
        var startPressedTime = Time.time;
        var power = 0f;
        while (Input.GetKey(KeyCode.Space))
        {
            power = curve.Evaluate(Mathf.PingPong((Time.time - startPressedTime), 1));
            slider.value = power;
            yield return null;
        }
        LaunchBall(power * powerScaler);
        StartCoroutine(FadeOutTheSlider(true));
        yield break;
    }
    

    private void LaunchBall(float power)
    {
        _ball.IsLaunched = true;
        _ball.StartCoroutine(_ball.StuckCheck());
        _ball.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _ball.Rigidbody2D.AddForce(new Vector2(power/2, power/1.3f), ForceMode2D.Impulse);
        _ball.Rigidbody2D.AddTorque(power);

        if (Meters.Instance.Friskiness >= 8)
        {
            _ball.Rigidbody2D.velocity = Vector2.zero;
            _ball.Rigidbody2D.AddForce(new Vector2(Random.Range(1, powerScaler), Random.Range(1, powerScaler)), ForceMode2D.Impulse);
            _ball.Rigidbody2D.AddTorque(power);
            TyperManager.Instance.OnFriskBallThrow?.Invoke(Typer.typerWordType.hurtWords);
            TyperManager.Instance.IsHurt = true;
        }
        Debug.Log("Launched with: " + power);
    }



    public IEnumerator FadeOutTheSlider(bool isFadingOut)
    {
        if (isFadingOut) // fadeOUT
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                foreach (Image j in sliderImagesArray)
                {
                    j.color = new Color(1, 1, 1, i);
                }
                yield return null;
            }
        }
        else // fadeIN
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                foreach (Image j in sliderImagesArray)
                {
                    j.color = new Color(1, 1, 1, i);
                }
                yield return null;
            }
        }
    }

}
