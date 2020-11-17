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

    private bool musicPlaying;

    public Sprite pause;
    public Sprite resume;

    private MainMusic mainMusic;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        mainMusic = FindObjectOfType<MainMusic>();
        image = GetComponent<Image>();

        musicPlaying = mainMusic.isPlaying(mainMusic.nowPlaying);
        //Debug.Log(musicPlaying);

        image.sprite = musicPlaying ? pause : resume;
    }

    public void PuaseResume() 
    {
        if (musicPlaying)
        {
            // If the music is Playing pause it
            mainMusic.Pause(mainMusic.nowPlaying);
            musicPlaying = false;

            // Change sprite to "resume" one
            image.sprite = resume;
        } else
        {
            // If the music isn't playing resume it
            mainMusic.Resume(mainMusic.nowPlaying);
            musicPlaying = true;

            // Change sprite to "pause" one
            image.sprite = pause;
        }
    }
}
