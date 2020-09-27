using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    // Manage level
    private int level;

    // Reference to our healthBar;
    public HealthBar healthBar;

    // Keeps track of which player we are
    private int player;

    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);

        // Check whether we are player 1 or 2
        player = gameObject.tag == "Player1" ? 1 : 2;

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
        // Perform Hit animation
        animation.SetTrigger("Hit");

        // Defense logic
        float dmg = damage - ((Defense + Random.Range(0, 10)) * damage / 100);

        // Modify health
        ModifyStat("health", - dmg);
        Debug.Log(gameObject.tag + " Health: " + Health);

        healthBar.SetHealth(Health);

        // Check if we died
        if (Health <= 0f)
            StartCoroutine(Die()); 
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Set the player control not enabled after dying

        GetComponent<PlayerControl>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
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

    public void pickHeart(float life)
    {
        Health = Mathf.Min(MaxHealth, Health + life);
        healthBar.SetHealth(Health);
    }
}
