using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EnumsAndClasses;

public class CardChoice : MonoBehaviour,IPointerDownHandler{
	public Sprite sprSelected;
	public Sprite sprUnselected;

	void Start(){
		GetComponent<Image> ().sprite = sprUnselected;
	}
	public void OnPointerDown(PointerEventData eventData){
        Debug.Log("OnPointerDown");
        GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode = AttackMode.Card;
        GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard = this.gameObject;
        Debug.Log("Card Activate: " + this.gameObject.GetComponent<InfoCard>().Card.Card_Name);
        StartCoroutine(cardActivated());
        Debug.Log("OnPointerDown Exit");
    }
	IEnumerator cardActivated(){
        GetComponent<Image> ().sprite = sprSelected;

        yield return StartCoroutine(GameObject.Find("Canvas").transform.FindChild("Hands").GetComponent<ChooseTarget>().SelectTarget());
        GetComponent<Image> ().sprite = sprUnselected;

        Debug.Log("Before Coroutine Stopped");
        //If Attack method is changed during the procedure, dismiss all actions
        if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Card ||
            GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard != this.gameObject)
        {
            Debug.Log("Coroutine Stopped");
        }
        else
        {
        //Increment counter
        //Check if all turns are exhausted
        //If exhausted, change the state of TurnBasedCombatStateMachine
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().incrementTurn();
   
        if (GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted())
        {
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().resetTurn();
        }
        Destroy(this.gameObject);// After using the card, destroy it from hand
        }

        

    }

}
