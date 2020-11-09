using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Pause and Resume our music and sound
 */

public class DisableMusicButton : MonoBehaviour
{
    // Manage button and its image
    private Button button;
    private Image image;
    public string sound;

    private bool musicPlaying;

    public Sprite pause;
    public Sprite resume;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        audioManager = FindObjectOfType<AudioManager>();
        image = GetComponent<Image>();

        musicPlaying = audioManager.isPlaying(sound);

        image.sprite = musicPlaying ? pause : resume;
    }

    public void PuaseResume() 
    {
        if (musicPlaying)
        {
            // If the music is Playing oause it
            audioManager.Pause(sound);
            musicPlaying = false;

            // Change sprite to "resume" one
            image.sprite = resume;
        } else
        {
            // If the music isn't playing resume it
            audioManager.Resume(sound);
            musicPlaying = true;

            // Change sprite to "pause" one
            image.sprite = pause;
        }
    }
}
