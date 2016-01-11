using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class BaseCharacter : MonoBehaviour {

	private string CharacterName;

	//stats
	public float MAX_HP=100.0f;
	public float HP;
    private ChemicalStates chemicalState;
    private int chemicalStateValue;

    public enum ChemicalStates
    {
        LIQUID,
        GAS,
        SOLID
    }



	void Start(){
		HP = 50;
        //Temporary inputs
        chemicalState = ChemicalStates.SOLID;
        chemicalStateValue = 0;
    }

    public ChemicalStates ChemicalState
    {
        get { return chemicalState; }
        set { chemicalState = value; }
    }
    public int ChemicalStateValue
    {
        get { return chemicalStateValue; }
        set { chemicalStateValue = value; }
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
