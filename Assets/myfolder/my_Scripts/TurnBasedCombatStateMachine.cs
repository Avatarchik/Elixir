using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    public MonsterPrefs monsterPrefs;
    public PlayerPrefs playerPrefs;
	public GameObject turnUI;

	GameObject card1;
    public BattleStates currentState;
    public int turnCount = 2;
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
		turnUI = GameObject.Find ("TurnUI");

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
		turnUI.transform.Find("RemainingTurn").GetComponent<Text>().text = "남은 공격기회: " + turnCount;
		turnUI.transform.Find("DustCount").GetComponent<Text>().text = "빛가루: " + dustCount;

		switch (currentState) {
		    case (BattleStates.START):
			    currentState=BattleStates.PLAYERCHOICE;
			    break;
		    case (BattleStates.PLAYERCHOICE):
                //Reduce player buff
                playerPrefs.player.ReduceDodgeTurn();
                playerPrefs.player.ReduceImmuneCriticalTargetTurn();
                playerPrefs.player.ReduceDebuffImmuneTurn();
                playerPrefs.player.ReduceGuardStateChangeTurn();
                playerPrefs.player.ReduceImmuneHeatTurn();
                playerPrefs.player.ReduceDamageResistanceTurn();
                playerPrefs.player.ReduceDotHealTurn();

                playerPrefs.player.ActivateDotHeal();

                //Activate player debuff
                playerPrefs.player.ActivateDotDamage();
                playerPrefs.player.ActivateActionLimit();

                //Reduce enemy debuff
                for(int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    monsterPrefs.monsterList[i].ReduceDotDamageTurn();
                    monsterPrefs.monsterList[i].ReduceStunTurn();
                    monsterPrefs.monsterList[i].ReduceBlindTurn();
                    monsterPrefs.monsterList[i].ReduceSilentTurn();
                }

                currentState = BattleStates.IDLE;
                GetComponent<PlayerTurn>().GetSkills();
                
                break;
		    case (BattleStates.ENEMYCHOICE):
                Debug.Log("Enemy Turn Reached");

                //Deactivate player UI
                GameObject skillPanel = GameObject.Find("SkillPanel");
                skillPanel.transform.Find("Skill1").GetComponent<Button>().interactable = false;
                skillPanel.transform.Find("Skill2").GetComponent<Button>().interactable = false;
                skillPanel.transform.Find("Skill3").GetComponent<Button>().interactable = false;

                GameObject.Find("Button").GetComponent<Button>().interactable = false;
                GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();

                //Reduce enemy buff
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    monsterPrefs.monsterList[i].ReduceShieldTurn();
                }

                //Activate enemy debuff
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    monsterPrefs.monsterList[i].ActivateDotDamage();
                }

                //Reduce Player debuff
                playerPrefs.player.ReduceDotDamageTurn();
                playerPrefs.player.ReduceActionLimitTurn();

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
}
