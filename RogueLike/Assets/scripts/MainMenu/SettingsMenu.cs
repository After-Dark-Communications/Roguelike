using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource _AudioSrc;
    private float _MusicVolume = 0.5f;

    void start()
    {
        _AudioSrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        _AudioSrc.volume = _MusicVolume;
    }

    public void SetVolume(float volume)
    {
        _MusicVolume = volume;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
