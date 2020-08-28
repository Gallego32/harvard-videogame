using System;

public class BaseStats
{
    // Declare BaseStats
    private float health, maxHealth, attack, defense, speed;
    private string tag;
    private int layer;

    public BaseStats(float maxHealth, float attack, float defense, float speed, string tag, int layer)
    {
        this.health = maxHealth > 0 ? maxHealth : 100;
        this.maxHealth = maxHealth > 0 ? maxHealth : 100;
        this.attack = attack > 0 ? attack : 10;
        this.defense = defense > 0 ? defense : 10;
        this.speed = speed > 0 ? speed : 12;
        this.tag = tag;
        this.layer = layer;
    }

    public BaseStats()
    {
        this.health = 100;
        this.maxHealth = 100;
        this.attack = 10;
        this.defense = 10;
        this.speed = 12;
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
                this.health = Math.Max(0, this.maxHealth + value);
                break;
            case "attack":
                this.health = Math.Max(0, this.attack + value);
                break;
            case "defense":
                this.health = Math.Max(0, this.defense + value);
                break;
            case "speed":
                this.health = Math.Max(0, this.speed + value);
                break;
        }
    }
}
