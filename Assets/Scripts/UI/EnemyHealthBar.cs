using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Control visual Health with a slider
    public Slider slider;

    public Animator animator;

    public void SetHealth(float health)
    {
        // Set actual health
        slider.value = health;
        
        // Start Animation
        animator.SetTrigger("Appear");
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
