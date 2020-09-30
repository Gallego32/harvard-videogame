using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public float detectDistance;

    public List<GameObject> loots;

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
        float dmg = damage - ((Defense + Random.Range(0, 2)) * damage / 100);

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

        if (Random.value > 0.5)
            GenerateObject(transform.position.x,
                           transform.position.y + GetComponent<Renderer>().bounds.size.y / 2,
                           Random.Range(1, 3));

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

    private void GenerateObject(float x, float y, int amount)
    {
        float actualX = x;

        for (int i = 0; i < amount; i++)
        {
            Instantiate(loots[Random.Range(0, loots.Count)], new Vector3(actualX, y, 0), Quaternion.identity);

            if (i % 2 == 0)
                actualX = x + (i + 1) * 0.1f;
            else
                actualX = x - (i + 1) * 0.1f;
                
        }
    }
}
