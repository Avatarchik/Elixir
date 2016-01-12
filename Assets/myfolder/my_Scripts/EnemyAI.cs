using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {


	float EnemyBehaviourBeforeDelay = 1.0f;
	float EnemyBehaviourAfterDelay=1.0f;

	public IEnumerator EnemyActChoice(List<Monster> monsters)
	{

		GameObject.Find ("GameManager").GetComponent<TurnBasedCombatStateMachine> ().currentState=TurnBasedCombatStateMachine.BattleStates.IDLE;
		Debug.Log ("Coroutine is started");
		foreach (Monster monster in monsters)
		{
			Debug.Log ("Monster Action");
            if(monster.stunned == true)
            {
                Debug.Log("Monster is stunned. Does not attack.");
            }
            else
            {
                yield return new WaitForSeconds(EnemyBehaviourBeforeDelay);
			    AttackAlly(monster);
			    yield return new WaitForSeconds(EnemyBehaviourAfterDelay);
            }
			
		}
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE;
        yield break;
    }
	void AttackAlly(Monster monster){
		Animator animator=monster.GetComponent<Animator>();
		Debug.Log (GameObject.Find ("Player(Clone)"));
		if(GameObject.Find ("Player(Clone)")!=null)
		{
			Debug.Log ("Monster Attack");
		GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
			animator.SetTrigger("Attack");
		}
	}
}
