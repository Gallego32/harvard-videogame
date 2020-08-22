using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Code for Controlling our players
    Depending on which player we are we'll have some different controls
    We can manage this with some tags
*/

public class PlayerControl : MonoBehaviour
{
    // Manage movement
    private CharacterController2D controller;

    // Manage RigidBody
    private Rigidbody2D rb;

    // Manage CircleCollider
    private CircleCollider2D cc;

    // Manage animations;
    private Animator animation;

    // Keeps track of the player we are
    private int player;

    // Movement variables
    private float xMovement = 0f;
    private bool jump = false;
    public float speed = 30f;

    // Moving on slopes
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;


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

        // Check whether we are player 1 or 2
        player = gameObject.tag == "Player1" ? 1 : 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement depending on the player
        xMovement = Input.GetAxisRaw("Horizontal" + player) * speed;

        animation.SetFloat("Speed", Mathf.Abs(xMovement));

        // Jump depending on player
        if (Input.GetButtonDown("Jump" + player)) {
            jump = true;
            animation.SetBool("Jump", true);
        }            
    }

    // Behaviour on landing
    public void landing() {
        //Debug.Log("Suelo");
        animation.SetBool("Jump", false);
        animation.SetBool("Falling", false);
    }

    // Behaviour on ceiling collision
    public void ceilingCollision() {
        //Debug.Log("Techo");
        // Change animation to falling
        animation.SetBool("Falling", true);
    }

    void FixedUpdate() 
    {
        // Movement
        controller.Move(xMovement * Time.fixedDeltaTime, false, jump);
        jump = false;

        if (xMovement == 0) {
            rb.sharedMaterial = friction;
            cc.sharedMaterial = friction;
        }
        else {
            rb.sharedMaterial = noFriction;
            cc.sharedMaterial = noFriction;
        }
            
        

        //Debug.Log("Sp = " + rb.velocity.y);

        // Behaviour for Animator
        if (rb.velocity.y < -0.5f)
            animation.SetBool("Falling", true);
        else {
            if (rb.velocity.y > 1f)
                animation.SetBool("Jump", true);

            //if (rb.velocity.y == 0f && !animation.GetBool("Falling")) 
                //animation.SetBool("Jump",false);

            animation.SetBool("Falling", false);
        }            

        if (controller.m_Grounded) {
            animation.SetBool("Jump",false);
            animation.SetBool("Falling", false);
            animation.SetBool("Grounded", true);
        } else 
            animation.SetBool("Grounded", false);
    }
}
