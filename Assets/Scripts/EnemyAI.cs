using System.Collections;
using System.Collections.Generic;
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

    // Movement variables
    private float xMovement = 0f;
    private bool jump = false;
    private float speed;
    private float detectDistance;

    // Players position
    public List<Transform> playersPosition;

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

        // StartCoroutine of AI RandomMovement
        // This will only perform when the enemy hasn't found the Player
        StartCoroutine(RandomMovement());

        StartCoroutine(FollowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        // Movement animation logic
        animation.SetFloat("Speed", Mathf.Abs(xMovement));


        if (!foundPlayer)
        {
            
        }

    }

    void FixedUpdate()
    {
        // Perform Movement
        controller.Move(xMovement * Time.fixedDeltaTime, false, jump);

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
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            //Debug.Log(foundPlayer);
            if (!foundPlayer)
            {
                float direction = Random.value > 0.5 ? 1 : -1;
            xMovement = (speed / 2) * direction;

            yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));

            xMovement = 0;

            yield return new WaitForSeconds(Random.Range(2, 4));
            } else yield return new WaitForSeconds(5);
        }
    }

    // BUGGY BEHAVIOUR
    IEnumerator FollowPlayer()
    {
        while (true)
        {
            // Control variable
            int closePlayers = 0;

            foreach (Transform player in playersPosition)
            if (player.position.x > transform.position.x - detectDistance &&
                player.position.x < transform.position.x + detectDistance)
                    closePlayers++;

            foundPlayer = closePlayers > 0 ? true : false;

            if (foundPlayer)
            {
                float direction = closestPlayer(playersPosition);
                xMovement = speed * direction;
            }

            // Update every half of a second
            yield return new WaitForSeconds(0.3f);
        }
    }

    private int closestPlayer(List<Transform> players)
    {
        float closestDistance = detectDistance;
        int direction = 0;

        // Check which player is closer to the enemy
        foreach (Transform player in players)
        {
            float distance = Mathf.Abs(transform.position.x - player.position.x);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                direction = player.position.x < transform.position.x ? -1 : 1;
            }

            //Debug.Log("Distancia: " + distance);
        }
        //Debug.Log("Posicion: " + closestDistance);

        return direction;
    }

}
