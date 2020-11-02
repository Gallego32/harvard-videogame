using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    // Reference to the camera
    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        // Clamping Horizontal Movement
        float clampPositionX;
        clampPositionX = Mathf.Clamp(transform.position.x, camera.transform.position.x - 2.9f, camera.transform.position.x + 2.9f);

        transform.position = new Vector3(clampPositionX, transform.position.y, transform.position.z);
    }
}
