using UnityEngine;
using System.Collections;

public class ChooseAlly : MonoBehaviour {

	public GameObject selectable;
	GameObject selectedAlly;
	
	
	public IEnumerator SelectAlly(GameObject cardObject){
		Debug.Log ("SelectEnemy");
		selectedAlly = null;
		HighlightAlly ();
		yield return StartCoroutine (WaitForAllySelect(cardObject));
		if(selectedAlly!=null)
			HealEnemy (cardObject, selectedAlly);
		
	}
	
	IEnumerator WaitForAllySelect(GameObject cardObject){
		while (true) {

			if (Input.GetMouseButtonDown (0)) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
				if (hit.collider!=null&&hit.collider.gameObject.tag == "Ally") {
					GameObject[] selectables = GameObject.FindGameObjectsWithTag ("selectable");
					selectedAlly=hit.collider.gameObject;
					foreach (GameObject selectable in selectables) {
						Destroy (selectable);
					}
					Debug.Log (hit.collider.gameObject);
					break;
					
				}else{
					Debug.Log ("Nothing");
				}
			}
			if (GetComponent<ChoosingManager>().SelectedCard!=cardObject) {
				Debug.Log("changed");
				GameObject[] Allies = GameObject.FindGameObjectsWithTag ("Ally");
				foreach (GameObject Ally in Allies) {
					Destroy (Ally.transform.FindChild ("selectable(Clone)").gameObject);
				}
				break;
			}
			
			yield return null;
		}
	}
	
	void HighlightAlly(){
		Debug.Log ("Hightlight");
		GameObject[] Allies=GameObject.FindGameObjectsWithTag ("Ally");
		foreach (GameObject Ally in Allies) {
			GameObject Selectable = (GameObject)Instantiate (selectable);
			Selectable.transform.parent = Ally.transform;
			Selectable.transform.position = Ally.transform.position;
			Selectable.transform.localScale=new Vector3(1.0f,1.0f);
		}
	}
	
	void HealEnemy(GameObject cardObject,GameObject SelectedAlly){
		SelectedAlly.GetComponent<BaseCharacter> ().SetHeal ((int)cardObject.GetComponent<InfoCard> ().Card.Card_Heal);
		GetComponent<ChoosingManager>().SelectedCard = null;
	}
}
