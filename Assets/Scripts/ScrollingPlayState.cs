using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollingPlayState : MonoBehaviour
{
    public Transform cam;

    public float speed;
    private float startPosition, camStart, width;

    void Start()
    {
        // Get all the main variables
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
        camStart = cam != null ? cam.position.x : 0;
    }

    void FixedUpdate()
    {
        // Get the camera change at every update
        float camChange = cam.position.x - camStart;

        // Move the background
        transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        
        // Know when we need to go back
        if (transform.position.x > width + camChange + startPosition)
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
    }
}