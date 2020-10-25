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

    // Manage Animation
    private Animator animation;

    void Start()
    {
        // Get Animator component
        animation = pauseMenu.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            if (Paused)
                Resume();
            else
                Pause();     
    }
    
    public IEnumerator Faint()
    {
        // Start Faint animation
        animation.SetTrigger("Faint");
        
        Paused = false;
        Time.timeScale = 1f;

        // Wait until animation gets updated
        yield return Time.deltaTime;

        // Get animation info for getting animation lenght
        AnimatorClipInfo[] m_CurrentClipInfo = animation.GetCurrentAnimatorClipInfo(0);

        // Wait for the exact time of the animation
        yield return new WaitForSeconds(m_CurrentClipInfo[0].clip.length);
        pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("PauseInverted");
        StartCoroutine(Faint());
    }

    void Pause()
    {
        FindObjectOfType<AudioManager>().Play("Pause");
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

    public void Reset()
    {
        // Resume behaviour
        Paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        SceneManager.LoadScene("Play - copia");
    }
}
