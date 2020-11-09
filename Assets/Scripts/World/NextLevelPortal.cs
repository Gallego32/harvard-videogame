using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NextLevelPortal : MonoBehaviour
{
    // Manage position
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Movement variables
    public float amplitude;
    public float speed;

    private float movement;

    // Going up when players are on the platform
    private bool onPlatform;

    private bool returnPlatform;

    private int upDown;

    public LoadGame lg;

    // Use this for initialization
    void Start()
    {
        // Starting point
        posOffset = transform.position;

        onPlatform = false;
        returnPlatform = false;
        movement = 0;
        upDown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onPlatform && !returnPlatform)
        {
            movement += Time.deltaTime * upDown;

            // Move up and down
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(movement * Mathf.PI * speed) * amplitude;
        } else
        {
            if (!returnPlatform)
                tempPos.y += Time.deltaTime / 1.5f;
            else
            {
                tempPos.y += Time.deltaTime * upDown;
                if (upDown == -1 && transform.position.y < posOffset.y)
                    returnPlatform = false;
                else if (upDown == 1 && transform.position.y > posOffset.y)
                    returnPlatform = false;
                movement = 0;
            }
        }

        transform.position = tempPos;
        
        if (transform.position.y > posOffset.y + 3)
        {    
            // Avoid changing level more than once
            tempPos = posOffset;
            lg.NextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.GetType() == typeof(BoxCollider2D))
        {
            onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.GetType() == typeof(BoxCollider2D))
        {
            onPlatform = false;
            returnPlatform = true;
            if (transform.position.y < posOffset.y)
                upDown = 1;
            else 
                upDown = -1;
        }
    }

}
