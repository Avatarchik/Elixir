using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour {

	public Object myGameObjectOrComponent;
	public float timer;
	Animator animator;
	int damagepopupHash;
	
	void Start(){		
		animator = GetComponent<Animator> ();
		damagepopupHash = Animator.StringToHash ("DamagePopup");
		animator.Play (damagepopupHash);
		// Default is the gameObject
		if (myGameObjectOrComponent == null)
			myGameObjectOrComponent = gameObject;
		
		// Destroy works with GameObjects and Components
		Destroy (myGameObjectOrComponent, timer);
	}
}
