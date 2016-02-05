using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

[System.Serializable]
public class BaseCharacter{

	private string CharacterName;

	//stats
	public float MAX_HP=200.0f;
	public float HP;
    public ChemicalStates criticalTarget;
    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;
    public int dodgeRate;

    public List<Buff> buffs = new List<Buff>();

  //  void Start(){
		//HP = 200;
  //      //Temporary inputs
  //      this.criticalTarget = ChemicalStates.LIQUID;
  //      this.currentChemicalState = ChemicalStates.LIQUID;
  //      this.currentChemicalStateValue = 1;
  //      this.solidStateValue = 1;
  //      this.liquidStateValue = 2;
  //      this.gasStateValue = 3;
  //      this.dodgeRate = 0;

  //  }

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

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
    }
    public void ReduceBuffTurn()
    {
        foreach (Buff buff in buffs)
        {
            buff.RemainTurn--;
            Debug.Log("Player Buff turn remaining: " + buff.RemainTurn);
        }
    }
    public void ActivateBuff()
    {
        bool isExistDodgeBuff = false;
        foreach (Buff buff in buffs)
        {
            if (buff.GetBuffname().Equals(BuffName.Dodge))//Activate Dot Damage
            {
                Debug.Log("This Buff is DodgeRateIncrease");
                isExistDodgeBuff = true;
            }
        }
        if (isExistDodgeBuff)
        {
            dodgeRate = 30;
            Debug.Log("Player's Dodge Rate: " + dodgeRate);
        }
        else
        {
            dodgeRate = 0;
            Debug.Log("Player's Dodge Rate: " + dodgeRate);
        }
    }
    public void RemoveBuff()
    {
        List<Buff> buffsToDestroy = new List<Buff>();
        foreach (Buff buff in buffs)
        {
            if (buff.RemainTurn == 0)
            {
                buffsToDestroy.Add(buff);
            }
        }
        foreach (Buff debuffToDestroy in buffsToDestroy)
        {
            buffs.Remove(debuffToDestroy);
        }
        buffsToDestroy.Clear();
    }


 //   void dead(){
	//	if (HP <= 0) {
	//		gameObject.SetActive (false);
	//	}
	//}

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
