﻿using System.Collections;
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
    private float speed;

    // Moving on slopes
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;

    // Reference to the Camera
    public GameObject camera;

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

        // Get Stats Speed
        speed = GetComponent<PlayerStats>().Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement depending on the player
        xMovement = Input.GetAxisRaw("Horizontal" + player) * speed;

        // Movement animation logic
        animation.SetFloat("Speed", Mathf.Abs(xMovement));

        if (animation.GetBool("Dead"))
            Debug.Log("Mov a 0");
            //xMovement = 0;

        // Jump depending on player
        // Second condition allows us to clamp y position
        if (Input.GetButtonDown("Jump" + player) && transform.position.y < camera.transform.position.y + 1.5f) {
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
        if (!controller.m_Grounded)
            animation.SetBool("Falling", true);
    }

    void FixedUpdate() 
    {
        // Movement
        controller.Move(xMovement * Time.fixedDeltaTime, false, jump);
        jump = false;

        // Avoid falling in slopes when the player isn't moving
        if (xMovement == 0) {
            rb.sharedMaterial = friction;
            cc.sharedMaterial = friction;
        }
        else {
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
            animation.SetBool("Grounded", false);
    }
}
