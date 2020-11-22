using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer masterMixer;

    private Slider master, music, effects;

    // Set te actual level of sound to every slider
    void Awake()
    {
        float temp;
        master = transform.Find("MasterVolume/MasterSlider/Master").gameObject.GetComponent<Slider>();
        music = transform.Find("MusicVolume/MusicSlider/Music").gameObject.GetComponent<Slider>();
        effects = transform.Find("EffectsVolume/EffectsSlider/Effects").gameObject.GetComponent<Slider>();

        masterMixer.GetFloat("MasterVolume", out temp);
        master.value = temp;
        
        masterMixer.GetFloat("MusicVolume", out temp);
        music.value = temp;
        
        masterMixer.GetFloat("EffectsVolume", out temp);
        effects.value = temp;

    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        masterMixer.SetFloat("EffectsVolume", volume);
    }
}
