using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	private string CharacterName;

	//stats
	public double MAX_HP=100.0f;
	public double HP;

	void Start(){
		HP = 50;
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
		if (HP < 0) {
			HP = 0;
		}
		Debug.Log ("Get " + damage + " damage by monster");
	}
}
