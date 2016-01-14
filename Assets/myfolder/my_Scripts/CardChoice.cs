using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EnumsAndClasses;

public class CardChoice : MonoBehaviour{//, IPointerDownHandler{
	public Sprite sprSelected;
	public Sprite sprUnselected;
    
    ChoosingManager choosingManager;
    TurnBasedCombatStateMachine turnBasedCombatStateMachine;

	void Start(){
        choosingManager = GameObject.Find("GameManager").GetComponent<ChoosingManager>();
        turnBasedCombatStateMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();
		GetComponent<Image> ().sprite = sprUnselected;
	}
	// public void OnPointerDown(PointerEventData eventData){
    //     Debug.Log("OnPointerDown");
    //     choosingManager.AttackMode = AttackMode.Card;
    //     choosingManager.SelectedCard = this.gameObject;
    //     Debug.Log("Card Activate: " + this.gameObject.GetComponent<InfoCard>().Card.Card_Name);
    //     StartCoroutine(cardActivated());
    //     Debug.Log("OnPointerDown Exit");
    // }
    
    public void ClickedCard(int indexInHand)
    {        
        choosingManager.AttackMode = AttackMode.Card;
        choosingManager.SelectedCard = this.gameObject;
        Debug.Log("Card Activate: " + transform.FindChild("CardName").GetComponent<Text> ().text);
        StartCoroutine(cardActivated(indexInHand));
    }
    
    IEnumerator cardActivated(int indexInHand)
    {
        GameObject card = FindObjectOfType<HandSet>().cards[indexInHand-1];
        
        // FIXME : color changed.
        yield return StartCoroutine(GameObject.Find("Canvas").transform.FindChild("Hands").GetComponent<ChooseTarget>().SelectTarget());
        // FIXME : color returned.
        
        Debug.Log("Before Coroutine Stopped");
        //If Attack method is changed during the procedure, dismiss all actions
        if (choosingManager.AttackMode != AttackMode.Card ||
            choosingManager.SelectedCard != this.gameObject)
        {
            Debug.Log("Coroutine Stopped");
        }
        else
        {
            //Increment counter
            //Check if all turns are exhausted
            //If exhausted, change the state of TurnBasedCombatStateMachine
            turnBasedCombatStateMachine.decrementTurn();
   
            if (turnBasedCombatStateMachine.isTurnExhausted())
            {
                turnBasedCombatStateMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
                turnBasedCombatStateMachine.resetTurn();
            }
            
            card.GetComponent<Button>().interactable = false;
        }
        yield return null;
    }
}
