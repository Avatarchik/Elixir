using UnityEngine;
using System.Collections;

public class ChooseEnemy : MonoBehaviour {

	public GameObject selectable;
	public GameObject SelectedCard;
	GameObject selectedEnemy;


	public IEnumerator SelectEnemy(GameObject cardObject){
		Debug.Log ("SelectEnemy");
		selectedEnemy = null;
		HighlightEnemy (cardObject);
		yield return StartCoroutine (WaitForEnemySelect (cardObject));
		AttackEnemy (cardObject, selectedEnemy);

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
					selectedEnemy=hit.collider.gameObject;
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

	void AttackEnemy(GameObject cardObject,GameObject SelectedEnemy){
		if (cardObject.GetComponent<InfoCard> ().Card.Card_Target == 4) {

			GameObject[] Monsters = GameObject.FindGameObjectsWithTag ("Monster");

			if (cardObject.GetComponent<InfoCard> ().Card.Card_Attack_Damage > 0) {
				foreach (GameObject Monster in Monsters) {
					Monster.GetComponent<Monster> ().SetDamage ((int)cardObject.GetComponent<InfoCard> ().Card.Card_Attack_Damage);
				}
			} else if (cardObject.GetComponent<InfoCard> ().Card.Card_HP_Damage > 0) {

				foreach (GameObject Monster in Monsters) {
					if((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage)*Monster.GetComponent<Monster>().maxHp/100>=cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage){
						Monster.GetComponent<Monster> ().SetDamage ((int)cardObject.GetComponent<InfoCard> ().Card.Card_Max_Damage);
					}else{
						Monster.GetComponent<Monster> ().SetDamage ((int)cardObject.GetComponent<InfoCard> ().Card.Card_HP_Damage);
					}
				}
			}
		}else if (cardObject.GetComponent<InfoCard> ().Card.Card_Target == 2)
		{
			if(cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage>0){
				SelectedEnemy.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage);
			}else if(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage>0){
				if((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage)*SelectedEnemy.GetComponent<Monster>().maxHp/100>=cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage)
				{
					SelectedEnemy.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage);
				}else{
					SelectedEnemy.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage);
				}
			}
		}
}
}
