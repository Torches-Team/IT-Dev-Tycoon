using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSceneSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _ResearchButtonSound;
    [SerializeField] private AudioSource _LoudButtonSound;
    [SerializeField] private AudioSource _SoftButtonSound;
    [SerializeField] private AudioSource _MenuButtonSound;

    public void PlayResearchButtonSound()
    {
        _ResearchButtonSound.Play();
    }
    
    public void PlayButtonLoudSound()
    {
        _LoudButtonSound.Play();
    }

    public void PlayButtonSoftSound()
    {
        _SoftButtonSound.Play();
    }

    public void PlayMenuButtonSound()
    {
        _MenuButtonSound.Play();
    }
}
