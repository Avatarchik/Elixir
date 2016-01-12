using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			    AttackAlly(monster.GetComponent<Monster>());
			    yield return new WaitForSeconds(EnemyBehaviourAfterDelay);
            }
			
		}
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE;
        yield break;
    }
	void AttackAlly(Monster monster){
		Animator animator=monster.GetComponent<Animator>();
		if(GameObject.Find ("Player(Clone)")!=null)
		{
			Debug.Log ("Monster Attack");
			GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
			animator.SetTrigger("Attack");
		}
	}
	void SelectAct(Monster monster){
		baseMonster Monsterdata = monster.GetComponent<InfoMonster> ().MonsterInfo;
		double Skill1Rate = Monsterdata.Mon_Skill1_Rate;
		double Skill2Rate = Monsterdata.Mon_Skill2_Rate;
		Animator animator=monster.GetComponent<Animator>();

		if (Random.Range (1, 100) <= Skill1Rate && SkillCondition (Monsterdata.Mon_Skill1_Name, 1)) {
			//skill1

		} else if (Random.Range (1, 100) <= Skill2Rate && SkillCondition (Monsterdata.Mon_Skill2_Name, 2)) {
			//skill2

		} else {
			if (GameObject.Find ("Player(Clone)") != null) {
				Debug.Log ("Monster Attack");
				GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
				animator.SetTrigger ("Attack");
			}
		}
	}

	//not implemented
	bool SkillCondition(string SkillName, int SkillNum)
	{
		GetComponent<MonsterSkillConditionLoad> ().Find_UseCondition (SkillName);



		return true;
	}

}
