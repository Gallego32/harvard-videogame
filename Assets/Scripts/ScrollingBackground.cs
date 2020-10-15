using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;
    private float startPosition, camStart;
    public float width;

    void Start()
    {
        startPosition = transform.position.x;
    }

    void FixedUpdate()
    {
        // Background Movement
        transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);

        if (transform.position.x - startPosition > width)
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
        
    }
}
