using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // How many players
    public static int players = 1;

    public static int level = 0;

    // Control both players
    public GameObject player1;
    public GameObject player2;

    // Control camera position
    public Transform camera;

    // Use Fader behaviour
    public Fader fader;

    // Remove second health bar if player2 isn't playing
    public GameObject player2HealthBar;

    // Control Level
    public LevelGenerator game;

    // Start is called before the first frame update
    void Start()
    {
        if (players == 1)
        {
            player2.SetActive(false);
            player2HealthBar.SetActive(false);
        }
        // Spawn Players
        StartCoroutine(Spawn());
    }

    void Update()
    {
        // Check if any of our players are alive, else, gameOver Scene
        if (!player1.active && !player2.active)
            SceneManager.LoadScene("Game Over");

        // A cambiar
        if (player1.transform.position.x > (game.offset.x + game.size.x - 10) * 0.159f)
        {
            
        }
    }

    public void NextLevel()
    {
        // Generate level
        game.NextLevel();

        // Fade IN and OUT
        StartCoroutine(fader.FadeFor(0.2f, 5, Fader.initialSpeed));

        // Spawn players in our spawn point (At the beggining of the level)
        StartCoroutine(Spawn());
    }

    /*void Spawn()
    {
        //camera.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);
        //No funciona player1.GetComponent<ClampPosition>().enabled = false;

        player1.transform.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);

        //player1.GetComponent<ClampPosition>().enabled = true;

        if (players == 2)
            player2.transform.position = new Vector3((game.offset.x + 6.5f) *  0.159f, game.offset.y + 2, 0);
    }*/

    IEnumerator Spawn()
    {
        // Move camera before player
        camera.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);

        // Update Camera to avoid bugs with Clamp Position script
        yield return Time.deltaTime;

        // Move players to our spawn point
        player1.transform.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);

        if (players == 2)
            player2.transform.position = new Vector3((game.offset.x + 6.5f) *  0.159f, game.offset.y + 2, 0);
    }
}
