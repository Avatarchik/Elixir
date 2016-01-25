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
			    yield return new WaitForSeconds(EnemyBehaviourAfterDelay);
            }
			
		}
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE;
        yield break;
    }
	/*
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

	}*/

	void SelectAct(GameObject monster){
		baseMonster Monsterdata = monster.GetComponent<InfoMonster> ().MonsterInfo;
		double Skill1Rate = Monsterdata.Mon_Skill1_Rate;//스킬1 확률
		double Skill2Rate = Monsterdata.Mon_Skill2_Rate;//스킬2 확률 
		string koreanSkillName;//한글명
		//Targetlist = new List<GameObject> ();
		if (Random.Range (1, 100) <= Skill1Rate && SkillCondition (monster, Monsterdata.Mon_Skill1_Name, 1)) {

			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill1_Name).MonsterSkillName;
			// 조건들을 만족하면 스킬 실행
			GetComponent<EnemySkill>().UseSkill(monster,Monsterdata.Mon_Skill1_Name,koreanSkillName);


		} else if (Random.Range (1, 100) <= Skill2Rate && SkillCondition (monster,Monsterdata.Mon_Skill2_Name, 2)) {
			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill2_Name).MonsterSkillName;
			// 조건들을 만족하면 스킬 실행
			GetComponent<EnemySkill>().UseSkill(monster,Monsterdata.Mon_Skill2_Name,koreanSkillName);

		} else {
			// 스킬조건을 만족하지못하면 평타 
			GetComponent<EnemySkill>().UseAttack (monster);
		}
	}

	//not implemented
	bool SkillCondition(GameObject monster, string SkillName, int SkillNum)
	{
		List<string> Condition1List=new List<string>();
		List<string> Condition2List=new List<string>();
		List<string> Condition3List=new List<string>();

		GetComponent<MonsterSkillLoad> ().SetConditionList (SkillName, Condition1List, Condition2List, Condition3List);

		if (IdentifyConditionList(monster,Condition1List))
			return true;


		if (IdentifyConditionList(monster,Condition2List))
			return true;


		if (IdentifyConditionList(monster,Condition3List)) 
			return true;
		return false;
	
	}

     bool IdentifyConditionList(GameObject monster, List<string> Conditionlist){
		if (Conditionlist [0] == "N/A"||Conditionlist[0]=="Always")
			return true;

		int falseCount = 0;

		foreach (string ConditionName in Conditionlist) {

			if (IdentifyCondition (monster, ConditionName))
				falseCount++;
		}
		return (falseCount == 0);
	}

	bool IdentifyCondition(GameObject monster, string ConditionName){
		Debug.Log (ConditionName);

		if (ConditionName=="N/A")
			return false;

		if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState != "N/A") {
			//TargetState 확인
			string targetState = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState;

			ChemicalStates currentState = GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().currentChemicalState;

			switch (targetState) {

			case "Solid":
				return(currentState == ChemicalStates.SOLID);
			case "Liquid":
				return(currentState == ChemicalStates.LIQUID);
			case "Gas":
				return(currentState == ChemicalStates.GAS);
			case "NoneSolid":
				return(currentState != ChemicalStates.SOLID);
			case "NoneLiquid":
				return(currentState != ChemicalStates.LIQUID);
			case "NoneGas":
				return(currentState != ChemicalStates.GAS);

			}
		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpBelowN > 0) {
			//HpBelowN 확인, N보다 플레이어의 Hp가 낮으면 true
			float TargetHp = GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().HP;
			float TargetMaxHp = GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().MAX_HP;
			int percenthp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpBelowN;
			return (TargetHp <= TargetMaxHp * percenthp / 100);
		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpMoreN > 0) {
			//HpMoreN 확인, N보다 플레이어의 Hp가 높으면 true
			float TargetHp = GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().HP;
			float TargetMaxHp = GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().MAX_HP;
			int percenthp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpMoreN;
			return (TargetHp >= TargetMaxHp * percenthp / 100);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).SelfHpBelowN > 0) {
			//SelfHpBelowN 확인, N보다 자신의 Hp가 낮으면 true
			float Hp=monster.GetComponent<Monster>().hp;
			float ConditionHp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).SelfHpBelowN;
			return (Hp<ConditionHp);

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
			//RandomRate 확인, 랜덤 확률 
			return(Random.Range (1,100)<=GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).RandomRate);

		}
		return false;


	}
	/*아직 만드는 중 
	List<GameObject> SelectTarget(string SkillName){
		
		//make a list of target 
		List<GameObject> list=new List<GameObject>();
		switch(GetComponent<MonsterSkillLoad>().Find_MonsterSkillID (SkillName).Target){
		case "All":
			list.Add (GameObject.Find ("Player"));
			GameObject[] monsters=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in monsters){
				list.Add (monster);
			}
			break;
		case "Player":
			list.Add (GameObject.Find ("Player"));
			break;
		case "Monster":
			GameObject[] Targetmonster=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in Targetmonster){
				list.Add (monster);
			}
			break;
		}
		return list;
	}
*/


}
