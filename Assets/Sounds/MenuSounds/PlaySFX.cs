using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] private AudioSource _MenuButtonSound;
    [SerializeField] private AudioSource _BackButtonSound;

    public void PlayMenuButtonSound()
    {
        _MenuButtonSound.Play();
    }
    public void PlayBackSound()
    {
        _BackButtonSound.Play();
    }
}
