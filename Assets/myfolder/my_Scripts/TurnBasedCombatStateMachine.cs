using UnityEngine;
using System.Collections;
using EnumsAndClasses;

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
		//If there is no monsters in the field, Player wins
        if(GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            currentState = BattleStates.WIN;
        }
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

                //Inflict Debuff(dot damage) in the beginning of Enemy turn
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
                foreach(GameObject monster in monsters)
                {
                    //Check Dot Damage
                    Debug.Log("ReduceDebuffTurn");
                    monster.GetComponent<Monster>().ActivateDebuff();
                    monster.GetComponent<Monster>().ReduceDebuffTurn();//ReduceTurn after inflicting Dot Damage
                    monster.GetComponent<Monster>().RemoveDebuff();
                }

			    StartCoroutine(GameObject.Find ("MonsterManager").GetComponent<EnemyAI>().EnemyActChoice(GameObject.Find("MonsterManager").GetComponent<MonsterManager>().Monsters));
                //Functions that delete all cards in hand (temporary)
                GameObject chCount = GameObject.Find("Canvas").transform.FindChild("Hands").gameObject;
                for(int i = 0; i < chCount.transform.childCount; i++)
                {
                    Destroy(chCount.transform.GetChild(i).gameObject);
                }
                //currentState = BattleStates.PLAYERCHOICE;
                break;
            case (BattleStates.IDLE):
                break;
		    case (BattleStates.LOSE):
			    break;
		    case (BattleStates.WIN):
                Debug.Log("Battle Won!");
                break;
		}
	}
	void OnGUI(){
        GUI.Label(new Rect(10, 40, 100, 20), "" + (2 - turnCount));//Display attack turn count
        if (GUILayout.Button ("NEXT STATE")){

			if(currentState==BattleStates.IDLE){
				currentState=BattleStates.ENEMYCHOICE;
			}
		}
	}
}
