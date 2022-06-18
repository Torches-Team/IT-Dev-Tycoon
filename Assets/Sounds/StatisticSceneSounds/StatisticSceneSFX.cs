using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticSceneSFX : MonoBehaviour
{
    [SerializeField] private AudioSource _PanelButtonSound;

    public void PlayPanelButtonSound()
    {
        _PanelButtonSound.Play();
    }
}
