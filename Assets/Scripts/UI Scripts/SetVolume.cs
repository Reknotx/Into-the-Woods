using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 4/25/2021
/// <summary>
/// the class that handles the audio sliders
/// </summary>
public class SetVolume : MonoBehaviour
{
    //referance to the audio mixer
    public AudioMixer mixer;

    //references to the two sliders
    public Slider musicSlider;
    public Slider effectSlider;

    static bool FirstStart = true;


    private void Start()
    {
        //sets the volume levels to the values of the player prefs for consistency
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", .25f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", .25f);
        MusicLevel(musicSlider.value);

        if (FirstStart)
        {
            transform.parent.gameObject.SetActive(false);
            FirstStart = false;
        }
    }

    /// Author: JT Esmond
    /// Date: 4/25/2021
    /// <summary>
    /// functions that controls the volume of the music based off of the slider value
    /// </summary>
    public void MusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    /// Author: JT Esmond
    /// Date: 4/25/2021
    /// <summary>
    /// functions that controls the volume of the sound effects based off of the slider value
    /// </summary>
    public void EffectLevel(float sliderValue)
    {
        mixer.SetFloat("EffectVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
    }
}
