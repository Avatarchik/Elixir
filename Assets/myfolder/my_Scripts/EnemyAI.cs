using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class EnemyAI : MonoBehaviour {

	float EnemyBehaviourBeforeDelay = 1.0f;
	float EnemyBehaviourAfterDelay=1.0f;

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
                //UseAttack(monster);
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
		List<string> Condition1List=new List<string>();
		List<string> Condition2List=new List<string>();
		List<string> Condition3List=new List<string>();

		GetComponent<MonsterSkillLoad> ().SetConditionList (SkillName, Condition1List, Condition2List, Condition3List);



		if (IdentifyConditionList(Condition1List))
			return true;


		if (IdentifyConditionList(Condition2List))
			return true;


		if (IdentifyConditionList(Condition3List)) 
			return true;
		return false;
	
	}

     bool IdentifyConditionList(List<string> Conditionlist){
		if (Conditionlist [0] == "N/A"||Conditionlist[0]=="Always")
			return true;

		int falseCount = 0;

		foreach (string ConditionName in Conditionlist) {



			if (IdentifyCondition (ConditionName))
				falseCount++;


		}
		return (falseCount == 0);
	}



	bool IdentifyCondition(string ConditionName){
		Debug.Log (ConditionName);

		if (ConditionName=="N/A")
			return false;



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
