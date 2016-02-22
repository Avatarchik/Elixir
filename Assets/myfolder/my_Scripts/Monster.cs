using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class Monster : MonoBehaviour {
    public int monsterID;
    public int maxHp;
    public int hp;
    public int attackDamage;
    public string type;
    public bool guarded;
    public bool stunned;
    public bool silenced;
    public bool blinded;
    public bool isDead;

    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;
    public ChemicalStates criticalTarget;

    public MonsterPrefs monsterPrefs;
	public baseMonster monsterInfo;

    List<Debuff> stunList;
    List<Debuff> dotDamageList;
    List<Debuff> silentList;
    List<Debuff> blindList;

    public void Initialize()
    {
        monsterPrefs = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>();
        monsterInfo = monsterPrefs.monsterDatabase[monsterID];

        this.guarded = false;
        this.stunned = false;
        this.silenced = false;
        this.blinded = false;
        this.isDead = false;

        stunList = new List<Debuff>();
        dotDamageList = new List<Debuff>();
        silentList = new List<Debuff>();
        blindList = new List<Debuff>();
    }

	public void SetHeal(int heal){
		hp += heal;
		if (hp > maxHp) {
			hp = maxHp;
		}
	}


    public void SetDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) {
            hp = 0;
            isDead = true;
            Dead();
        }
            
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

    // Stun Implementation
    public void AddStun(Debuff stun)
    {
        monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        stunned = true;
        stunList.Add(stun);
    }
    public void ReduceStunTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff stun in stunList)
        {
            stun.RemainTurn--;
            if (stun.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(stun);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            stunList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (stunList.Count >= 1)
        {
            monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        }
        else
        {
            monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(false);
        }
    }
    public void ActivateStun()
    {
        if(stunList.Count >= 1)
        {
            stunned = true;
        }
        else
        {
            stunned = false;
        }
    }
    public void RemoveStun()
    {
        stunList.Clear();
    }

    // DotDamage Implementation
    public void AddDotDamage(Debuff dotDamage)
    {
        monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(true);
        dotDamageList.Add(dotDamage);
    }
    public void ReduceDotDamageTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff dotDamage in dotDamageList)
        {
            dotDamage.RemainTurn--;
            if (dotDamage.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(dotDamage);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            dotDamageList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (dotDamageList.Count >= 1)
        {
            monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(true);
        }
        else
        {
            monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(false);
        }
    }
    public void ActivateDotDamage()
    {
        foreach(Debuff dotDamage in dotDamageList)
        {
            SetDamage(dotDamage.DebuffDamage);
            if(hp <= 0)
            {
                Dead();
                break;
            }
        }
    }
    public void RemoveDotDamage()
    {
        stunList.Clear();
    }

    //Silent
    public void AddSilent(Debuff silent)
    {
        //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        silenced = true;
        silentList.Add(silent);
    }
    public void ReduceSilentTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff silent in silentList)
        {
            silent.RemainTurn--;
            if (silent.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(silent);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            silentList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (silentList.Count >= 1)
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        }
        else
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(false);
        }
    }
    public void ActivateSilent()
    {
        if (silentList.Count >= 1)
        {
            silenced = true;
        }
        else
        {
            silenced = false;
        }
    }
    public void RemoveSilent()
    {
        silentList.Clear();
    }

    //Blind
    public void AddBlind(Debuff silent)
    {
        //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        blinded = true;
        blindList.Add(silent);
    }
    public void ReduceBlindTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff blind in blindList)
        {
            blind.RemainTurn--;
            if (blind.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(blind);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            blindList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (blindList.Count >= 1)
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
        }
        else
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(false);
        }
    }
    public void ActivateBlind()
    {
        if (blindList.Count >= 1)
        {
            blinded = true;
        }
        else
        {
            blinded = false;
        }
    }
    public void RemoveBlind()
    {
        blindList.Clear();
    }

	public void Dead(){
        Debug.Log(monsterInfo.Mon_GoldRate);
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().dustCount += monsterInfo.Mon_GoldRate;
        //Destroy (this.gameObject);
        monsterPrefs.monsterObjectList[monsterID].SetActive(false);
        Debug.Log (this + " is Dead.");
	}
	// Use this for initialization
	void Start () {
	
	}

}
