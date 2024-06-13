using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider volume;
    [SerializeField] AudioMixer mixer;

   
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Sound")) * 20);
            volume.value = PlayerPrefs.GetFloat("Sound");
        }
        else
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(0.1f) * 20);
            volume.value = 0.1f;
        }
    }
    
    public void OnSliderChange()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value)*20);
        PlayerPrefs.SetFloat("Sound", volume.value);
        
    }
}
