using UnityEngine;
using System.Collections;

public class MonsterEvent : MonoBehaviour {

	private int clickedMonsterID;
	private Monster clickedMonster;
	private bool checkPress;
	private float timer;
	void OnMouseDown()
	{
		clickedMonsterID = this.GetComponent<MonsterIndex> ().MonsterID;
		clickedMonster = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>().monsterList[clickedMonsterID];
		Debug.Log (clickedMonster.monsterExtName);
		CheckLongPress();
	}

	void Update()
	{
		if (checkPress)
		{
			timer -= Time.deltaTime;
			if(timer <= 0)
			{
				GameObject.Find("GameManager").GetComponent<AnalyzePanel>().OpenAnalyzePanel(clickedMonster);
				Debug.Log("sadasdasd");
				checkPress = false;
			}
		}
	}

	public void CheckLongPress()
	{
		checkPress = true;
		timer = 1.5f;
	}

	public void OnMouseUp()
	{
		checkPress = false;
	}
}
