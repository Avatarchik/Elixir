using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class EnemyAI : MonoBehaviour {

	float EnemyBehaviourBeforeDelay = 1.0f;
	float EnemyBehaviourAfterDelay=1.0f;
	public List<GameObject> TargetList;

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

	void SelectAct(GameObject monster){
		baseMonster Monsterdata = monster.GetComponent<InfoMonster> ().MonsterInfo;
		double Skill1Rate = Monsterdata.Mon_Skill1_Rate;//스킬1 확률
		double Skill2Rate = Monsterdata.Mon_Skill2_Rate;//스킬2 확률 
		string koreanSkillName;//한글명

		if (Random.Range (1, 100) <= Skill1Rate && SkillCondition (monster, Monsterdata.Mon_Skill1_Name)) {

			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill1_Name).MonsterSkillName;
			// 조건들을 만족하면 스킬 실행
			GetComponent<EnemySkill>().UseSkill(monster,Monsterdata.Mon_Skill1_Name,koreanSkillName,TargetList);


		} else if (Random.Range (1, 100) <= Skill2Rate && SkillCondition (monster,Monsterdata.Mon_Skill2_Name)) {
			koreanSkillName=GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(Monsterdata.Mon_Skill2_Name).MonsterSkillName;
			// 조건들을 만족하면 스킬 실행
			GetComponent<EnemySkill>().UseSkill(monster,Monsterdata.Mon_Skill2_Name,koreanSkillName,TargetList);

		} else {
			// 스킬조건을 만족하지못하면 평타 
			GetComponent<EnemySkill>().UseAttack (monster);
		}
	}

	//not implemented
	bool SkillCondition(GameObject monster, string SkillName)
	{
		List<string> Condition1List=new List<string>();
		List<string> Condition2List=new List<string>();
		List<string> Condition3List=new List<string>();

		GetComponent<MonsterSkillLoad> ().SetConditionList (SkillName, Condition1List, Condition2List, Condition3List);

		if (IdentifyConditionList(monster,Condition1List , GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(SkillName).Target))
			return true;


		if (IdentifyConditionList(monster,Condition2List,GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(SkillName).Target))
            return true;


		if (IdentifyConditionList(monster,Condition3List,GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(SkillName).Target)) 
			return true;
		return false;
	
	}



	bool IdentifyConditionList(GameObject monster, List<string> Conditionlist , string Target_string){

		if (Conditionlist[0] == "N/A")//N/A일 경우는 false
			return false;

		if (Conditionlist [0] == "Always") {//Always 일 경우는 리스트 만들고 바로 true

			TargetList=new List<GameObject>();
			switch (Target_string) {
			case "All":
				TargetList.Add (GameObject.Find ("Player(Clone)"));
				GameObject[] TargetAllmonster=GameObject.FindGameObjectsWithTag("Monster");
				foreach(GameObject monsterinfield in TargetAllmonster){
					TargetList.Add (monsterinfield);
				}
				break;
			case "Player":
				TargetList.Add (GameObject.Find ("Player(Clone)"));
				break;
			case "Monster":
				GameObject[] Targetmonster=GameObject.FindGameObjectsWithTag("Monster");
				foreach(GameObject monsterinfield in Targetmonster){
					TargetList.Add (monsterinfield);
				}
				break;
			}
			return true;
		}

		int falseCount = 0;//조건을 하나씩 확인하고 false가 하나라도 있으면 false를 출력한다. 

		foreach (string ConditionName in Conditionlist) {

			if (IdentifyCondition (monster, ConditionName, Target_string))
				falseCount++;
		}
		return (falseCount == 0);
	}



	bool IdentifyCondition(GameObject monster, string ConditionName, string Target_string){
		Debug.Log (ConditionName);

		if (ConditionName == "N/A")
			return true;
		
		TargetList=new List<GameObject>();

		switch (Target_string) {
		case "All":
			TargetList.Add (GameObject.Find ("Player(Clone)"));
			GameObject[] TargetAllmonster=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monsterinfield in TargetAllmonster){
			TargetList.Add (monsterinfield);
			}
			break;
		case "Player":
			TargetList.Add (GameObject.Find ("Player(Clone)"));
			break;
		case "Monster":
			GameObject[] Targetmonster=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monsterinfield in Targetmonster){
			TargetList.Add (monsterinfield);
			}
			break;
		}

		Debug.Log (TargetList.Count);


		if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState != "N/A") {

			//TargetState 확인
			//타겟 리스트의 타겟을 하나씩 확인 
			for(int i=0; i<TargetList.Count;i++){
				GameObject target = TargetList[i];
			string targetState = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetState;
			ChemicalStates currentState = ChemicalStates.LIQUID;//초기화 
				Debug.Log (target);
			if(target.tag=="Ally")//플레이어인 경우 
			currentState = target.GetComponent<baseCharacter> ().currentChemicalState;
			else if(target.tag=="Monster")//몬스터들의 경우 
			currentState = target.GetComponent<Monster> ().currentChemicalState;
			
			switch (targetState) {

			case "Solid":
				if(currentState != ChemicalStates.SOLID)
				TargetList.Remove(target);
					break;
			case "Liquid":
				if(currentState != ChemicalStates.LIQUID)
						TargetList.Remove(target);
					break;
			case "Gas":
				if(currentState != ChemicalStates.GAS)
						TargetList.Remove(target);
					break;
			case "NoneSolid":
				if(currentState == ChemicalStates.SOLID)
						TargetList.Remove(target);
					break;
			case "NoneLiquid":
				if(currentState == ChemicalStates.LIQUID)
						TargetList.Remove(target);
					break;
			case "NoneGas":
				if(currentState == ChemicalStates.GAS)
						TargetList.Remove(target);
					break;
				}

			}
			return(TargetList.Count>0);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpBelowN > 0) {

			foreach(GameObject target in TargetList){
			//HpBelowN 확인, N보다 플레이어의 Hp가 낮으면 true
			float TargetHp=0 ;
			float TargetMaxHp=0 ;
			
			if(target.tag=="Ally"){
					TargetHp = target.GetComponent<baseCharacter> ().HP;
					TargetMaxHp = target.GetComponent<baseCharacter> ().MAX_HP;
			}else if(target.tag =="Monster"){
					TargetHp = target.GetComponent<Monster>().hp;
					TargetMaxHp = target.GetComponent<Monster> ().maxHp;
				}

			int percenthp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpBelowN;
			if (TargetHp <= TargetMaxHp * percenthp / 100){
					TargetList.Remove(target);
				}
			}
			return(TargetList.Count>0);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpMoreN > 0) {

			foreach(GameObject target in TargetList){
				//HpMoreN 확인, N보다 플레이어의 Hp가 높으면 true
				float TargetHp =0;
				float TargetMaxHp=0 ;
				
				if(target.tag=="Ally"){
					TargetHp = target.GetComponent<baseCharacter> ().HP;
					TargetMaxHp = target.GetComponent<baseCharacter> ().MAX_HP;
				}else if(target.tag =="Monster"){
					TargetHp = target.GetComponent<Monster>().hp;
					TargetMaxHp = target.GetComponent<Monster> ().maxHp;
				}
				
				int percenthp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetHpMoreN;
				if (TargetHp >= TargetMaxHp * percenthp / 100){
					TargetList.Remove(target);
				}
			}
			return(TargetList.Count>0);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).SelfHpBelowN > 0) {

			//SelfHpBelowN 확인, N보다 자신의 Hp가 낮으면 true
			float Hp = monster.GetComponent<Monster> ().hp;
			float ConditionHp = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).SelfHpBelowN;
			return (Hp < ConditionHp);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).Actionlimit != "N/A") {

			//if player's Action is limited return true

			foreach(GameObject target in TargetList){
				string actionlimit = GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).Actionlimit;
				if (actionlimit == "N") {



					
				} else if (actionlimit == "Y") {


					
					
				}
			}
			return(TargetList.Count>0);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetNumber > 0) {
			//not implemented

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).RandomRate > 0) {
			//RandomRate 확인, 랜덤 확률 
			return(Random.Range (1, 100) <= GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).RandomRate);

		} else if (GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (ConditionName).TargetAffectedEffect != "N/A") {
			//TargetAffectedEffect 확인,
			string targetaffectedeffect = GetComponent<MonsterSkillConditionLoad>().Find_UseCondition (ConditionName).TargetAffectedEffect;

			for(int i=0;i<TargetList.Count;i++){
				GameObject target =TargetList[i];
			switch(targetaffectedeffect){
			case "Debuff":
					if(target.tag=="Monster"&&target.GetComponent<Monster>().DebuffListCount()==0)
						TargetList.Remove(target);
					if(target.tag=="Ally"){
					}
					break;
			case "Buff":
					if(target.tag=="Monster"&&target.GetComponent<Monster>().BuffListCount()==0)
						TargetList.Remove(target);
					if(target.tag=="Ally"&&target.GetComponent<baseCharacter>().BuffListCount()==0){
						TargetList.Remove(target);
					}
					break;
			case "DotDamage":
					if(target.tag=="Monster"&&target.GetComponent<Monster>().DotDamageListCount()==0)
						TargetList.Remove (target);
					break;
			case "NoShield":
			break;
				}
			}
			return (TargetList.Count>0);
		}
		return false;


	}
	/*
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
