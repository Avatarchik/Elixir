using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

[System.Serializable]
public class baseCharacter{

	private string CharacterName;

    //stats
    public int level;
	public float MAX_HP=200.0f;
	public float HP;
    public float AttackDamage;
    public ChemicalStates criticalTarget;
    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;
    public int dodgeRate;

    public List<Buff> buffs = new List<Buff>();
    public List<Buff> dodgeList = new List<Buff>();
    public List<Buff> immuneCriticalTargetList = new List<Buff>();
    public List<Buff> debuffImmuneList = new List<Buff>();
    public List<Buff> guardStateChangeList = new List<Buff>();
    public List<Buff> immuneHeatList = new List<Buff>();
    public List<Buff> damageResistanceList = new List<Buff>();
    public List<Buff> dotHealList = new List<Buff>();

    public List<Debuff> dotDamage = new List<Debuff>();


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
                if (currentChemicalStateValue >= gasStateValue)
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

    //Dodge
    public void AddDodge(Buff dodge)
    {
        //this.transform.Find("stun").gameObject.SetActive(true);
        dodgeList.Add(dodge);
    }
    public void ReduceDodgeTurn()
    {
        List<Buff> buffsToDestroy = new List<Buff>();
        foreach (Buff dodge in dodgeList)
        {
            dodge.RemainTurn--;
            if (dodge.RemainTurn <= 0)
            {
                buffsToDestroy.Add(dodge);
            }
        }
        foreach (Buff buffToDestroy in buffsToDestroy)
        {
            dodgeList.Remove(buffToDestroy);
        }
        buffsToDestroy.Clear();
        if (dodgeList.Count >= 1)
        {
            //this.transform.Find("stun").gameObject.SetActive(true);
        }
        else
        {
            //this.transform.Find("stun").gameObject.SetActive(false);
        }
    }
    public void ActivateDodge()
    {
        if (dodgeList.Count >= 1)
        {
            
        }
        else
        {
            dodgeRate = 0;
        }
    }
    public void RemoveDodge()
    {
        dodgeList.Clear();
    }

    public void SetHeal(int heal){
		HP += heal;
		if(HP>MAX_HP){
			HP=MAX_HP;
		}
		Debug.Log ("Get " + heal + " heal by player");
	}
	public void SetDamage(int damage){
		HP -= damage;
		//GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateDamagePopup (this.transform,damage);
		if (HP < 0) {
			HP = 0;
		}
		Debug.Log ("Get " + damage + " damage by monster");
	}
	public int BuffListCount(){
		return buffs.Count;

	}
}
