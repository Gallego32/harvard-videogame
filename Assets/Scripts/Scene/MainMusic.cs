using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 *  We will inherit our Audio system from AudioManager script
 *  We will use super methods and Exceptions system to avoid errors
 */

public class MainMusic : AudioManager
{
    [HideInInspector]
    public string nowPlaying;

    [HideInInspector]
    public bool isPaused;

    // Playlist variables control
    private int[] order;
    private int inOrder;

    void Awake()
    {
        order = new int[audios.Length];

        isPaused = true;

        // Generate vector
        GeneratePlaylist();

        // Add source components
        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;

            audio.source.loop = audio.loop;

            audio.source.outputAudioMixerGroup = audioMixerGroup;
        }

        // Play first song
        Play(audios[order[inOrder]].name);
    }

    void FixedUpdate() 
    {
        // Detect when the song ends and play next song
        if (!isPaused && !base.isPlaying(nowPlaying))
        {
            Debug.Log("Here we go again");
            Play(audios[order[inOrder]].name);
            // When the playlist is over 
            if (inOrder == order.Length)
                GeneratePlaylist(order[inOrder - 1]);
        }
               
    }

    // Inherited methods from the base class
    // We'll use them with Exception logic to avoid errors
    public void Play(string name)
    {
        try 
        {
            base.Play(name);
            
            nowPlaying = name;
            inOrder++;
            //isPaused = false;
        }
        catch (System.ArgumentException ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public void Pause(string name)
    {
        try 
        {
            base.Pause(name);
            isPaused = true;
        }
        catch (System.ArgumentException ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public void Resume(string name)
    {
        try 
        {
            base.Resume(name);
            isPaused = false;
        }
        catch (System.ArgumentException ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    // Generate new random vector with non repeated values depending on some value
    private void GeneratePlaylist(int lastNumber)
    {
        inOrder = 0;
        for (int i = 0; i < order.Length;)
        {
            bool repeated = false;
            order[i] = Random.Range(0, order.Length);

            if (i == 0 && order[i] == lastNumber)
                repeated = true;

            for (int j = i - 1; j >= 0; j--)
            {
                if (order[i] == order[j])
                    repeated = true;
            }
            if (!repeated)
                i++;
        }
    }

    private void GeneratePlaylist()
    {
        GeneratePlaylist(Random.Range(0, audios.Length));
    }
}
