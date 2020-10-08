using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // How many players
    public static int players = 2;

    public static int level;

    // Control both players
    public GameObject player1;
    public GameObject player2;

    public LevelGenerator game;

    // Start is called before the first frame update
    void Start()
    {
        if (players == 1)
            player2.SetActive(false);

        // Spawn Players
        Spawn();
    }

    void Update()
    {
        // Check if any of our players are alive, else, gameOver Scene
        if (!player1.active && !player2.active)
            SceneManager.LoadScene("Menu");
    }

    void Spawn()
    {
        player1.transform.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);

        if (players == 2)
            player2.transform.position = new Vector3((game.offset.x + 6.5f) *  0.159f, game.offset.y + 2, 0);
    }
}
