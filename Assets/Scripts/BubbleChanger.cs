using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class BubbleChanger : MonoBehaviour
{
    [SerializeField] private Image currentWordBackground;

    [SerializeField] private Sprite loveBubble;
    [SerializeField] private Sprite normalBubble;
    [SerializeField] private Sprite[] painBubbles;

    [SerializeField] private AnimationCurve bubblePop;
    [SerializeField] private AnimationCurve bubbleFade;
    [SerializeField] private AnimationCurve bubblePainPop;


    private bool isFadingBubble;
    private bool isFadedOut;
    private Rect startingWordBubbleRect;
    private Sprite newWordBackground;


    private void Start()
    {
        TyperManager.Instance.OnBallPickedUp += SetBubble;
        TyperManager.Instance.OnBallRunBackSpawned += SetBubble;
        TyperManager.Instance.OnFriskBallThrow += SetPainBubble;
        TyperManager.Instance.OnPainBubbleChange += UpdatePainBubble;
        TyperManager.Instance.OnChange += ChangeBubbleTransition;
        TyperManager.Instance.OnLoveWordComplete += HideLoveBubble;

        startingWordBubbleRect = currentWordBackground.rectTransform.rect;
    }

    private void OnDisable()
    {
        TyperManager.Instance.OnBallPickedUp -= SetBubble;
        TyperManager.Instance.OnBallRunBackSpawned -= SetBubble;
        TyperManager.Instance.OnFriskBallThrow -= SetPainBubble;
        TyperManager.Instance.OnPainBubbleChange -= UpdatePainBubble;
        TyperManager.Instance.OnChange -= ChangeBubbleTransition;
        TyperManager.Instance.OnLoveWordComplete -= HideLoveBubble;
    }


    private void SetBubble(Typer.typerWordType type)
    {
        switch (type)
        {
            case Typer.typerWordType.loveWords:
                newWordBackground = loveBubble;
                break;

            case Typer.typerWordType.pspsps:
                newWordBackground = normalBubble;
                break;
        }
    }


    private void ChangeBubbleTransition()
    {
        if (isFadingBubble) return;
        isFadingBubble = true;
        if (isFadedOut)
            StartCoroutine(FadeIn());
        else { StartCoroutine(FadeBubbleInAndOut()); }
    }

    private IEnumerator FadeBubbleInAndOut()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            currentWordBackground.color = new Color(1, 1, 1, bubbleFade.Evaluate(i));
            currentWordBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bubblePop.Evaluate(i) * startingWordBubbleRect.width);
            currentWordBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, bubblePop.Evaluate(i) * startingWordBubbleRect.height);
            if (i > .5f && newWordBackground != null) currentWordBackground.sprite = newWordBackground;
            yield return null;
        }
        isFadingBubble = false;
    }


    private void HideLoveBubble()
    {
        StartCoroutine(FadeLoveBubble());
        isFadingBubble = true;
    }

    private IEnumerator FadeLoveBubble()
    {
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            currentWordBackground.color = new Color(1, 1, 1, i);
            yield return null;
        }
        isFadingBubble = false;
        isFadedOut = true;
    }

    private IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            if (i > 0) currentWordBackground.sprite = newWordBackground;
            currentWordBackground.color = new Color(1, 1, 1, i);
            yield return null;
        }
        isFadingBubble = false;
        isFadedOut = false;
    }


    private void SetPainBubble(Typer.typerWordType typerWordType)
    {
        Debug.Log("SetPainBubble method entered from ball thrower invoke");
        newWordBackground = painBubbles[0];
        StartCoroutine(PainBubbleBounceAnimation());
    }

    private void UpdatePainBubble(int _remainingHurtWords)
    {
        newWordBackground = painBubbles[_remainingHurtWords];
        StartCoroutine(PainBubbleBounceAnimation());
    }

    private IEnumerator PainBubbleBounceAnimation()
    {
        for (float i = 0; i <= .5f; i += Time.deltaTime)
        {
            currentWordBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bubblePainPop.Evaluate(i) * startingWordBubbleRect.width);
            currentWordBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, bubblePainPop.Evaluate(i) * startingWordBubbleRect.height);
            if (newWordBackground != null) currentWordBackground.sprite = newWordBackground;
            yield return null;
        }
        isFadingBubble = false;
    }
}
