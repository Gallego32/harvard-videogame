using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    // Detect the player at this distance
    public float detectDistance;

    // Items to drop when the enemy dies
    public List<GameObject> loots;

    // Control EnemyAI
    private EnemyAI AI;

    // Bool control for enemy death
    private bool dead;

    void Start()
    {
        // Enemy starts alive
        dead = false;

        // Get EnemyAI script
        AI = GetComponent<EnemyAI>();

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
        if (Health <= 0f && !dead)
        {
            // Avoid Hit and loot bugs
            dead = true;

            // Make a Coroutine for death, wait before removing object
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Avoid moving
        GetComponent<Rigidbody2D>().sharedMaterial = AI.friction;
        GetComponent<CircleCollider2D>().sharedMaterial = AI.friction;

        // Avoid moving and attacking when dies
        AI.XMovement = 0;
        AI.enabled = false;
        enabled = false;

        if (Random.value > 0.25)
            GenerateObject(transform.position.x,
                           transform.position.y + GetComponent<Renderer>().bounds.size.y / 2,
                           Random.Range(1, 3));

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
            GameObject item = loots[Random.Range(0, loots.Count)];
            item.GetComponent<ItemMovement>().despawn = true;

            Instantiate(item, new Vector3(actualX, y, 0), Quaternion.identity);

            // This way all the items won't be placed in the same position
            if (i % 2 == 0)
                actualX = x + (i + 1) * 0.1f;
            else
                actualX = x - (i + 1) * 0.1f;
                
        }
    }
}
