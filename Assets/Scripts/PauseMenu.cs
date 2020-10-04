using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Control whether we are paused or not
    public static bool Paused = false;

    // Set active if we are paused
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            if (Paused)
                Resume();
            else
                Pause();     
    }
    
    public void Resume()
    {
        Paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        Paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        Paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
