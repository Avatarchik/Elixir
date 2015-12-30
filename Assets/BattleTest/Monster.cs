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
    public Monster ()
    {
        this.hp = 20;
        this.attackDamage = 5;
        this.type = MonsterType.None;
        this.boilingPoint = 100;
        this.meltingPoint = 0;
        // this.cards = new List<Card>(); // Current not used.
    }
    
    // Default image.
    public Monster (int hp, int attackDamage, MonsterType type, int boilingPoint, int meltingPoint)
    {
        this.hp = hp;
        this.attackDamage = attackDamage;
        this.type = type;
        this.boilingPoint = boilingPoint;
        this.meltingPoint = meltingPoint;
        // this.cards = new List<Card>(); // Current not used.
    }
    public Monster (string imagePath, int hp, int attackDamage, MonsterType type, int boilingPoint, int meltingPoint)
    {
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        this.hp = hp;
        this.attackDamage = attackDamage;
        this.type = type;
        this.boilingPoint = boilingPoint;
        this.meltingPoint = meltingPoint;
        // this.cards = new List<Card>(); // Current not used.
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
