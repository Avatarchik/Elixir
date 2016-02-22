using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class EnemySkill : MonoBehaviour {


	public void UseSkill(GameObject monster,string SkillName, string KoreanSkillName, List<GameObject> TargetList){
		Debug.Log (monster + " use " + SkillName);
		//Create Skill popup
		GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateMonsterSkillPopup (monster.transform,KoreanSkillName);

		//set skill info
		MonsterSkillRow skillrow = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID (SkillName);

		//make a targetlist
		foreach (GameObject target in TargetList) {
			Debug.Log (target.name);
		}

		//스킬 구현 
		if (skillrow.DamageFactor != 0) {
			// 스킬 공격력 계수(%)
			foreach (GameObject target in TargetList) {
				int monsterdamage=monster.GetComponent<Monster>().attackDamage;
                GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.SetDamage(monsterdamage*skillrow.DamageFactor/100);
			}
		}
		if (skillrow.Heal != 0) {
			//힐 
			foreach (GameObject target in TargetList) {
				target.GetComponent<Monster>().SetHeal(skillrow.Heal);
			}
		}
		if (skillrow.TargetTempChange != 0) {
			// 온도 변화 

		}
		if (skillrow.TargetStateChange != "N/A") {
			// 상태 변화 
		}
		if (skillrow.DebuffName != "N/A" && skillrow.DebuffRate >= Random.Range (1, 100)) {
			// 디버프 
		}
		if (skillrow.BuffName != "N/A" && skillrow.BuffRate >= Random.Range (1, 100)) {
			//버프 
		}




	}
	public void UseAttack(GameObject monster){
		//평타 구현 
		Animator animator=monster.GetComponent<Animator>();
		if (GameObject.Find ("Player(Clone)") != null) {
			Debug.Log ("Monster Attack");
            if (monster.GetComponent<Monster>().blinded)
            {
                animator.SetTrigger("Attack");
                //Miss effect
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.SetDamage (monster.GetComponent<Monster> ().attackDamage);
			    animator.SetTrigger ("Attack");
            }
            
		}
	}

}
