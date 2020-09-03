using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Base public stats
    public float baseMaxHealth, baseAttack, baseDefense, baseSpeed, baseRange, baseAttackSpeed;

    // Manage Animator for every entity
    protected Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        // Set variables to our public value or fixed value 
        Health = baseMaxHealth > 0 ? baseMaxHealth : 100f;
        MaxHealth = baseMaxHealth > 0 ? baseMaxHealth : 100f;
        Attack = baseAttack > 0 ? baseAttack : 10f;
        Defense = baseDefense > 0 ? baseDefense : 10f;
        Speed = baseSpeed > 0 ? baseSpeed : 12f;
        Range = baseRange > 0 ? baseRange : 0.4f;
        AttackSpeed = baseAttackSpeed > 0 ? baseAttackSpeed : 2f;

        // Get Animator component
        animation = GetComponent<Animator>();
    
        Debug.Log(AttackSpeed);
    }

    // ModifyStat function to contorl its value
    public void ModifyStat(string stat, float value)
    {
        switch (stat)
        {
            case "health":
                this.Health = Mathf.Max(0, this.Health + value);
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
        }
    }
}
