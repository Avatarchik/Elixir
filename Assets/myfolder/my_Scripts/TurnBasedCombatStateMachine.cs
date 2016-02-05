﻿using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using UnityEngine.UI;

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
        //Load Databases
        GetComponent<Loader>().Load();
        GetComponent<CardLoad>().Initialize();
        //Initialize Player and Monster
        GetComponent<PlayerPrefs>().Initialize(); //Set Player's initial stats
        GetComponent<AllyManager>().GeneratePlayer(); //Summon player into field
        GameObject.Find("MonsterManager").GetComponent<MonsterManager>().Initialize(); //Summon monsters into field
    }
	
	void Update () {
		//If there is no monsters in the field, Player wins
        //if(GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        //{
        //    currentState = BattleStates.WIN;
        //}
		switch (currentState) {
		    case (BattleStates.START):

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

                GetComponent<PlayerPrefs>().player.ActivateBuff();
                GetComponent<PlayerPrefs>().player.ReduceBuffTurn();
                GetComponent<PlayerPrefs>().player.RemoveBuff();

                GetComponent<PlayerTurn>().GetSkills();
                currentState = BattleStates.IDLE;
                break;
		    case (BattleStates.ENEMYCHOICE):
                Debug.Log("Enemy Turn Reached");

                GameObject.Find("SkillPanel").transform.GetChild(0).GetComponent<Button>().interactable = false;
                GameObject.Find("SkillPanel").transform.GetChild(1).GetComponent<Button>().interactable = false;
                GameObject.Find("SkillPanel").transform.GetChild(2).GetComponent<Button>().interactable = false;


                GameObject[] monsters2 = GameObject.FindGameObjectsWithTag("Monster");
                foreach (GameObject monster in monsters2)
                {
                    monster.GetComponent<Monster>().ActivateDotDamage();
                }
                
                StartCoroutine(GameObject.Find ("MonsterManager").GetComponent<EnemyAI>().EnemyActChoice(GameObject.FindGameObjectsWithTag("Monster")));
                 currentState = BattleStates.PLAYERCHOICE;
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
                resetTurn();
			}
		}
	}
}
