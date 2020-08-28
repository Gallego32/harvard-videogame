using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private BaseStats stats;

    private int level;

    public float baseMaxHealth, baseAttack, baseDefense, baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        stats = new BaseStats(baseMaxHealth, baseAttack, baseDefense, baseSpeed, gameObject.tag, gameObject.layer);
        Debug.Log(stats.Tag + " " + stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.Health <= 0f)
            Die();
        //stats.ModifyStat("health", -0.1f);
    }

    void Die()
    {
        Debug.Log("Die " + stats.Tag);
        if (Equals(stats.Tag, "Player1"))
            GameObject.Find("Player1").SetActive(false);
        else if (Equals(stats.Tag, "Player2"))
            GameObject.Find("Player2").SetActive(false);
        else
            Destroy(gameObject);        
    }
}
