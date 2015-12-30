using UnityEngine;
using System.Collections;

public enum MonsterType
{
    None
}

public class Monster : MonoBehaviour {

    public new SpriteRenderer renderer;
    public int hp;
    public int attackDamage;
    public MonsterType type;
    public int boilingPoint;
    public int meltingPoint;
    // List<Card> cards; // Current not used.
    
    // Apply default stats.
    public void SetStat ()
    {
        this.hp = 20;
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
        this.hp = hp;
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
