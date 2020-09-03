using System;

public class BaseStats0
{
    // Declare BaseStats
    private float health, maxHealth, attack, defense, speed, range, attackSpeed;
    private string tag;
    private int layer;

    // BaseStats constructors
    public BaseStats0(float maxHealth, float attack, float defense, float speed, float range, float attackSpeed,string tag, int layer)
    {
        this.health = maxHealth > 0 ? maxHealth : 100f;
        this.maxHealth = maxHealth > 0 ? maxHealth : 100f;
        this.attack = attack > 0 ? attack : 10f;
        this.defense = defense > 0 ? defense : 10f;
        this.speed = speed > 0 ? speed : 12f;
        this.range = range > 0 ? range : 0.4f;
        this.attackSpeed = attackSpeed > 0 ? attackSpeed : 2f;
        this.tag = tag;
        this.layer = layer;
    }

    public BaseStats0()
    {
        this.health = 100f;
        this.maxHealth = 100f;
        this.attack = 10f;
        this.defense = 10f;
        this.speed = 12f;
        this.range = 0.4f;
        this.attackSpeed = 2f;
    }

    // *** Get and set functions ***

    public float Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public float MaxHealth
    {
        get { return this.maxHealth; }
        set { this.maxHealth = value; }
    }

    public float Attack
    {
        get { return this.attack; }
        set { this.attack = value; }
    }

    public float Defense
    {
        get { return this.defense; }
        set { this.defense = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }
    
    public float Range
    {
        get { return this.range; }
        set { this.range = value; }
    }

    public float AttackSpeed
    {
        get { return this.attackSpeed; }
        set { this.attackSpeed = value; }
    }

    public string Tag
    {
        get { return this.tag; }
        set { this.tag = value; }
    }

    public int Layer
    {
        get { return this.layer; }
        set { this.layer = value; }
    }

    // Increment or decrement and clamp
    public void ModifyStat(string stat, float value)
    {
        switch (stat)
        {
            case "health":
                this.health = Math.Max(0, this.health + value);
                break;
            case "maxHealth":
                this.maxHealth = Math.Max(0, this.maxHealth + value);
                break;
            case "attack":
                this.attack = Math.Max(0, this.attack + value);
                break;
            case "defense":
                this.defense = Math.Max(0, this.defense + value);
                break;
            case "speed":
                this.speed = Math.Max(0, this.speed + value);
                break;
            case "range":
                this.range = Math.Max(0, this.range + value);
                break;
            case "attackSpeed":
                this.attackSpeed = Math.Max(0, this.attackSpeed + value);
                break;
        }
    }
}
