using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	private string CharacterName;

	//stats
	public double MAX_HP=100.0f;
	public double HP;

	void Start(){
		HP = MAX_HP;
	}

	void dead(){
		if (HP <= 0) {
			gameObject.SetActive (false);
		}
	}
}
