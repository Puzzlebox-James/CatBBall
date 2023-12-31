using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public static ParticleHandler Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }


    [SerializeField] private ParticleSystem _bloodParticleSystem;
    [SerializeField] private ParticleSystem _healedParticleSystem;
    [SerializeField] private ParticleSystem _completedWordParticleSystem;

    public void ToggleBlood()
    {
        ParticleSystem.EmissionModule _bloodEmisMod = _bloodParticleSystem.emission;
        if (_bloodParticleSystem.isEmitting) _bloodEmisMod.enabled = false;
        else _bloodEmisMod.enabled = true;
    }

    public void PlayHealed()
    {
        _healedParticleSystem.Play();
    }

    public void PlayWordCompleted()
    {
        _completedWordParticleSystem.Play();
    }
}
