using UnityEngine;
using System.Collections;

public class TurnBasedCombatStateMachine : MonoBehaviour {

	GameObject card1;


	public enum BattleStates{
		START,
		PLAYERTURN,
		PLAYERCHOICE,
		ENEMYCHOICE,
		ENEMYTURN,
		LOSE,
		WIN
	}

	public BattleStates CurrentState
	{
		get{ return currentState; }
		set{ value = currentState; }
	}

	public BattleStates currentState;

	void Start () {
		currentState = BattleStates.START;
	}
	
	void Update () {
		//Debug.Log (CurrentState);
		switch (currentState) {
		case (BattleStates.START):
			//setup battle function
			break;
		case (BattleStates.PLAYERCHOICE):
			break;
		case (BattleStates.PLAYERTURN):
			break;
		case (BattleStates.ENEMYCHOICE):
			break;
		case (BattleStates.ENEMYTURN):
			break;
		case (BattleStates.LOSE):
			break;
		case (BattleStates.WIN):
			break;
		}
	
	}
	void OnGUI(){
		if(GUILayout.Button ("NEXT STATE")){
			if(currentState==BattleStates.START){
				currentState=BattleStates.PLAYERTURN;
			}else if( currentState==BattleStates.PLAYERTURN)
			{
				currentState=BattleStates.PLAYERCHOICE;
			}
			else if( currentState==BattleStates.PLAYERCHOICE)
			{
				currentState=BattleStates.ENEMYTURN;
			}
			else if( currentState==BattleStates.ENEMYTURN)
			{
				currentState=BattleStates.ENEMYCHOICE;
			}
			else if (currentState==BattleStates.ENEMYCHOICE)
			{
				currentState=BattleStates.LOSE;
			}
			else if (currentState==BattleStates.LOSE)
			{
				currentState=BattleStates.WIN;
			}
			else if (currentState==BattleStates.WIN)
			{
				currentState=BattleStates.START;
			}
			else if( currentState==BattleStates.WIN)
			{
				currentState=BattleStates.START;
			}
		}
	}
}
