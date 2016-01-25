using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class EnemySkill : MonoBehaviour {

	public void UseSkill(GameObject monster,string SkillName, string KoreanSkillName){
		Debug.Log (monster + " use " + SkillName);
		//Create Skill popup
		GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateMonsterSkillPopup (monster.transform,KoreanSkillName);

		//set skill info
		MonsterSkillRow skillrow = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID (SkillName);

		List<GameObject> Targetlist;

		//make a targetlist
		Targetlist = SelectTarget (SkillName);
		foreach (GameObject target in Targetlist) {
			Debug.Log (target.name);
		}
		if (skillrow.DamageFactor != 0) {
			foreach (GameObject target in Targetlist) {
				int monsterdamage=monster.GetComponent<Monster>().attackDamage;
				target.GetComponent<BaseCharacter>().SetDamage(monsterdamage*skillrow.DamageFactor/100);
			}
		}
		if (skillrow.Heal != 0) {
			foreach (GameObject target in Targetlist) {
				target.GetComponent<Monster>().SetHeal(skillrow.Heal);
			}
		}
		if (skillrow.TargetTempChange != 0) {

		}
		if (skillrow.TargetStateChange != "N/A") {
		}
		if (skillrow.DebuffName != "N/A" && skillrow.DebuffRate >= Random.Range (1, 100)) {
		
		}
		if (skillrow.BuffName != "N/A" && skillrow.BuffRate >= Random.Range (1, 100)) {
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

		//make a list of target 
		List<GameObject> list=new List<GameObject>();
		switch(GetComponent<MonsterSkillLoad>().Find_MonsterSkillID (SkillName).Target){
		case "All":
			list.Add (GameObject.Find ("Player(Clone)"));
			GameObject[] monsters=GameObject.FindGameObjectsWithTag("Monster");
			foreach(GameObject monster in monsters){
				list.Add (monster);
			}
			break;
		case "Player":
			list.Add (GameObject.Find ("Player(Clone)"));
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
}
