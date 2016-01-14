using UnityEngine;
using System.Collections;

public class MonsterSkillPopup : MonoBehaviour {

	public Object myGameObjectOrComponent;
	public float timer;
	int damagepopupHash;
	
	void Start(){
		// Default is the gameObject
		if (myGameObjectOrComponent == null)
			myGameObjectOrComponent = gameObject;
		
		// Destroy works with GameObjects and Components
		Destroy (myGameObjectOrComponent, timer);
	}
}
