using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;
[System.Serializable]

public class Monster{
    public int monsterID;
    public int maxHp;
    public int hp;
    public int attackDamage;
    public string type;
    public bool guarded;
    //Buff
    public bool shielded;
    //Debuff
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

    //Buff
    public Buff shield;
    //Debuff
    public Debuff stun;
    public List<Debuff> dotDamageList;
    public Debuff silent;
    public Debuff blind;

    public void Initialize()
    {
        monsterPrefs = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>();
        monsterInfo = monsterPrefs.monsterDatabase[monsterID];

        this.guarded = false;
        this.stunned = false;
        this.silenced = false;
        this.blinded = false;
        this.isDead = false;

        stun = null;
        dotDamageList = new List<Debuff>();
        silent = null;
        blind = null;
    }

	public void SetHeal(int heal){
		hp += heal;
		if (hp > maxHp) {
			hp = maxHp;
		}
	}
    public void SetHeal(int heal, ChemicalStates critical)
    {
        if(this.currentChemicalState == critical)
        {
            hp += (int)(heal * 1.5f);
        }
        else
        {
            hp += heal;
        }
        
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
    public void SetDamage(int damage)
    {
        if (shielded)
        {
            Debug.Log("This monster is shielded");
            RemoveShield();
            shielded = false;
        }
        else
        {
            hp -= damage;
            if (hp <= 0) {
                hp = 0;
                Dead();
            }
        }

        Debug.Log("Get " + damage + " damage by player");
    }
    public void SetDamage(int damage, ChemicalStates critical)
    {
        if (shielded)
        {
            Debug.Log("This monster is shielded");
            RemoveShield();
            shielded = false;
        }
        else
        {
            if(this.currentChemicalState == critical)
            {
                hp -= (int)(damage * 1.5f);
            }
            else
            {
                hp -= damage;
            }
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
        }
        Debug.Log("Get " + damage + " damage by player");
    }

    public void ChangeState(ChemicalStates chemicalState)
    {
        switch (chemicalState)
        {
            case ChemicalStates.SOLID:
                switch (this.currentChemicalState)
                {
                    case ChemicalStates.SOLID:
                        Debug.Log("Do nothing");
                        break;
                    case ChemicalStates.LIQUID:
                        this.currentChemicalState = ChemicalStates.SOLID;
                        this.currentChemicalStateValue = this.solidStateValue;
                        break;
                    case ChemicalStates.GAS:
                        this.currentChemicalState = ChemicalStates.SOLID;
                        this.currentChemicalStateValue = this.solidStateValue;
                        break;
                }
                break;
            case ChemicalStates.LIQUID:
                switch (this.currentChemicalState)
                {
                    case ChemicalStates.SOLID:
                        this.currentChemicalState = ChemicalStates.LIQUID;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.LIQUID:
                        Debug.Log("Do nothing");
                        break;
                    case ChemicalStates.GAS:
                        this.currentChemicalState = ChemicalStates.LIQUID;
                        this.currentChemicalStateValue = this.liquidStateValue;
                        break;
                }
                break;
            case ChemicalStates.GAS:
                switch (this.currentChemicalState)
                {
                    case ChemicalStates.SOLID:
                        this.currentChemicalState = ChemicalStates.GAS;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.LIQUID:
                        this.currentChemicalState = ChemicalStates.GAS;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.GAS:
                        Debug.Log("Do nothing");
                        break;
                }
                break;
        }
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

    public bool isBuffPresent()
    {
        return (shield != null);
    }
    public bool isDebuffPresent()
    {
        return (stun != null) || (dotDamageList.Count != 0) || (silent != null) || (blind != null);
    }

    public void AddBuff(Buff buff)
    {
        switch (buff.GetBuffname())
        {
            case BuffName.Shield:
                shield = buff;
                shielded = true;
                break;
            default:
                Debug.Log("No corresponding buff");
                break;
        }
    }

    public void AddDebuff(Debuff debuff)
    {
        switch (debuff.GetDebuffname())
        {
            case DebuffName.Stun:
                monsterPrefs.monsterObjectList[monsterID].transform.Find("stun").gameObject.SetActive(true);
                stun = debuff;
                stunned = true;
                break;
            case DebuffName.DoteDamage:
                monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(true);
                dotDamageList.Add(debuff);
                break;
            case DebuffName.Blind:
                blind = debuff;
                blinded = true;
                break;
            case DebuffName.Silent:
                silent = debuff;
                silenced = true;
                break;
            default:
                Debug.Log("No corresponding debuff");
                break;
        }
    }

    //Shield Implementation
    public void ReduceShieldTurn()
    {
        if (shield == null)
        {
            return;
        }
        if (shield != null)
        {
            shield.RemainTurn--;
        }
        if (shield.RemainTurn == 0)
        {
            shield = null;
            shielded = false;
        }
    }
    public void ActivateShield()
    {
        if (shield != null)
        {

        }
        else
        {
            shielded = false;
        }
    }
    public void RemoveShield()
    {
        shield = null;
    }

    // Stun Implementation
    public void ReduceStunTurn()
    {
        if(stun == null)
        {
            return;
        }
        if (stun != null)
        {
            stun.RemainTurn--;
        }
        if (stun.RemainTurn == 0)
        {
            stun = null;
            stunned = false;
        }
    }
    public void ActivateStun()
    {
        if(stun != null)
        {

        }
        else
        {
            stunned = false;
        }
    }
    public void RemoveStun()
    {
        stun = null;
    }

    // DotDamage Implementation
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
        dotDamageList.Clear();
    }

    //Silent
    public void ReduceSilentTurn()
    {
        if (silent == null)
        {
            return;
        }
        if (silent != null)
        {
            silent.RemainTurn--;
        }
        if (silent.RemainTurn == 0)
        {
            silent = null;
            silenced = false;
        }
    }
    public void ActivateSilent()
    {
        if (silent != null)
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
        silent = null;
    }

    //Blind
    public void ReduceBlindTurn()
    {
        if (blind == null)
        {
            return;
        }
        if (blind != null)
        {
            blind.RemainTurn--;
        }
        if (blind.RemainTurn == 0)
        {
            blind = null;
            blinded = false;
        }
    }
    public void ActivateBlind()
    {
        if (blind != null)
        {

        }
        else
        {
            blinded = false;
        }
    }
    public void RemoveBlind()
    {
        blind = null;
    }

	public void Dead(){
        Debug.Log(monsterInfo.Mon_GoldRate);
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().dustCount += monsterInfo.Mon_GoldRate;
        //Destroy (this.gameObject);
        isDead = true;
        monsterPrefs.monsterObjectList[monsterID].SetActive(false);
        Debug.Log (this + " is Dead.");
	}

}
