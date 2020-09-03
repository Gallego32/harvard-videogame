using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    // Manage level
    private int level;

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
        float dmg = damage - (Defense * damage / 100);

        // Modify health
        ModifyStat("health", - dmg);
        Debug.Log(gameObject.tag + " Health: " + Health);

        // Check if we died
        if (Health <= 0f)
            StartCoroutine(Die()); 
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Set the player control not enabled after dying
        if (Equals(gameObject.tag, "Player1") || Equals(gameObject.tag, "Player2"))
        {
            GetComponent<PlayerControl>().enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<AttackControl>().enabled = false;
            GetComponent<ClampPosition>().enabled = false;
        }

        Debug.Log("Entered Waiter");
        // Wait some time before removing entity
        yield return new WaitForSeconds(1f);

        Debug.Log("Die " + gameObject.tag);
        // SetActive to false to Player object 
        // * We don't want to destroy the object in the case of the players*
        if (Equals(gameObject.tag, "Player1"))
            GameObject.Find("Player1").SetActive(false);
        else if (Equals(gameObject.tag, "Player2"))
            GameObject.Find("Player2").SetActive(false);
    }
}
