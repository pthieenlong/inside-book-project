using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public Slider SFX_Slider;
    public Slider Music_Slider;

    void Start()
    {
        SFX_Slider.value = SoundManager.SoundVolume;
        Music_Slider.value = SoundManager.MusicVolume;
    }

    public void OnSFXValueChange(float val)
    {
        SoundManager.Instance.SetSFXVolume(val);
    }

    public void OnMusicValueChange(float val)
    {
        SoundManager.Instance.SetMusicVolume(val);
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
    }
}
