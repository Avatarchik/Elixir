using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MonsterType
{
    None
}

public enum Buff
{
    None
}

public enum Debuff
{
    None,
    Stun
}

public class Monster : MonoBehaviour {

    public new SpriteRenderer renderer;
    public int maxHp;
    public int hp;
    public int attackDamage;
    public MonsterType type;
    public int boilingPoint;
    public int meltingPoint;
    // List<Card> cards; // Current not used.
    
    // List<Buff> buffs;
    // List<Debuff> debuffs;
    
    // Apply default stats.
    public void SetStat ()
    {
        this.maxHp = 20;
        this.hp = maxHp;
        this.attackDamage = 5;
        this.type = MonsterType.None;
        this.boilingPoint = 100;
        this.meltingPoint = 0;
    }
    
    // Only apply image.
    public void SetStat (string imagePath)
    {
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        SetStat();
        // this.cards = new List<Card>(); // Current not used.
    }
    
    // Default image.
    public void SetStat (int hp, int attackDamage, MonsterType type, int boilingPoint, int meltingPoint)
    {
        this.maxHp = hp;
        this.hp = maxHp;
        this.attackDamage = attackDamage;
        this.type = type;
        this.boilingPoint = boilingPoint;
        this.meltingPoint = meltingPoint;
        // this.cards = new List<Card>(); // Current not used.
    }
    public void SetStat (string imagePath, int hp, int attackDamage, MonsterType type, int boilingPoint, int meltingPoint)
    {
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        SetStat(hp, attackDamage, type, boilingPoint, meltingPoint);
    }
    
    public void SetDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
        Debug.Log("Get " + damage + " damage by player");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
