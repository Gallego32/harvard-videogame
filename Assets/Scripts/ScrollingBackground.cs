using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject cam;

    public float speed;
    private float length, startPosition;
    public int width;

    // Start is called before the first frame update
    void Start()
    {
        //length = GetComponent<RectTransform>().sizeDelta.x;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Background Movement
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

        if (transform.position.x - startPosition > width)
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
    }
}
