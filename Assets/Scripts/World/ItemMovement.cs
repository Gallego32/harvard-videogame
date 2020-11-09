using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    // Manage position
    private Vector3 posOffset = new Vector3 ();
    private Vector3 tempPos = new Vector3 ();

    // Movement variables
    public float amplitude;
    public float speed;

    // Control whether we want to despawn it or not
    public bool despawn = false;
    public int despawnTime;

    // Use this for initialization
    void Start () {
        // Starting point
        posOffset = transform.position;

        if (despawn)
            StartCoroutine(Despawn());
    }
     
    // Update is called once per frame
    void Update () {
        // Move up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amplitude;
 
        transform.position = tempPos;
    }
    
    // Remove the item after a while
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
