using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingCamera : MonoBehaviour
{
    // Determining whether we have one or two players
    public bool twoPlayers;

    // Reference to our Players
    public GameObject player1, player2;

    // Reference to our CVCamera
    private CinemachineBrain onePlayerCamera;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject camera;

    // Reference to our twoPlayers script
    private MultiplePlayers twoPlayersScript;


    // Start is called before the first frame update
    void Start()
    {
        twoPlayersScript = GetComponent<MultiplePlayers>();
        onePlayerCamera = GetComponent<CinemachineBrain>();
        cinemachineVirtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.active && player2.active && !twoPlayers)
            twoPlayers = true;

        if (!player1.active || !player2.active && twoPlayers)
            twoPlayers = false;
        

        if (twoPlayers) {
            onePlayerCamera.enabled = false;
            twoPlayersScript.enabled = true;
        } else {
            twoPlayersScript.enabled = false;
            onePlayerCamera.enabled = true;
            if (player1.active)
                cinemachineVirtualCamera.Follow = player1.transform;
            else
                cinemachineVirtualCamera.Follow = player2.transform;
        }
    }
}
