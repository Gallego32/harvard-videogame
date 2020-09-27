using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    // Manage position
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    public float amplitude;
    public float speed;

    // Use this for initialization
    void Start () {
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        // Move up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amplitude;
 
        transform.position = tempPos;
    }
}
