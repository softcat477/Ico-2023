using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Start() {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    public void SetMasterSound(float soundLevel)
    {
        DoClampSound(soundLevel);
        DoMusicExposedValueSetFloat("masterVol", soundLevel);
    }

    public void SetBGMSound(float soundLevel)
    {
        DoClampSound(soundLevel);
        DoMusicExposedValueSetFloat("bgmVol", soundLevel);
    }

    public void SetSFXSound(float soundLevel)
    {
        DoClampSound(soundLevel);
        DoMusicExposedValueSetFloat("sfxVol", soundLevel);
    }

    public void DoClampSound(float soundLevel)
    {
        soundLevel = Mathf.Clamp(soundLevel, 0.001f, 1f);
    }

    public void DoMusicExposedValueSetFloat(string valueName, float soundLevel)
    {
        masterMixer.SetFloat(valueName, Mathf.Log(soundLevel) * 20);
    }
}
