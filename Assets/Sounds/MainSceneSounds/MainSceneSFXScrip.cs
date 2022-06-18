using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneSFXScrip : MonoBehaviour
{
    [SerializeField] private AudioSource _PopUpSound;
    [SerializeField] private AudioSource _PauseSound;
    [SerializeField] private AudioSource _PanelButtonSound;
    [SerializeField] private AudioSource _ContextButtonSound;
    [SerializeField] private AudioSource _ChooseButtonSound;
    [SerializeField] private AudioSource _CreditSound;

    public void PlayPopUpSound()
    {
        _PopUpSound.Play();
    }
    public void PlayPauseSound()
    {
        _PauseSound.Play();
    }
    public void PlayPanelButtonSound()
    {
        _PanelButtonSound.Play();
    }
    public void PlayContextButtonSound()
    {
        _ContextButtonSound.Play();
    }
    public void PlayChooseButtonSound()
    {
        _ChooseButtonSound.Play();
    }
    public void PlayCreditSound()
    {
        _CreditSound.Play();
    }
}
