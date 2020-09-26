﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public float detectDistance;

    void Start()
    {
        // Dissapear on height coroutine
        StartCoroutine(Disappear());
    }

    public void Hit(float damage)
    {
        // Perform Hit animation
        animation.SetTrigger("Hit");

        // Defense logic
        float dmg = damage - (Defense * damage / 100);

        // Modify health
        ModifyStat("health", -dmg);
        Debug.Log(gameObject.tag + " Health: " + Health);

        // Check if we died
        if (Health <= 0f)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Avoid moving and attacking when dies
        GetComponent<EnemyAI>().XMovement = 0;
        GetComponent<EnemyAI>().enabled = false;

        // Wait some time before removing entity
        yield return new WaitForSeconds(1f);

        Debug.Log("Die " + gameObject.tag);

        Destroy(gameObject);
    }

    private IEnumerator Disappear()
    {
        while (true)
        {
            if (transform.position.y < -10)
                Destroy(gameObject);

            yield return new WaitForSeconds(1);
        }
    }
}
