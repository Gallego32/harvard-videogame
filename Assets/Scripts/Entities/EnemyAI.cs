using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Manage movement
    private CharacterController2D controller;

    // Manage CircleCollider
    private CircleCollider2D cc;

    // Manage animations;
    private Animator animation;

    // Moving on slopes
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;

    // Manage RigidBody
    private Rigidbody2D rb;

    // Manage Stats
    private EnemyStats stats;

    // Manage our attack center point
    public Transform attackCenter;
    public Transform holeDetector;

    // Only attack the layers we want
    public LayerMask canAttack;
    public LayerMask shouldJump;

    // Movement variables
    private float xMovement;
    
    public float XMovement
    {
        get { return this.xMovement; }
        set { this.xMovement = value; }
    }

    private bool jump;
    private float speed;
    private float detectDistance;

    // Players position
    private List<Transform> playersPosition;

    // Keeps track of when the enemy has to follow the player
    private bool foundPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get Controller2D component
        controller = GetComponent<CharacterController2D>();
        //Get CircleCollider2D component
        cc = GetComponent<CircleCollider2D>();
        // Get Animator component
        animation = GetComponent<Animator>();
        // Get RigidBody2D Component
        rb = GetComponent<Rigidbody2D>();

        // Get and set Stats variables
        stats = GetComponent<EnemyStats>();

        speed = stats.Speed;
        detectDistance = stats.detectDistance;

        xMovement = 0f;
        jump = false;

        SetPlayers();

        // StartCoroutine of AI RandomMovement
        // This will only perform when the enemy hasn't found the Player
        StartCoroutine(RandomMovement());

        // StartCoroutine FollowPlayer which will allow the enemy to know if there are any
        // players nearby and which one is the closest one
        StartCoroutine(FollowPlayer());

        // The enemy will jump if it collides with something in the way
        StartCoroutine(ShouldJump());

        StartCoroutine(JumpOverHoles());
    }

    // Update is called once per frame
    void Update()
    {
        // Movement animation logic
        animation.SetFloat("Speed", Mathf.Abs(xMovement));

        if (!foundPlayer)
        {
            // If the enemy is going to fall change direction
            Collider2D[] hit = Physics2D.OverlapCircleAll(holeDetector.position, 0.05f, shouldJump);
            if (hit.Length == 0)
                xMovement *= -1;
        }

    }

    void FixedUpdate()
    {
        // Perform Movement
        controller.Move(xMovement * Time.fixedDeltaTime, false, jump);
        jump = false;

        // Avoid falling in slopes when the enemy isn't moving
        if (xMovement == 0)
        {
            rb.sharedMaterial = friction;
            cc.sharedMaterial = friction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
            cc.sharedMaterial = noFriction;
        }

        // Behaviour for Animator
        if (rb.velocity.y < -0.5f)
            animation.SetBool("Falling", true);
        else {
            if (rb.velocity.y > 1f)
                animation.SetBool("Jump", true);

            animation.SetBool("Falling", false);
        }            

        if (controller.m_Grounded) {
            animation.SetBool("Jump",false);
            animation.SetBool("Falling", false);
            animation.SetBool("Grounded", true);
        } else 
        {    
            animation.SetBool("Grounded", false);
            jump = false;
        }
    }

    // Performed only if the players are far enough
    // The enemy will walk to a random direction for a random time
    IEnumerator RandomMovement()
    {
        xMovement = 0f;

        yield return Time.deltaTime;

        while (true)
        {
            if (!foundPlayer)
            {
                float direction = Random.value > 0.5 ? 1 : -1;
                xMovement = (speed / 2) * direction;

                

                yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));

                xMovement = 0;

                yield return new WaitForSeconds(Random.Range(2, 4));
            } else yield return new WaitForSeconds(3);
        }
    }

    // The enemy will detect the player and will choose to go after the closest player
    IEnumerator FollowPlayer()
    {
        while (true)
        {
            // Control variable
            int closePlayers = 0;

            foreach (Transform player in playersPosition)
                if (player.position.x > transform.position.x - detectDistance &&
                    player.position.x < transform.position.x + detectDistance &&
                    player.gameObject.active)
                    closePlayers++;

            foundPlayer = closePlayers > 0 ? true : false;

            // Attack and chasing after logic
            if (foundPlayer)
            {
                // We want to know which is the closest player
                float direction = closestPlayer(playersPosition);
                xMovement = speed * direction;

                foreach (Transform player in playersPosition)
                {
                    if (player.position.x > transform.position.x - stats.Range * 3/2 &&
                        player.position.x < transform.position.x + stats.Range * 3/2 &&
                        player.position.y > transform.position.y - stats.Range * 3/2 &&
                        player.position.y < transform.position.y + stats.Range * 3/2 &&
                        player.gameObject.active)
                        xMovement = 0;
                }

                if (xMovement == 0)
                {
                    // Perform Attack animation
                    animation.SetTrigger("Attack");
                    // Flip the Enemy if the player is behind
                    
                    yield return new WaitForSeconds(1f / stats.AttackSpeed);

                    if (direction == -1 && controller.LookingRight() || direction == 1 && !controller.LookingRight())
                        controller.Flip();

                }   else 
                {
                        /*if (Mathf.Abs(rb.velocity.x) <= 0.5f)
                        jump = true;
                        */    
                    yield return new WaitForSeconds(0.1f);      
                }
            } else yield return new WaitForSeconds(0.3f);        
        }
    }

    // The enemy will jump if it collides with something in the way
    IEnumerator ShouldJump()
    {
        while (true)
        {
            if (foundPlayer && Mathf.Abs(xMovement) > 0 && controller.m_Grounded)
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(attackCenter.position, stats.Range / 2, shouldJump);

                if (hit.Length > 0)
                    jump = true;
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator JumpOverHoles()
    {
        while (true)
        {
            if (foundPlayer && Mathf.Abs(xMovement) > 0 && controller.m_Grounded)
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(holeDetector.position, 0.05f, shouldJump);

                //Debug.Log(hit.Length);
                if (hit.Length == 0)
                    jump = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    // Attack will be performed in an animation Event
    public void Attack()
    {
        // We get an array of collided objects depending of the selected "canAttack" layer
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackCenter.position, stats.Range, canAttack);
        
        // This array will get both circle and box collider 2D so we need some logic
        // to remove duplicated object
        // The best way I have found is creating a list with the non repeated objects.
        List<GameObject> hitList = RemoveDuplicated(hit);

        // Damage Logic for critic attack
        float damage = stats.Attack;
        if (Random.value > 0.5)
            damage += Random.Range(0, stats.CriticDamage);

        // Call hit's enemy function
        foreach(GameObject enemy in hitList)
        {
            if (enemy != gameObject) 
            {
                if (enemy.layer == 9)
                    enemy.GetComponent<EnemyStats>().Hit(damage);
                else 
                    enemy.GetComponent<PlayerStats>().Hit(damage);
            }
        }
    }

    private int closestPlayer(List<Transform> players)
    {
        float closestDistance = detectDistance;
        int direction = 0;

        // Check which player is closer to the enemy
        foreach (Transform player in players)
        {
            if (player.gameObject.active)
            {
                float distance = Mathf.Abs(transform.position.x - player.position.x);
            
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    direction = player.position.x < transform.position.x ? -1 : 1;
                }
            }     
        }
        return direction;
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

    // Find our players
    private void SetPlayers()
    {
        playersPosition = new List<Transform>();

        GameObject temp = GameObject.FindGameObjectWithTag("Player1");

        if (temp)
            playersPosition.Add(temp.transform);

        temp = GameObject.FindGameObjectWithTag("Player2");

        if (temp)
            playersPosition.Add(temp.transform);
    }

    /*      DEBUG FUNCTION
    void OnDrawGizmosSelected()
	{
        if (attackCenter == null)
            return;

		Gizmos.DrawWireSphere(holeDetector.position, 0.05f);
        Gizmos.DrawWireSphere(attackCenter.position, 0.4f);
        //Gizmos.DrawWireSphere(new Vector3(holeDetector.position.x + .15f, holeDetector.position.y - .1f, 0), 0.05f);
	}*/
}
