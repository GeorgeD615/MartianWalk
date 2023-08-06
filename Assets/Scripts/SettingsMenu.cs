using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private Resolution[] _resolutions;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("_musicVolume"))
            PlayerPrefs.SetFloat("_musicVolume", -20.0f);
        if (!PlayerPrefs.HasKey("_effectsVolume"))
            PlayerPrefs.SetFloat("_effectsVolume", -20.0f);
        SetMusicValue(PlayerPrefs.GetFloat("_musicVolume"));
        SetEffectsValue(PlayerPrefs.GetFloat("_effectsVolume"));
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < _resolutions.Length; ++i)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width && 
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }


        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicValue(float val)
    {
        PlayerPrefs.SetFloat("_musicVolume", val);
        _audioMixer.SetFloat("MusicVolume", val);
    }

    public void SetEffectsValue(float val)
    {
        PlayerPrefs.SetFloat("_effectsVolume", val);
        _audioMixer.SetFloat("EffectsVolume", val);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
