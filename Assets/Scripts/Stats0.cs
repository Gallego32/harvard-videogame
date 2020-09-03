using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats0 : MonoBehaviour
{
    // Reference to our BaseStats class
    public BaseStats0 stats;

    // Manage animations
    private Animator animation;

    // Manage level
    private int level;

    // Base public stats
    public float baseMaxHealth, baseAttack, baseDefense, baseSpeed, baseRange, baseAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        // Create a BaseStats object
        stats = new BaseStats0(baseMaxHealth, baseAttack, baseDefense, baseSpeed, baseRange, baseAttackSpeed,gameObject.tag, gameObject.layer);
        Debug.Log(stats.Tag + " Health: " + stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //    StartCoroutine(Die()); 
        
        //BUG AUMENTA VIDA
        //Hit(1);
            
        //stats.ModifyStat("health", -0.1f);
    }

    public void Hit(float damage)
    {
        // Perform Hit animation
        animation.SetTrigger("Hit");

        // Defense logic
        float dmg = damage - (stats.Defense * damage / 100);

        // Modify health
        stats.ModifyStat("health", - dmg);
        Debug.Log(stats.Tag + " Health: " + stats.Health);

        // Check if we died
        if (stats.Health <= 0f)
            StartCoroutine(Die()); 
    }

    IEnumerator Die()
    {
        // Perform Death animation
        animation.SetBool("Dead", true);

        // Set the player control not enabled after dying
        if (Equals(stats.Tag, "Player1") || Equals(stats.Tag, "Player2"))
        {
            GetComponent<PlayerControl>().enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<AttackControl>().enabled = false;
            GetComponent<ClampPosition>().enabled = false;
        }

        Debug.Log("Entered Waiter");
        // Wait some time before removing entity
        yield return new WaitForSeconds(1f);

        Debug.Log("Die " + stats.Tag);
        if (Equals(stats.Tag, "Player1"))
            GameObject.Find("Player1").SetActive(false);
        else if (Equals(stats.Tag, "Player2"))
            GameObject.Find("Player2").SetActive(false);
        else
            Destroy(gameObject);
    }

}
