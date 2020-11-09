using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Control visual Health with a slider
    public Slider slider;

    // When we die, put a skull instead of a heart
    public GameObject skull;
    public GameObject heart;
    private bool death = false;

    public void SetHealth(float health)
    {
        slider.value = health;

        if (slider.value == 0)
            SetSkull();
        else if (death)
            SetHeart();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    // Put a skull instead of a heart
    private void SetSkull()
    {
        heart.SetActive(false);
        skull.SetActive(true);
        death = true;
    }

    private void SetHeart()
    {
        heart.SetActive(true);
        skull.SetActive(false);
        death = false;
    }
}
