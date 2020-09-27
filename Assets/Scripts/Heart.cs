using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float life;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Choca");
        if (other.gameObject.layer == 8)
        {
            other.GetComponent<PlayerStats>().pickHeart(life);
            Destroy(gameObject);
        }
    }
}
