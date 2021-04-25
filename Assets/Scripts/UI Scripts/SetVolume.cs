using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider effectSlider;
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", .05f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", .05f);
    }

    public void MusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void EffectLevel(float sliderValue)
    {
        mixer.SetFloat("EffectVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
    }
}
