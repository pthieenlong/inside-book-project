using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public static float SoundVolume = 1;
    public static float MusicVolume = 1;
    public AudioSource audioSource;

    void Awake()
    {
        // Simple Singleton
        Instance = this;

        // Load Data
        SoundVolume = GamePlaySetting.LoadFloat(SaveKey.SoundVolume, 1.0f);
        MusicVolume = GamePlaySetting.LoadFloat(SaveKey.MusicVolume, 1.0f);
    }

    public void SetSFXVolume(float vol)
    {
        SoundVolume = vol;
        GamePlaySetting.SaveFloat(SaveKey.SoundVolume, SoundVolume);
    }

    public void SetMusicVolume(float vol)
    {
        MusicVolume = vol;
        audioSource.volume = MusicVolume;
        GamePlaySetting.SaveFloat(SaveKey.MusicVolume, MusicVolume);
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, SoundVolume);
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
