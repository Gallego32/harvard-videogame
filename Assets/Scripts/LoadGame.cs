using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public static int players;

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        if (players == 1)
            player2.SetActive(false);
    }

    void Update()
    {
        // Check if any of our players are alive, else, gameOver Scene
    }
}
