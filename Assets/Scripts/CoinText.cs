using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public class CoinText : MonoBehaviour
{
    // Control coins
    private float coins;

    public void Start()
    {   
        // Start coins at 0
        coins = 0;
    }

    public void setCoins(float coins)
    {
        this.coins += coins;
        GetComponent<Text>().text = "COINS: " + this.coins;
    }
}
