using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

// Use FindObjectOfType<AudioManager>().Play("name");
// Better Use FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("name");

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    public static GameObject instance;
    
    public AudioMixerGroup audioMixerGroup;

    void Awake()
    {
        if (instance == null)
            instance = gameObject;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;

            audio.source.loop = audio.loop;

            audio.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void Play(string name)
    {
        Audio s = Array.Find(audios, audio => audio.name == name);
        if (s != null)
            s.source.Play();
        else
            //Debug.LogWarning("Not found Sound");
            throw new System.ArgumentException("Not found Sound");
    }

    public void Pause(string name)
    {
        Audio s = Array.Find(audios, audio => audio.name == name);
        if (s != null)
        {
            if (s.source.isPlaying)
                s.source.Pause();
        }
        else
            //Debug.LogWarning("Not found Sound");   
            throw new System.ArgumentException("Not found Sound");
    }

    public void Resume(string name)
    {
        Audio s = Array.Find(audios, audio => audio.name == name);
        if (s != null)
        {
            if (!s.source.isPlaying)
                s.source.UnPause();
        }
        else
          //  Debug.LogWarning("Not found Sound");  
          throw new System.ArgumentException("Not found Sound");
    }

    public bool isPlaying(string name)
    {
        Audio s = Array.Find(audios, audio => audio.name == name);
        if (s != null)
            return s.source.isPlaying;
        return false;
    }
}
