using UnityEngine;
using System.Collections;

public class ChooseEnemy : MonoBehaviour {

	public GameObject selectable;
	public GameObject SelectedCard;


	public IEnumerator SelectEnemy(GameObject cardObject){
		Debug.Log ("SelectEnemy");
		HighlightEnemy (cardObject);
		yield return StartCoroutine (WaitForEnemySelect (cardObject));
		AttackEnemy (cardObject);
	}

	IEnumerator WaitForEnemySelect(GameObject cardObject){
		while (true) {

			if (Input.GetMouseButtonDown (0)) {
				Debug.Log (Input.mousePosition);
				Debug.Log (Camera.main.ScreenToWorldPoint(Input.mousePosition));
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
				RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
				if (hit.collider != null) {
					GameObject[] selectables = GameObject.FindGameObjectsWithTag ("selectable");
					foreach (GameObject selectable in selectables) {
						Destroy (selectable);
					}
					Debug.Log (hit.collider.gameObject);
					break;
				
				}else{
					Debug.Log ("Nothing");
				}
			}
			if (SelectedCard!=cardObject) {
				Debug.Log("changed");
				GameObject[] Monsters = GameObject.FindGameObjectsWithTag ("Monster");
				foreach (GameObject Monster in Monsters) {
					Destroy (Monster.transform.FindChild ("selectable(Clone)").gameObject);
				}
				break;
			}

			yield return null;
		}
	}

	void HighlightEnemy(GameObject cardObject){
		Debug.Log ("Hightlight");
		GameObject[] Monsters=GameObject.FindGameObjectsWithTag ("Monster");
		foreach (GameObject Monster in Monsters) {
			GameObject Selectable = (GameObject)Instantiate (selectable);
			Selectable.transform.parent = Monster.transform;
			Selectable.transform.position = Monster.transform.position;
		}
		}

	void AttackEnemy(GameObject cardObject){
	}
}
