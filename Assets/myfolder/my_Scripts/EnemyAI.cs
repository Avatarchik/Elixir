using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

	float EnemyBehaviourBeforeDelay = 3.0f;
	float EnemyBehaviourAfterDelay=5.0f;

	public IEnumerator EnemyActChoice(List<Monster> monsters)
	{

		GameObject.Find ("GameManager").GetComponent<TurnBasedCombatStateMachine> ().currentState=TurnBasedCombatStateMachine.BattleStates.IDLE;
		Debug.Log ("Coroutine is started");
		foreach (Monster monster in monsters)
		{
			Debug.Log ("Monster Action");
			yield return new WaitForSeconds(EnemyBehaviourBeforeDelay);
			AttackAlly(monster);
			yield return new WaitForSeconds(EnemyBehaviourAfterDelay);
		}
	}
	void AttackAlly(Monster monster){
		Debug.Log ("Monster Attack");
		Debug.Log (GameObject.Find ("Player"));
		if(GameObject.Find ("Player")!=null)
		{
		GameObject.Find ("Player").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
		}
	}
}
