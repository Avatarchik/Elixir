using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

	float EnemyBehaviourDelay = 8.0f;

	public IEnumerator EnemyActChoice(List<Monster> monsters)
	{

		GameObject.Find ("GameManager").GetComponent<TurnBasedCombatStateMachine> ().currentState=TurnBasedCombatStateMachine.BattleStates.IDLE;
		Debug.Log ("Coroutine is started");
		foreach (Monster monster in monsters)
		{
			Debug.Log ("Monster Action");
			AttackAlly();
			yield return new WaitForSeconds(EnemyBehaviourDelay);
		}
	}
	void AttackAlly(){
		Debug.Log ("Monster Attack");
	}
}
