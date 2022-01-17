using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    public static float volumeValue;

    void Awake()
    {
        volumeSlider = GetComponent<Slider>();

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartMenu"))
        {
            volumeSlider.value = 1;
        }
        else
        {
            volumeSlider.value = volumeValue;
        }
        
    }

    public void SetVolLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        volumeValue = volumeSlider.value;
    }
}
