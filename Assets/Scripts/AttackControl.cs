using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    // Manage Animation
    private Animator animation;

    // Keeps track of which player we are
    private int player;

    // Only attack the layers we want
    public LayerMask canAttack;

    // Manage our attack center point
    public Transform attackCenter;

    // Get player stats
    private PlayerStats stats;

    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        // Get Animator component
        animation = GetComponent<Animator>();

        // Get Stats script
        stats = GetComponent<PlayerStats>();

        // Check whether we are player 1 or 2
        player = gameObject.tag == "Player1" ? 1 : 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack" + player) && Time.time >= nextAttackTime) 
        {
            Attack();
            nextAttackTime = Time.time + (1f / stats.AttackSpeed);
        }
            
    }

    void Attack()
    {
        // Perform Attack animation
        animation.SetTrigger("Attack");

        // We get an array of collided objects depending of the selected "canAttack" layer
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackCenter.position, stats.Range, canAttack);
        
        // This array will get both circle and box collider 2D so we need some logic
        // to remove duplicated object
        // The best way I have found is creating a list with the non repeated objects.
        List<GameObject> hitList = RemoveDuplicated(hit);

        // Call hit's enemy function
        foreach(GameObject enemy in hitList)
        {
            if (enemy != gameObject) 
            {
                if (enemy.layer == 9)
                    enemy.GetComponent<EnemyStats>().Hit(stats.Attack);
                else 
                    enemy.GetComponent<PlayerStats>().Hit(stats.Attack);
            }
        }
    }
    
    // RemoveDuplicated method so we don't hit an enemy twice
    private List<GameObject> RemoveDuplicated(Collider2D[] hit)
    {
        List<GameObject> hitList = new List<GameObject>();

        for (int i = 0; i < hit.Length; i ++)
        {
            if (!hitList.Contains(hit[i].gameObject))
                hitList.Add(hit[i].gameObject);
        }
        return hitList;
    }

    /*      DEBUG FUNCTION
    void OnDrawGizmosSelected()
	{
        if (attackCenter == null)
            return;

		Gizmos.DrawWireSphere(attackCenter.position, 0.2f);
	}
    */
    
}
