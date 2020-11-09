using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindAudioManager : MonoBehaviour
{
    private Button button;
    public string sound;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play(sound);
    }
}
