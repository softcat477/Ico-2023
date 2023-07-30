using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;

    public SoundSettings soundSettings;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        LoadVolume();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        audioMixer.SetFloat(SoundSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat(SoundSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}
