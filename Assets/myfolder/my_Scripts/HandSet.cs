using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HandSet : MonoBehaviour {
	private List<int> number;
	private int temp;
	public GameObject cardPrefab;
	void Start(){

	}
	void Update(){
		Debug.Log (GetComponent<TurnBasedCombatStateMachine> ().CurrentState);
		if (GetComponent<TurnBasedCombatStateMachine> ().CurrentState == TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE) {
			GetComponent<TurnBasedCombatStateMachine> ().currentState=TurnBasedCombatStateMachine.BattleStates.PLAYERTURN;
			CardSelect ();
			CardDraw ();
		}
	}
	void CardSelect(){
		Debug.Log ("CardSelect");
		int count;
		number = new List<int> ();
		for(int i = 0; i<=3; i++)
		{
			number.Add(i);
		}
		for (int i=0; i<=3; i++) {
			do {
				count=0;
				temp=Random.Range (0, 19);
				for(int x=0;x<i;x++){
					if(number[x]==temp)
						count++;
				}
			} while(count!=0);
			number[i]=temp;
		}
		
	}
	void AddCards(int cardIndex)
	{
		Debug.Log ("AddCards");
		GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
		
		cardCopy.transform.SetParent(this.transform);
		cardCopy.GetComponent<InfoCard> ().Card = GetComponent<CardLoad> ().cardDeck [cardIndex];
		cardCopy.transform.FindChild ("CardName").GetComponent<Text> ().text = GetComponent<CardLoad> ().cardDeck [cardIndex].Card_ExtName;
		cardCopy.transform.FindChild ("CardStatement").GetComponent<Text> ().text = GetComponent<CardLoad> ().cardDeck [cardIndex].Card_Description;
	}
	void CardDraw(){
		for (int i=0; i<=3; i++) {
			AddCards (number [i]);
		}
	}

}
