using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    We'll be able to make FADE IN and FADE OUT with this script attached to a PANEL in the canvas
 */


public class Fader : MonoBehaviour
{
    private Animator animator;

   public static float initialSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialSpeed = animator.speed;
        animator.enabled = false;
    }
    
    public void FadeIn()
    {
        animator.enabled = true;
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    // This will be as an event at the end of Fade Out to ensure it resets
    private void DeactivateAnimator()
    {
        animator.enabled = false;
        animator.speed = initialSpeed;
    }

    // Standard fade in and out
    public void FadeFor()
    {
        FadeIn();
        FadeOut();
    }

    // Fading for an amount of time
    public IEnumerator FadeFor(float seconds)
    {
        FadeIn();
        yield return new WaitForSeconds(seconds);
        FadeOut();
    }

    // FADE FOR but selecting speed for the transition
    public IEnumerator FadeFor(float seconds, float speed)
    {
        animator.speed = speed;
        
        FadeIn();
        yield return new WaitForSeconds(seconds);
        FadeOut();
    }

    // FADE FOR but selecting speed for each part
    public IEnumerator FadeFor(float seconds, float inSpeed, float outSpeed)
    {
        // FadeIn speed
        animator.speed = inSpeed;
        
        FadeIn();
        yield return new WaitForSeconds(seconds);

        // FadeOut speed
        animator.speed = outSpeed;

        FadeOut();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(FadeFor(0));
        if (Input.GetKeyDown(KeyCode.G))
            StartCoroutine(FadeFor(0, 3f));
        if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine(FadeFor(0, 8f, initialSpeed));
    }
       
}
