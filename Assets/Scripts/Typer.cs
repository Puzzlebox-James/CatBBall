using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Typer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordOutput;
    [Space]
    [SerializeField] private float pspspsPaceBonus;
    [SerializeField] private float pspspsFriskBonus;

    [SerializeField] private float loveWordPaceBonus;
    [SerializeField] private float loveWordFriskBonus;

    [SerializeField] private float hurtWordFriskBonus;

    private int _remainingHurtWords;

    private WordBanks wordBank;
    private string remainingWord = String.Empty;
    private string currentWord;

    private typerWordType currentWordType;
    
    public enum typerWordType
    {
        loveWords,
        hurtWords,
        pspsps
    };
    

    private void Start()
    {
        TyperManager.Instance.OnBallPickedUp += SetCurrentWord;
        TyperManager.Instance.OnBallRunBackSpawned += SetCurrentWord;
        TyperManager.Instance.OnFriskBallThrow += SetCurrentWord;
        TyperManager.Instance.OnChange += CullWord;
        wordBank = new WordBanks();
    }
    private void OnDisable()
    {
        TyperManager.Instance.OnBallPickedUp -= SetCurrentWord;
        TyperManager.Instance.OnBallRunBackSpawned -= SetCurrentWord;
        TyperManager.Instance.OnFriskBallThrow -= SetCurrentWord;
        TyperManager.Instance.OnChange -= CullWord;
    }


    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!Input.anyKeyDown) return;
        var keyPressed = Input.inputString;
        if (keyPressed.Length != 1) return;
        EnterLetter(keyPressed);
    }

    
    
    private void SetCurrentWord(typerWordType type)
    {
        switch (type)
        {
            case typerWordType.loveWords:
                switch (Meters.Instance.Pace)
                {
                    case < 5:
                    {
                        currentWordType = typerWordType.loveWords;
                        var rng = Random.Range(0, wordBank.LoveWordsEasy.Count);
                        SetRemainingWord(wordBank.LoveWordsEasy[rng]);
                        break;
                    }
                    case >= 5 and < 8:
                    {
                        currentWordType = typerWordType.loveWords;
                        var rng = Random.Range(0, wordBank.LoveWordsMedium.Count);
                        SetRemainingWord(wordBank.LoveWordsMedium[rng]);
                        break;
                    }
                    case >= 8:
                    {
                        currentWordType = typerWordType.loveWords;
                        var rng = Random.Range(0, wordBank.LoveWordsHard.Count);
                        SetRemainingWord(wordBank.LoveWordsHard[rng]);
                        break;
                    }
                }
                break;
            
            case typerWordType.hurtWords:
                currentWordType = typerWordType.hurtWords;
                _remainingHurtWords = 4;
                var rng2 = Random.Range(0, wordBank.HurtWords.Count);
                SetRemainingWord(wordBank.HurtWords[rng2]);
                break;
            
            case typerWordType.pspsps:
                currentWordType = typerWordType.pspsps;
                var rng3 = Random.Range(0, wordBank.ComeWords.Count);
                SetRemainingWord(wordBank.ComeWords[rng3]);
                break;
            default:
                Debug.Log("Something's gone real wrong");
                break;
        }
    }

    private void SetRemainingWord(string wordStub)
    {
        remainingWord = wordStub;
        wordOutput.text = remainingWord;
    }
    
    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            
            if (IsWordComplete())
            {
                ResolveCompleteWord();
                // do other stuff.
            }
        }
    }

    private void ResolveCompleteWord()
    {
        switch (currentWordType)
        {
            case typerWordType.pspsps:
                Meters.Instance.ChangePace(pspspsPaceBonus);
                Meters.Instance.ChangeFriskiness(pspspsFriskBonus);
                SetRemainingWord(wordBank.ComeWords[Random.Range(0, wordBank.ComeWords.Count)]);
                break;
            
            case typerWordType.loveWords:
                Meters.Instance.ChangePace(loveWordPaceBonus);
                Meters.Instance.ChangeFriskiness(loveWordFriskBonus);
                break;
            
            case typerWordType.hurtWords:
                if (_remainingHurtWords > 0)
                {
                    var rng = Random.Range(0, wordBank.HurtWords.Count);
                    SetRemainingWord(wordBank.HurtWords[rng]);
                    _remainingHurtWords -= 1;
                    Meters.Instance.ChangeFriskiness(hurtWordFriskBonus);
                    return;
                }
                TyperManager.Instance.IsHurt = false;
                TyperManager.Instance.OnChange?.Invoke();
                TyperManager.Instance.OnBallPickedUp?.Invoke(Typer.typerWordType.loveWords);

                if (FindObjectOfType<BallRunBack>() == null && FindObjectOfType<Ball>() == null)
                {
                    BallCreator.Instance.SpawnBall();
                }
                break;
            
            default:
                Debug.Log("Something's gone wrong in Resolving the Word");
                break;
        }
    }
    

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        var newWordStub = remainingWord.Remove(0, 1);
        SetRemainingWord(newWordStub);
        
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    
    
    private void CullWord()
    {
        remainingWord = "";
        wordOutput.text = remainingWord;
    }
}
