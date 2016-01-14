using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class EnemyAI : MonoBehaviour {

	float EnemyBehaviourBeforeDelay = 3.0f;
	float EnemyBehaviourAfterDelay=3.0f;

	public IEnumerator EnemyActChoice(GameObject[] monsters)
	{
        Debug.Log("Enemy count: " + monsters.Length);
		GameObject.Find ("GameManager").GetComponent<TurnBasedCombatStateMachine> ().currentState=TurnBasedCombatStateMachine.BattleStates.IDLE;
		Debug.Log ("Coroutine is started");
		foreach (GameObject monster in monsters)
		{
			Debug.Log ("Monster Action");
            if(monster.GetComponent<Monster>().stunned == true)
            {
                Debug.Log("Monster is stunned. Does not attack.");
            }
            else
            {
                yield return new WaitForSeconds(EnemyBehaviourBeforeDelay);
			    SelectAct(monster);
			    yield return new WaitForSeconds(EnemyBehaviourAfterDelay);
            }
			
		}
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE;
        yield break;
    }

	void UseSkill(GameObject monster,string SkillName, string KoreanSkillName){
		Debug.Log (monster + " use " + SkillName);
		GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateMonsterSkillPopup (monster.transform,KoreanSkillName);
	}
	void UseAttack(GameObject monster){
		Animator animator=monster.GetComponent<Animator>();
		if (GameObject.Find ("Player(Clone)") != null) {
			Debug.Log ("Monster Attack");
			GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
			animator.SetTrigger ("Attack");
		}

	}
	void SelectAct(GameObject monster){
		baseMonster Monsterdata = monster.GetComponent<InfoMonster> ().MonsterInfo;
		double Skill1Rate = Monsterdata.Mon_Skill1_Rate;
		double Skill2Rate = Monsterdata.Mon_Skill2_Rate;
		string koreanSkillName;

		if (Random.Range (1, 100) <= Skill1Rate && SkillCondition (Monsterdata.Mon_Skill1_Name, 1)) {

			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill1_Name).MonsterSkillName;
			UseSkill(monster,Monsterdata.Mon_Skill1_Name,koreanSkillName);


		} else if (Random.Range (1, 100) <= Skill2Rate && SkillCondition (Monsterdata.Mon_Skill2_Name, 2)) {
			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill2_Name).MonsterSkillName;
			UseSkill(monster,Monsterdata.Mon_Skill2_Name,koreanSkillName);

		} else {

			UseAttack (monster);
		}
	}

	//not implemented
	bool SkillCondition(string SkillName, int SkillNum)
	{
		string ConditionName1_1;
		string ConditionName1_2;
		string ConditionName2_1;
		string ConditionName2_2;
		string ConditionName3_1;
		string ConditionName3_2;

		ConditionName1_1 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition1_1;
		ConditionName1_2 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition1_2;

		if (IdentifyCondition (ConditionName1_1) && IdentifyCondition (ConditionName1_2))
			return true;

		ConditionName2_1 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition2_1;
		ConditionName2_2 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition2_2;

		Debug.Log (!((ConditionName2_1 == "N/A") && (ConditionName2_2 == "N/A")));

		if ((!((ConditionName2_1=="N/A")&&(ConditionName2_2=="N/A")))&&IdentifyCondition (ConditionName2_1) && IdentifyCondition (ConditionName2_2))
			return true;

		ConditionName3_1 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition3_1;
		ConditionName3_2 = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID(SkillName).UseCondition3_2;

		if ((!((ConditionName3_1=="N/A")&&(ConditionName3_2=="N/A")))&&IdentifyCondition (ConditionName3_1) && IdentifyCondition (ConditionName3_2)) 
			return true;
		return false;
	
	}

	bool IdentifyCondition(string ConditionName){
		Debug.Log (ConditionName);

		if (ConditionName == "Always"||ConditionName=="N/A")
			return true;



		if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState != "N/A")
		{
			string targetState = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState;

			ChemicalStates currentState=GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter>().currentChemicalState;

			switch(targetState){

			case "Solid":
				return(currentState==ChemicalStates.SOLID);
			case "Liquid":
				return(currentState==ChemicalStates.LIQUID);
			case "Gas":
				return(currentState==ChemicalStates.GAS);
			case "NoneSolid":
				return(currentState!=ChemicalStates.SOLID);
			case "NoneLiquid":
				return(currentState!=ChemicalStates.LIQUID);
			case "NoneGas":
				return(currentState!=ChemicalStates.GAS);
			}
		} 
		else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).HpBelowN >0 ) 
		{
			float TargetHp=GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().HP;
			float TargetMaxHp=GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter>().MAX_HP;
			int percenthp=GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).HpBelowN;
			return (TargetHp<=TargetMaxHp*percenthp/100);
		} 
		else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).HpMoreN >0)
		{
			float TargetHp=GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().HP;
			float TargetMaxHp=GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter>().MAX_HP;
			int percenthp=GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).HpMoreN;
			return (TargetHp>=TargetMaxHp*percenthp/100);

		} 
		else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).Actionlimit != "N/A") 
		{
			//if player's Action is limited return true
			string actionlimit = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).Actionlimit;
			if(actionlimit=="N"){

			}
			else if(actionlimit=="Y"){


			}




		} 
		else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetNumber >0)
		{
			//not implemented

		} 
		else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).RandomRate >0)
		{
			return(Random.Range (1,100)<=GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).RandomRate);

		}
		return false;


	}
}
