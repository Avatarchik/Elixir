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
        Debug.Log(transform.parent.GetComponent<ChoosingManager>().SelectedCard);
        //if there's already instance in the ChoosingManager, just change the SelectedCard instead of activating a new coroutine
        //Make it a singleton (why use ChooseEnemy and ChooseAlly in a separate manner?)
        if(transform.parent.GetComponent<ChoosingManager>().SelectedCard == null)
        {
            Debug.Log("Coroutine Activated");
            StartCoroutine(cardActivated());
        }else if(transform.parent.GetComponent<ChoosingManager>().SelectedCard != this.gameObject)
        {
            Debug.Log("Coroutine not Activated");
            transform.parent.GetComponent<ChoosingManager>().SelectedCard = this.gameObject;
        }


        //if (transform.parent.GetComponent<ChoosingManager>().SelectedCard != this.gameObject)
		//StartCoroutine (cardActivated());
		}
	IEnumerator cardActivated(){
		transform.parent.GetComponent<ChoosingManager> ().SelectedCard = this.gameObject;
        GetComponent<Image> ().sprite = sprSelected;
<<<<<<< HEAD

        yield return StartCoroutine(GameObject.Find("Canvas").transform.FindChild("Hands").GetComponent<ChooseTarget>().SelectTarget());

        GetComponent<Image> ().sprite = sprUnselected;
=======
		switch (GetComponent<ChooseEnemy> ().tar) {
		case 1:
			yield return StartCoroutine (GameObject.Find ("Canvas").transform.FindChild ("Hands").GetComponent<ChooseAlly> ().SelectAlly(this.gameObject));
			break;
		case 2:
			yield return StartCoroutine (GameObject.Find ("Canvas").transform.FindChild ("Hands").GetComponent<ChooseEnemy> ().SelectEnemy ());
			break;
		case 3:
			break;
		case 4:
			yield return StartCoroutine (GameObject.Find ("Canvas").transform.FindChild ("Hands").GetComponent<ChooseEnemy> ().SelectEnemy ());
			break;
		case 5:
			break;
		default:
			break;
		}
		GetComponent<Image> ().sprite = sprUnselected;
>>>>>>> origin/master
        //Increment counter
        //Check if all turns are exhausted
        //If exhausted, change the state of TurnBasedCombatStateMachine
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().incrementTurn();
        Debug.Log(GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted());
        if (GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted())
        {
            Debug.Log("Turn Exhausted");
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            Debug.Log(GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().CurrentState);
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().resetTurn();
        }
        

    }

}
