using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class EnemySkill : MonoBehaviour {

	public void UseSkill(GameObject monster,string SkillName, string KoreanSkillName){
		Debug.Log (monster + " use " + SkillName);
		GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateMonsterSkillPopup (monster.transform,KoreanSkillName);
		MonsterSkillRow skillrow = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID (SkillName);
		List<GameObject> Targetlist;

		if (skillrow.DamageFactor != 0) {
			Targetlist =SelectTarget (SkillName);

		}
		if (skillrow.Heal != 0) {
			Targetlist = SelectTarget(SkillName);
		}
		if (skillrow.TargetTempChange != 0) {
			Targetlist = SelectTarget(SkillName);
		}
		if (skillrow.TargetStateChange != "N/A") {
			Targetlist = SelectTarget(SkillName);
		}
		if (skillrow.DebuffName != "N/A" && skillrow.DebuffRate >= Random.Range (1, 100)) {
			Targetlist = SelectTarget(SkillName);
		
		}
		if (skillrow.BuffName != "N/A" && skillrow.BuffRate >= Random.Range (1, 100)) {
			Targetlist = SelectTarget(SkillName);
		}




	}
	public void UseAttack(GameObject monster){
		Animator animator=monster.GetComponent<Animator>();
		if (GameObject.Find ("Player(Clone)") != null) {
			Debug.Log ("Monster Attack");
			GameObject.Find ("Player(Clone)").GetComponent<BaseCharacter> ().SetDamage (monster.GetComponent<Monster> ().attackDamage);
			animator.SetTrigger ("Attack");
		}
	}
	List<GameObject> SelectTarget(string SkillName){
		List<GameObject> list=new List<GameObject>();
		switch(GetComponent<MonsterSkillLoad>().Find_MonsterSkillID (SkillName).Target){
		case "All":
			list.Add (GameObject.Find ("Player"));
			GameObject[] monsters=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in monsters){
				Debug.Log (monster);
				list.Add (monster);
			}
			break;
		case "Player":

			list.Add (GameObject.Find ("Player"));
			break;
		case "Monster":
			GameObject[] Targetmonster=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in Targetmonster){
				Debug.Log (monster);
				list.Add (monster);
			}
			break;
		}
		Debug.Log (list.Count);
		return list;
	}
}
