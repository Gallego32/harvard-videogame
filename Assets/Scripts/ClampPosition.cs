using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    // Reference to the camera
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Clamping Horizontal Movement
        float clampPositionX;
        clampPositionX = Mathf.Clamp(transform.position.x, camera.transform.position.x - 2.9f, camera.transform.position.x + 2.9f);

        transform.position = new Vector3(clampPositionX, transform.position.y, transform.position.z);

        // Kill player if it spends an amount of tieme out of the bounds of the map ?
    }
}
