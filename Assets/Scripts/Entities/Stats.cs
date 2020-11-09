using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is a standard class for generating stats for players and enemies
 * This will be inherited by both players and enemies
 */

public class Stats : MonoBehaviour
{
    // Declare Stats variables
    // *** Get and Set functions ***
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Speed { get; set; }
    public float Range { get; set; }
    public float AttackSpeed { get; set; }
    public float CriticDamage { get; set; }

    // Base public stats
    [Header("Stats")]
    public float baseMaxHealth;
    public float baseAttack, baseDefense, baseSpeed, baseRange, baseAttackSpeed, baseCriticDamage;

    // Manage Animator for every entity
    protected Animator animation;

    void Awake()
    {
        // Set variables to our public value or fixed value 
        Health = baseMaxHealth > 0 ? baseMaxHealth : 100f;
        MaxHealth = baseMaxHealth > 0 ? baseMaxHealth : 100f;
        Attack = baseAttack > 0 ? baseAttack : 10f;
        Defense = baseDefense > 0 ? baseDefense : 10f;
        Speed = baseSpeed > 0 ? baseSpeed : 12f;
        Range = baseRange > 0 ? baseRange : 0.4f;
        AttackSpeed = baseAttackSpeed > 0 ? baseAttackSpeed : 2f;
        CriticDamage = baseCriticDamage >= 0 ? baseCriticDamage : 0;

        // Get Animator component
        animation = GetComponent<Animator>();
    }

    // ModifyStat function to contorl its value
    public void ModifyStat(string stat, float value)
    {
        switch (stat)
        {
            case "health":
                this.Health = Mathf.Clamp(this.Health + value, 0, MaxHealth);
                break;
            case "maxHealth":
                this.MaxHealth = Mathf.Max(0, this.MaxHealth + value);
                break;
            case "attack":
                this.Attack = Mathf.Max(0, this.Attack + value);
                break;
            case "defense":
                this.Defense = Mathf.Max(0, this.Defense + value);
                break;
            case "speed":
                this.Speed = Mathf.Max(0, this.Speed + value);
                break;
            case "range":
                this.Range = Mathf.Max(0, this.Range + value);
                break;
            case "attackSpeed":
                this.AttackSpeed = Mathf.Max(0, this.AttackSpeed + value);
                break;
            case "criticDamage":
                this.CriticDamage = Mathf.Max(0, this.CriticDamage + value);
                break;
        }
    }

}
