using UnityEngine;
using System.Collections;

public class TurnBasedCombatStateMachine : MonoBehaviour {

	GameObject card1;

	public enum BattleStates{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
        IDLE,
		LOSE,
		WIN
	}

	public BattleStates CurrentState
	{
		get{ return currentState; }
		set{ value = currentState; }
	}

	public BattleStates currentState;

    private int turnCount = 0;
    public void incrementTurn()
    {
        turnCount++;
    }
    public void resetTurn()
    {
        turnCount = 0;
    }
    public bool isTurnExhausted()
    {
        return (turnCount >= 2);
    }


	void Start () {
		currentState = BattleStates.START;
	}
	
	void Update () {
		switch (currentState) {
		    case (BattleStates.START):
                //Start
			    currentState=BattleStates.PLAYERCHOICE;
			    break;
		    case (BattleStates.PLAYERCHOICE):
                GameObject.Find ("Hands").GetComponent<HandSet>().CardSet();
                currentState = BattleStates.IDLE;
                break;
		    case (BattleStates.ENEMYCHOICE):
                Debug.Log("Enemy Turn Reached");
                //Functions that delete all cards in hand (temporary)
                GameObject chCount = GameObject.Find("Canvas/Hands");
                for(int i = 0; i < chCount.transform.childCount; i++)
                {
                    Destroy(chCount.transform.GetChild(i).gameObject);
                }
                //
                currentState = BattleStates.PLAYERCHOICE;
                break;
            case (BattleStates.IDLE):
                break;
		    case (BattleStates.LOSE):
			    break;
		    case (BattleStates.WIN):
			    break;
		}
	}
	void OnGUI(){
        GUI.Label(new Rect(10, 40, 100, 20), "" + (2 - turnCount));//Display attack turn count
        if (GUILayout.Button ("NEXT STATE")){

			if(currentState==BattleStates.START){
				currentState=BattleStates.PLAYERCHOICE;
			}else if( currentState==BattleStates.PLAYERCHOICE)
			{
				currentState=BattleStates.ENEMYCHOICE;
			}
			else if( currentState==BattleStates.ENEMYCHOICE)
			{
				currentState=BattleStates.LOSE;
			}
			else if( currentState==BattleStates.LOSE)
			{
				currentState=BattleStates.WIN;
			}
			else if (currentState==BattleStates.WIN)
			{
				currentState=BattleStates.START;
			}
		}
	}
}
