using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour {

	public GameObject damagePrefab;
	public int sortinglayerID;

	void Start(){
		// sortinglayerID=SortingLayer.GetLayerValueFromName("Default");
	}
	public void CreateDamagePopup(Transform damageTransform, int damage){
		GameObject damageGameObject = (GameObject)Instantiate(damagePrefab,
		                                                      damageTransform.position,
		                                                      damageTransform.rotation);
		damageGameObject.transform.SetParent(damageTransform);
		Renderer renderer = damagePrefab.GetComponent<Renderer> ();
		// renderer.sortingLayerID = sortinglayerID;
		damageGameObject.GetComponentInChildren<TextMesh>().text = damage.ToString();
	}

}
