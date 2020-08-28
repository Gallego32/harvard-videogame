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
        Debug.Log(stats.Health);
        Debug.Log(stats.Tag);
        Debug.Log(stats.Layer);
        stats.ModifyStat("health", -123);
        Debug.Log(stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void Die()
    //{
    //   if (stats.Health)
    //}
}
