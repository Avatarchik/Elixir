using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardChoice : MonoBehaviour,IPointerDownHandler{
	public Sprite sprSelected;
	public Sprite sprUnselected;
	void Start(){
		GetComponent<Image> ().sprite = sprUnselected;
	}
	public void OnPointerDown(PointerEventData eventData){

		Debug.Log (GetComponent<InfoCard>().Card.Card_ExtName);
		transform.parent.GetComponent<ChooseEnemy> ().SelectedCard = this.gameObject;
		StartCoroutine (cardActivated());



	}
	IEnumerator cardActivated(){
		GetComponent<Image> ().sprite = sprSelected;
		switch (GetComponent<InfoCard> ().Card.Card_Target) {
		case 1:
			break;
		case 2:
			yield return StartCoroutine (GameObject.Find ("Canvas").transform.FindChild ("Hands").GetComponent<ChooseEnemy> ().SelectEnemy (this.gameObject));
			break;
		case 3:
			break;
		case 4:
			yield return StartCoroutine (GameObject.Find ("Canvas").transform.FindChild ("Hands").GetComponent<ChooseEnemy> ().SelectEnemy (this.gameObject));
			break;
		case 5:
			break;
		default:
			break;
		}

		GetComponent<Image> ().sprite = sprUnselected;
	}

}
