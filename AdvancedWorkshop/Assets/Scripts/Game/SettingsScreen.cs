using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingsScreen : MonoBehaviour
{


    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Dropdown resolutionDropdown;

    public Slider musicSlider;
    public Slider sfxSlider;
    float musicVolume, sfxVolume;

    Resolution[] resolutions;






    // Start is called before the first frame update
    void Awake()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
    }



}
