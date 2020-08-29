using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Reference to our BaseStats class
    private BaseStats stats;

    // Manage animations
    private Animator animation;

    // Manage level
    private int level;

    // Base public stats
    public float baseMaxHealth, baseAttack, baseDefense, baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        // Create a BaseStats object
        stats = new BaseStats(baseMaxHealth, baseAttack, baseDefense, baseSpeed, gameObject.tag, gameObject.layer);
        Debug.Log(stats.Tag + " " + stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(Die()); 
            
        //stats.ModifyStat("health", -0.1f);
    }

    void Hit(float damage)
    {
        // Perform Hit animation
        animation.SetTrigger("Hit");

        // Modify health
        stats.ModifyStat("health", damage - stats.Defense);

        // Check if we died
        if (stats.Health <= 0f)
            StartCoroutine(Die()); 
    }

    IEnumerator Die()
    {
        animation.SetBool("Dead", true);

        Debug.Log("Entered Waiter");
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Die " + stats.Tag);
        if (Equals(stats.Tag, "Player1"))
            GameObject.Find("Player1").SetActive(false);
        else if (Equals(stats.Tag, "Player2"))
            GameObject.Find("Player2").SetActive(false);
        else
            Destroy(gameObject);
    }

}
