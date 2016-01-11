using UnityEngine;
using System.Collections;

public class SetLifeSpawn : MonoBehaviour {

	public Object myGameObjectOrComponent;
	public float timer;
	Animator animator;
	int popup;
	
	void Start(){

		animator = GetComponent<Animator> ();
		popup = Animator.StringToHash ("DamagePopup");
		animator.Play (popup);
		// Default is the gameObject
		if (myGameObjectOrComponent == null)
			myGameObjectOrComponent = gameObject;
		
		// Destroy works with GameObjects and Components
		Destroy (myGameObjectOrComponent, timer);
	}	
}
