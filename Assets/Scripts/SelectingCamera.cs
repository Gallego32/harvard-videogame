using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingCamera : MonoBehaviour
{
    // Determining whether we have one or two players
    public bool twoPlayers;

    // Reference to our OnePlayerCamera
    public GameObject onePlayerCamera;

    // Reference to our twoPlayers script
    private MultiplePlayers twoPlayersScript;


    // Start is called before the first frame update
    void Start()
    {
        twoPlayersScript = GetComponent<MultiplePlayers>();
    }

    // Update is called once per frame
    void Update()
    {
        if (twoPlayers) {
            onePlayerCamera.SetActive(false);
            twoPlayersScript.enabled = true;
        } else {
            twoPlayersScript.enabled = false;
            onePlayerCamera.SetActive(true);
        }
    }
}
