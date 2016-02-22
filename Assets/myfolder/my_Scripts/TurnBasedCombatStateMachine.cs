using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    public MonsterPrefs monsterPrefs;
    public PlayerPrefs playerPrefs;

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
        monsterPrefs = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>();
        playerPrefs = GetComponent<PlayerPrefs>();

        currentState = BattleStates.START;
        //Load Databases
        GetComponent<Loader>().Load();
        GetComponent<SkillLoader>().Load();
        //Initialize Player and Monster
        GetComponent<PlayerPrefs>().Initialize(); //Set Player's initial stats
        //GetComponent<AllyManager>().GeneratePlayer(); //Summon player into field
        GameObject.Find("MonsterManager").GetComponent<MonsterSkillLoad>().Initialize();
        GameObject.Find("MonsterManager").GetComponent<MonsterSkillConditionLoad>().Initialize();
        GameObject.Find("MonsterManager").GetComponent<MonsterLoad>().Initialize();
        GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>().Initialize();
        //GameObject.Find("MonsterManager").GetComponent<MonsterManager>().Initialize(); //Summon monsters into field
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
                foreach(Monster monster in monsterPrefs.monsterList)
                {
                    monster.ReduceStunTurn();
                    monster.ActivateStun();
                    monster.ReduceDotDamageTurn();
                }

                playerPrefs.player.ActivateDodge();
                playerPrefs.player.ReduceDodgeTurn();
                playerPrefs.player.RemoveDodge();

                currentState = BattleStates.IDLE;
                GetComponent<PlayerTurn>().GetSkills();
                
                break;
		    case (BattleStates.ENEMYCHOICE):
                Debug.Log("Enemy Turn Reached");

                GameObject skillPanel = GameObject.Find("SkillPanel");
                for (int i = 0; i < skillPanel.transform.childCount; i++)
                {
                    skillPanel.transform.GetChild(i).GetComponent<Button>().interactable = false;
                }
                GameObject.Find("Button").GetComponent<Button>().interactable = false;
                GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();

                foreach (Monster monster in monsterPrefs.monsterList)
                {
                    monster.ActivateDotDamage();
                }

                //StartCoroutine(GameObject.Find ("MonsterManager").GetComponent<EnemyAI>().EnemyActChoice(GameObject.FindGameObjectsWithTag("Monster")));
                currentState = BattleStates.IDLE;
                StartCoroutine(GameObject.Find("MonsterManager").GetComponent<EnemyAI>().EnemyActChoice());
                
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
