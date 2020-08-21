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

    // Manage animations;
    private Animator animation;

    // Keeps track of the player we are
    private int player;

    private float xMovement = 0f;
    private bool jump = false;
    public float speed = 30f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animation = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
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
        //Movement
        controller.Move(xMovement * Time.fixedDeltaTime, false, jump);
        jump = false;

        //Debug.Log("Sp = " + rb.velocity.y);

        // Behaviour for Animator
        if (rb.velocity.y < -0.5f)
            animation.SetBool("Falling", true);
        else {
            if (rb.velocity.y > 1f)
                animation.SetBool("Jump", true);

            if (rb.velocity.y == 0f) 
                animation.SetBool("Jump",false);

            animation.SetBool("Falling", false);
        }
            

            

    }
}
