using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class BaseCharacter : MonoBehaviour {

	private string CharacterName;

	//stats
	public float MAX_HP=100.0f;
	public float HP;
    public ChemicalStates criticalTarget;
    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;

	void Start(){
		HP = 50;
        //Temporary inputs
        this.criticalTarget = ChemicalStates.LIQUID;
        this.currentChemicalState = ChemicalStates.LIQUID;
        this.currentChemicalStateValue = 1;
        this.solidStateValue = 1;
        this.liquidStateValue = 2;
        this.gasStateValue = 3;
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

    void dead(){
		if (HP <= 0) {
			gameObject.SetActive (false);
		}
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
		GameObject.Find ("GameManager").GetComponent<DamagePopup>().CreateDamagePopup (this.transform,damage);
		if (HP < 0) {
			HP = 0;
		}
		Debug.Log ("Get " + damage + " damage by monster");
	}
}
