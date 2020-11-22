using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    // Detect the player at this distance
    [Space(10)]
    public float detectDistance;
    
    // Enemies healthbar
    public EnemyHealthBar healthBar;

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

        // Starting point of our healthbar
        healthBar.SetMaxHealth(MaxHealth);

        // Get EnemyAI script
        AI = GetComponent<EnemyAI>();

        // Dissapear on height coroutine
        StartCoroutine(Disappear());
    }

    public void Hit(float damage)
    {
        if (!dead)
        {
            // Perform Hit animation
            animation.SetTrigger("Hit");

            // Play Hurt sound
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("EnemyHurt");

            // Defense logic
            float dmg = damage - ((Defense + Random.Range(0, 2)) * damage / 100);

            // Modify health
            ModifyStat("health", -dmg);
            //Debug.Log(gameObject.tag + " Health: " + Health);

            // Update HealthBar
            healthBar.SetHealth(Health);

            // Check if we died
            if (Health <= 0f && !dead)
            {
                // Avoid Hit and loot bugs
                dead = true;

                // Make a Coroutine for death, wait before removing object
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Play Hurt sound
        //FindObjectOfType<AudioManager>().Play("EnemyDead");
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("EnemyDead");

        // Avoid moving
        GetComponent<Rigidbody2D>().sharedMaterial = AI.friction;
        GetComponent<CircleCollider2D>().sharedMaterial = AI.friction;

        // Disable collider while dying
        GetComponent<BoxCollider2D>().enabled = false;

        // Avoid moving and attacking when dies
        AI.XMovement = 0;
        AI.enabled = false;
        enabled = false;

        if (Random.value > 0.25)
            GenerateObject(transform.position.x,
                           transform.position.y + GetComponent<Renderer>().bounds.size.y / 2,
                           Random.Range(1, 3),
                           GameObject.Find("PropsParent"));

        // Wait some time before removing entity
        yield return new WaitForSeconds(1f);

        //Debug.Log("Die " + gameObject.tag);
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

    private void GenerateObject(float x, float y, int amount, GameObject parent)
    {
        float actualX = x;

        for (int i = 0; i < amount; i++)
        {
            GameObject item = loots[Random.Range(0, loots.Count)];
            item.GetComponent<ItemMovement>().despawn = true;

            var myPrefab = Instantiate(item, new Vector3(actualX, y, 0), Quaternion.identity);
            myPrefab.transform.parent = parent.transform;

            // This way all the items won't be placed in the same position
            if (i % 2 == 0)
                actualX = x + (i + 1) * 0.1f;
            else
                actualX = x - (i + 1) * 0.1f;
                
        }
    }
}
