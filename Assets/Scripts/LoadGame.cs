using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // How many players
    public static int players = 1;

    // Level control
    public static int level = 1;
    public LevelText levelText;

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
    }

    public void NextLevel()
    {
        // Generate level
        game.NextLevel();
        levelText.IncrementLevelText();

        // Fade IN and OUT
        StartCoroutine(fader.FadeFor(0.2f, 5, Fader.initialSpeed));

        FindObjectOfType<AudioManager>().Play("NextLevel");

        // Spawn players in our spawn point (At the beggining of the level)
        StartCoroutine(Spawn());

        RegeneratePlayers();
    }

    IEnumerator Spawn()
    {
        // Deactivate ClampPosition script before moving players
        player1.GetComponent<ClampPosition>().enabled = false;
        player2.GetComponent<ClampPosition>().enabled = false;

        // Wait for a small amount of time
        yield return Time.fixedDeltaTime;
        yield return Time.fixedDeltaTime;
        
        // Move camera before anything else
        camera.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);;
        
        // Move players to our spawn point
        player1.transform.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);
        
        // Move second player if there are two players
        if (players == 2)
            player2.transform.position = new Vector3((game.offset.x + 6.5f) *  0.159f, game.offset.y + 2, 0);

        // Wait for a small amount of time
        yield return Time.fixedDeltaTime;
        yield return Time.fixedDeltaTime;

        // Activate ClampPosition Script again
        player1.GetComponent<ClampPosition>().enabled = true;
        player2.GetComponent<ClampPosition>().enabled = true;
        
    }

    private void RegeneratePlayers()
    {
        // Set health to its max value again
        PlayerStats playerStats = player1.GetComponent<PlayerStats>();
        if (player1.active)
        {
            playerStats.Regenerate();
        } else
        {
            // Revive
            player1.transform.position = new Vector3((game.offset.x + 3.5f) *  0.159f, game.offset.y + 2, 0);
            player1.SetActive(true);
            playerStats.Revive();
        }

        playerStats = player2.GetComponent<PlayerStats>();
        if (players == 2 && player2.active)
        {
            playerStats.Regenerate();
        } else if (players == 2)
        {
            // Revive
            player2.transform.position = new Vector3((game.offset.x + 6.5f) *  0.159f, game.offset.y + 2, 0);
            player2.SetActive(true);
            playerStats.Revive();
        }
    }
}
