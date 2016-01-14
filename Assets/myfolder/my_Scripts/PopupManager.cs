using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour {

	public GameObject DamagePopupPrefab;
	public GameObject MonsterSkillPopupPrefab;
	public int sortinglayerID;


	void Start(){
		sortinglayerID=SortingLayer.GetLayerValueFromName("Default");
	}

	public void CreateDamagePopup(Transform damageTransform, int damage){
		GameObject damageGameObject = (GameObject)Instantiate(DamagePopupPrefab,
		                                                      damageTransform.position,
		                                                      damageTransform.rotation);
		damageGameObject.transform.SetParent(damageTransform);
		Renderer renderer = damageGameObject.GetComponent<Renderer> ();
		renderer.sortingOrder = 1;
		damageGameObject.GetComponentInChildren<TextMesh>().text = damage.ToString();
	}

	public void CreateMonsterSkillPopup(Transform monsterTransform, string skillname)
	{
		GameObject MonsterSkillGameObject = (GameObject)Instantiate(MonsterSkillPopupPrefab,
		                                                      monsterTransform.position+new Vector3(-1f,1.5f,0),
		                                                      monsterTransform.rotation);
		MonsterSkillGameObject.transform.SetParent(monsterTransform);
		Renderer renderer = MonsterSkillGameObject.GetComponent<Renderer> ();
		renderer.sortingOrder = 1;
		MonsterSkillGameObject.GetComponentInChildren<TextMesh> ().text = skillname;

	}
}
