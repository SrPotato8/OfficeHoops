using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OPtionsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerOptions;
    public Slider VolumeSlider;

    public Toggle screen;

    public TMP_Dropdown resolution1;
    private Resolution[] resolution2;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        VolumeSlider.value = savedVolume;
        MixerOptions.SetFloat("OptionsMenu", savedVolume);

        screen.isOn = Screen.fullScreen;

        ChectResolution();
    }

    public void VolumeMenu(float x)
    {
        MixerOptions.SetFloat("OptionsMenu", x);
        PlayerPrefs.SetFloat("Volume", x); 
    }

    public void FullScreen(bool A)
    {
        Screen.fullScreen = A;
        PlayerPrefs.SetInt("FullScreen", A ? 1 : 0); 
    }

    public void ChectResolution()
    {
        resolution2 = Screen.resolutions;
        resolution1.ClearOptions();
        List<string> options = new List<string>();
        int ActualResolution = 0;

        for (int i = 0; i < resolution2.Length; i++)
        {
            string opcion = resolution2[i].width + " x " + resolution2[i].height;
            options.Add(opcion);

            if (Screen.fullScreen && resolution2[i].width == Screen.currentResolution.width && resolution2[i].height == Screen.currentResolution.height)
            {
                ActualResolution = i;
            }
        }

        resolution1.AddOptions(options);

        int savedResolution = PlayerPrefs.GetInt("intResolution", ActualResolution);
        resolution1.value = savedResolution;
        resolution1.RefreshShownValue();
        ChangeResolution(savedResolution);

        resolution1.onValueChanged.AddListener(ChangeResolution);
    }

    public void ChangeResolution(int resolution)
    {
        Debug.Log("Changing resolution to index: " + resolution);
        PlayerPrefs.SetInt("intResolution", resolution);

        if (resolution2 != null && resolution >= 0 && resolution < resolution2.Length)
        {
            Resolution resolution3 = resolution2[resolution];
            Debug.Log("Setting resolution to: " + resolution3.width + " x " + resolution3.height);
            Screen.SetResolution(resolution3.width, resolution3.height, Screen.fullScreen);
        }
        else
        {
            Debug.LogError("Invalid resolution index or resolution2 is not initialized.");
        }
    }
}
