using UnityEngine;
using System.Collections;

public class ChooseEnemy : MonoBehaviour {

	public GameObject selectable;
	public int SelectedCardIndex;


	public IEnumerator SelectEnemy(GameObject cardObject){
		Debug.Log ("SelectEnemy");
		HighlightEnemy (cardObject);
		yield return StartCoroutine (WaitForEnemySelect (cardObject));
		AttackEnemy (cardObject);
	}
	IEnumerator WaitForEnemySelect(GameObject cardObject){


		while (true) {
			Debug.Log (SelectedCardIndex);
			Debug.Log (cardObject.GetComponent<InfoCard>().Card.Card_ID);

			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
				if (hit.collider != null) {
					GameObject[] selectables = GameObject.FindGameObjectsWithTag ("selectable");
					foreach (GameObject selectable in selectables) {
						Destroy (selectable);
					}
					Debug.Log (hit.collider.gameObject);
					break;
				
				} 
			}
			if (SelectedCardIndex!=cardObject.GetComponent<InfoCard>().Card.Card_ID) {
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
		Debug.Log ("제발");
	}
}
