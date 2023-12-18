using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingsScreen : MonoBehaviour
{


    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public Slider musicSlider;
    public Slider sfxSlider;
    float musicVolume, sfxVolume;

    Resolution[] resolutions;






    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("Volume", volume);
        sfxVolume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }




}
