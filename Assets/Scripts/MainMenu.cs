using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int players)
    {
        LoadGame.players = players;
        SceneManager.LoadScene("Play - copia");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
