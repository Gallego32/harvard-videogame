using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // How many coins do we want for this item
    public float coins;

    // Trigger Pick Coin function from PlayerStats
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.GetType() == typeof(BoxCollider2D))
        {
            other.GetComponent<PlayerStats>().pickCoin(coins);
            Destroy(gameObject);
        }
    }
}
