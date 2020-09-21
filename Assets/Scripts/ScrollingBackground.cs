using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject cam;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        x = cam.transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
