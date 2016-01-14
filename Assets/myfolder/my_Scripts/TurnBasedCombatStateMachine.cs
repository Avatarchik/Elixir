using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class TurnBasedCombatStateMachine : MonoBehaviour {

	GameObject card1;
    public BattleStates currentState;
    private int turnCount = 2;
    public int dustCount = 0;

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


    public void incrementTurn()
    {
        turnCount++;
    }
    public void decrementTurn()
    {
        turnCount--;
    }
    public void resetTurn()
    {
        turnCount = 2;
    }
    public bool isTurnExhausted()
    {
        return (turnCount == 0);
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
                //Reduce enemy stun counter after EnemyTurn
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
                foreach (GameObject monster in monsters)
                {
                    monster.GetComponent<Monster>().ReduceStunTurn();
                    monster.GetComponent<Monster>().ActivateStun();
                    monster.GetComponent<Monster>().ReduceDotDamageTurn();
                }

                GameObject.Find("Player(Clone)").GetComponent<BaseCharacter>().ActivateBuff();
                GameObject.Find("Player(Clone)").GetComponent<BaseCharacter>().ReduceBuffTurn();
                GameObject.Find("Player(Clone)").GetComponent<BaseCharacter>().RemoveBuff();

                GameObject.Find ("Hands").GetComponent<HandSet>().CardSet();
                currentState = BattleStates.IDLE;
                break;
		    case (BattleStates.ENEMYCHOICE):
                Debug.Log("Enemy Turn Reached");

                //Functions that delete all cards in hand (temporary)
                foreach (var card in FindObjectOfType<HandSet>().cards)
                {
                    card.SetActive(false);
                }
                FindObjectOfType<HandSet>().skillTextPanel.SetActive(false);
                
                GameObject[] monsters2 = GameObject.FindGameObjectsWithTag("Monster");
                foreach (GameObject monster in monsters2)
                {
                    monster.GetComponent<Monster>().ActivateDotDamage();
                }
                /*
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
                */
                StartCoroutine(GameObject.Find ("MonsterManager").GetComponent<EnemyAI>().EnemyActChoice(GameObject.FindGameObjectsWithTag("Monster")));
                // currentState = BattleStates.PLAYERCHOICE;
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
        GUI.Label(new Rect(10, 40, 100, 20), "" + turnCount);//Display attack turn count
        GUI.Label(new Rect(10, 60, 100, 20), "빛가루: " + dustCount);
        if (GUILayout.Button ("NEXT STATE")){

			if(currentState==BattleStates.IDLE){
				currentState=BattleStates.ENEMYCHOICE;
			}
		}
	}
}
