﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Manage Animation
    private Animator animation;

    // Keeps track of which player we are
    private int player;

    // Start is called before the first frame update
    void Start()
    {
        // Get Animator component
        animation = GetComponent<Animator>();

        // Check whether we are player 1 or 2
        player = gameObject.tag == "Player1" ? 1 : 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack" + player))
            Attack1();
    }

    void Attack1()
    {
        animation.SetTrigger("Attack1");
    }
}
