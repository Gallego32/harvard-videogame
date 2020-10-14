using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // How many health do we want
    public float life;

    void Start() 
    {
        StartCoroutine(Despawn());
    }

    // Trigger Pick Heart function
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.GetType() == typeof(BoxCollider2D))
        {
            other.GetComponent<PlayerStats>().pickHeart(life);
            Destroy(gameObject);
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
