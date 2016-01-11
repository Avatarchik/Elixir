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
    public bool stunned;

    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;

    List<Buff> buffs = new List<Buff>();
    List<Debuff> debuffs = new List<Debuff>();
    
	void Update(){

		if (hp <= 0) {
			Dead();
		}
	}

    // Apply default stats.
    public void SetStat ()
    {
        this.maxHp = 20;
        this.hp = maxHp;
        this.attackDamage = 5;
        this.type = MonsterType.None;
        this.stunned = false;
        this.currentChemicalState = ChemicalStates.LIQUID;
        this.currentChemicalStateValue = 1;
        this.solidStateValue = 1;
        this.liquidStateValue = 2;
        this.gasStateValue = 3;
    }
    
    // Only apply image.
    public void SetStat (string imagePath)
    {
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        SetStat();
    }
    
    // Default image.
    public void SetStat (int hp, int attackDamage, MonsterType type, int boilingPoint, int meltingPoint)
    {
        this.maxHp = hp;
        this.hp = maxHp;
        this.attackDamage = attackDamage;
        this.type = type;
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

    //Chemical State
    public void IncrementCSVal()
    {
        switch (currentChemicalState)
        {
            case ChemicalStates.SOLID:
                currentChemicalStateValue++;
                if (currentChemicalStateValue > solidStateValue)
                {
                    currentChemicalState = ChemicalStates.LIQUID;
                    currentChemicalStateValue = 1;
                }
                break;
            case ChemicalStates.LIQUID:
                currentChemicalStateValue++;
                if (currentChemicalStateValue > liquidStateValue)
                {
                    currentChemicalState = ChemicalStates.GAS;
                    currentChemicalStateValue = 1;
                }
                break;
            case ChemicalStates.GAS:
                if(currentChemicalStateValue >= gasStateValue)
                {
                    Debug.Log("Cannot increase chemical state value");
                }
                else
                {
                    currentChemicalStateValue++;
                }
                
                break;
        }
    }
    public void DecrementCSVal()
    {
        switch (currentChemicalState)
        {
            case ChemicalStates.SOLID:
                if (currentChemicalStateValue == 1)
                {
                    Debug.Log("Cannot decrease chemical state value");
                }
                else
                {
                    currentChemicalStateValue--;
                }
                break;
            case ChemicalStates.LIQUID:
                currentChemicalStateValue--;
                if (currentChemicalStateValue <= 0)
                {
                    currentChemicalState = ChemicalStates.SOLID;
                    currentChemicalStateValue = solidStateValue;
                }
                break;
            case ChemicalStates.GAS:
                currentChemicalStateValue--;
                if (currentChemicalStateValue <= 0)
                {
                    currentChemicalState = ChemicalStates.LIQUID;
                    currentChemicalStateValue = liquidStateValue;
                }
                break;
        }
    }

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        Debug.Log("Add buff to monster");
        //PrintAllBuffAndDebuff();
    }
    
    public void AddDebuff(Debuff debuff)
    {
        debuffs.Add(debuff);
        Debug.Log("Add debuff to monster");
        //PrintAllBuffAndDebuff();
    }

    public void ReduceDebuffTurn()
    {
        foreach(Debuff debuff in debuffs)
        {
            debuff.RemainTurn--;
            Debug.Log("Debuff turn remaining: " + debuff.RemainTurn);
        }
    }

    public void ActivateDebuff()
    {
        foreach (Debuff debuff in debuffs)
        {

            if(debuff.GetDebuffname().Equals(DebuffName.DoteDamage))//Activate Dot Damage
            {
                Debug.Log("This Debuff is Dot Damage");
                SetDamage(debuff.DebuffDamage);
            }

            if (debuff.GetDebuffname().Equals(DebuffName.Stun))
            {
                //Stun Enemy
                stunned = true;
            }

        }
    }
    
    public void RemoveBuff()
    {
        //NotImplemented
    }
    
    public void RemoveAllBuff()
    {
        buffs = new List<Buff>();
        Debug.Log("All buffs are removed");
    }

    public void RemoveDebuff()
    {
        // FIXME : Not implemented.
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff debuff in debuffs)
        {
            if (debuff.RemainTurn == 0)
            {
                debuffsToDestroy.Add(debuff);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            debuffs.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
    }
    
    public void RemoveAllDebuff()
    {
        debuffs = new List<Debuff>();
        Debug.Log("All debuffs are removed");
    }

    // using test.
    /*
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
<<<<<<< HEAD
<<<<<<< HEAD

	public void Dead(){
		Destroy (this);
		Debug.Log (this+" is Dead.");
=======
=======
>>>>>>> origin/master
    */
	// Use this for initialization
	void Start () {
	
>>>>>>> origin/master
	}

}
