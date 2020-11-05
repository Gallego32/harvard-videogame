using UnityEngine.Audio;
using UnityEngine;
using System;

// Use FindObjectOfType<AudioManager>().Play("name");

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
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
        }

        Play("MainTheme");
    }

    public void Play(string name)
    {
        Audio s = Array.Find(audios, audio => audio.name == name);
        if (s != null)
            s.source.Play();
        else
            Debug.LogWarning("Not found Sound");
    }
}
