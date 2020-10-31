using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats
{
    // Manage level
    private int level;

    // Reference to our healthBar;
    public HealthBar healthBar;

    // Keeps track of which player we are
    private int player;

    // Coin UI
    private CoinText coinText;

    // Avoiding dead bugs
    private bool dead = false;

    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);

        // Check whether we are player 1 or 2
        player = gameObject.tag == "Player1" ? 1 : 2;

        // Get the Coin Text element
        coinText = GameObject.Find("CoinText").GetComponent<CoinText>();

        // Dissapear on height coroutine
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(Die()); 
    }

    public void Hit(float damage)
    {
        if (!dead)
        {
            // Perform Hit animation
            animation.SetTrigger("Hit");

            // Defense logic
            float dmg = damage - ((Defense + Random.Range(0, 10)) * damage / 100);

            // Modify health
            ModifyStat("health", - dmg);
            Debug.Log(gameObject.tag + " Health: " + Health);

            healthBar.SetHealth(Health);

            // Check if we died
            if (Health <= 0f && !dead)
            {
                // Avoid Hit and loot bugs
                dead = true;

                // Make a Coroutine for death, wait before removing object
                StartCoroutine(Die());
            } else
                // Play Hurt sound
                FindObjectOfType<AudioManager>().Play("Hurt");
        }
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Play Dead sound
        FindObjectOfType<AudioManager>().Play("Dead");

        PlayerControl PC = GetComponent<PlayerControl>();

        // Avoid moving
        GetComponent<Rigidbody2D>().sharedMaterial = PC.friction;
        GetComponent<CircleCollider2D>().sharedMaterial = PC.friction;

        // Set the player control not enabled after dying
        PC.enabled = false;
        GetComponent<AttackControl>().enabled = false;
        GetComponent<ClampPosition>().enabled = false;

        // Wait some time before removing entity
        yield return new WaitForSeconds(1f);

        Debug.Log("Die " + gameObject.tag);
        // SetActive to false to Player object 
        // * We don't want to destroy the object in the case of the players*
        GameObject.Find("Player" + player).SetActive(false);
    }

    private IEnumerator Disappear()
    {
        while (true)
        {
            if (transform.position.y < -10)
            {
                GameObject.Find("Player" + player).SetActive(false);

                healthBar.SetHealth(0);
            }    

            yield return new WaitForSeconds(1);
        }
    }

    // Heal our player if we pick a heart
    public void pickHeart(float life)
    {
        ModifyStat("health", life);
        healthBar.SetHealth(Health);
    }

    public void pickCoin(float coins)
    {
        coinText.setCoins(coins);
    }

}
