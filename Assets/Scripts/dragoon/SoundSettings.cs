using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer masterMixer;

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
