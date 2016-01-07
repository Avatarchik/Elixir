using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class Monster : MonoBehaviour {

    public new SpriteRenderer renderer;
    public int maxHp;
    public int hp;
    public int attackDamage;
    public MonsterType type;
    public int boilingPoint;
    public int meltingPoint;
    // List<Card> cards; // Current not used.
    
    public Phase phase;
    
    List<Buff> buffs;
    List<Debuff> debuffs;
    
    // Apply default stats.
    public void SetStat ()
    {
        this.maxHp = 20;
        this.hp = maxHp;
        this.attackDamage = 5;
        this.type = MonsterType.None;
        this.boilingPoint = 100;
        this.meltingPoint = 0;
        
        InitializeBuffAndDebuff();
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
        
        InitializeBuffAndDebuff();
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

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        Debug.Log("Add buff to monster");
        PrintAllBuffAndDebuff();
    }
    
    public void AddDebuff(Debuff debuff)
    {
        debuffs.Add(debuff);
        Debug.Log("Add debuff to monster");
        PrintAllBuffAndDebuff();
    }
    
    public void RemoveBuff()
    {
        // FIXME : Not implemented.
    }
    
    public void RemoveAllBuff()
    {
        buffs = new List<Buff>();
        Debug.Log("All buffs are removed");
    }

    public void RemoveDebuff()
    {
        // FIXME : Not implemented.
    }
    
    public void RemoveAllDebuff()
    {
        debuffs = new List<Debuff>();
        Debug.Log("All debuffs are removed");
    }

    void InitializeBuffAndDebuff()
    {
        buffs = new List<Buff>();
        debuffs = new List<Debuff>();
    }

    // using test.
    void PrintAllBuffAndDebuff()
    {
        string buffList = "Buff : ";
        foreach (var buff in buffs)
        {
            buffList += buff.GetBuffname().ToString();
            buffList += ", ";
        }
        Debug.Log(buffList);
        
        string debuffList = "Debuff : ";
        foreach (var debuff in debuffs)
        {
            debuffList += debuff.GetDebuffname().ToString();
            debuffList += ", ";
        }
        Debug.Log(debuffList);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


}
