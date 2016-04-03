using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterPrefs : MonoBehaviour {
    public GameObject monsterPrefab;

    public List<Monster> monsterList;
    public List<GameObject> monsterObjectList;
    public List<baseMonster> monsterDatabase;

    public void Initialize()
    {
        monsterList = new List<Monster>();
        monsterObjectList = new List<GameObject>();
        monsterDatabase = GetComponent<MonsterLoad>().monsterList;
		int[] arMonsterIndex = {-1, -1, -1, -1};
		Debug.Log("Monster List Count : " + monsterDatabase.Count);
		int nMonsterArrayIndex = 0;
		int nRandomMonsterValue = 0;

		while (nMonsterArrayIndex < 4)
		{
			bool bHaveSame = false;
			nRandomMonsterValue = Random.Range (0, monsterDatabase.Count);
			for (int i = 0; i < 4; i++)
			{
				if (nRandomMonsterValue == arMonsterIndex [i])
					bHaveSame = true;
			}

			if (!bHaveSame) 
			{
				arMonsterIndex [nMonsterArrayIndex] = nRandomMonsterValue;
				nMonsterArrayIndex++;
			} 
		}
        //Temporarily add Monsters in list
		monsterList.Add(SetStats(0, arMonsterIndex[0]));
		monsterList.Add(SetStats(1, arMonsterIndex[1]));
		monsterList.Add(SetStats(2, arMonsterIndex[2]));
		monsterList.Add(SetStats(3, arMonsterIndex[3]));
		//Debug.Log ("Monster List 4 : " + monsterList[0].monsterID);
		//Debug.Log(monsterList[0].maxHp);
        //Summon Monsters into field
		GenerateMonsters(arMonsterIndex);
    }

	public void GenerateMonsters(int[] arMonsterData)
    {
		Vector3 firstPosition = new Vector3(0.5f, 0.7f, -2); // left
		Vector3 secondPosition = new Vector3(1.8f, -0.19f, -4); // down
		Vector3 thirdPosition = new Vector3(2.2f, 1.5f, -1); // up
		Vector3 fourthPosition = new Vector3(3.5f, 0.6f, -3); // right

        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(firstPosition);
        positionList.Add(secondPosition);
        positionList.Add(thirdPosition);
        positionList.Add(fourthPosition);

        for (int i = 0; i < 4; i++)
        {
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monsterObjectList.Add(monster);
			//monsterList[i].monsterID
			string imagePath = "MonsterImage/" + monsterDatabase[arMonsterData[i]].Mon_Name;
			Debug.Log (imagePath);
            monster.transform.Find("MonsterSprite").GetComponent<SpriteRenderer>().sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
            monster.GetComponent<MonsterIndex>().MonsterID = monsterList[i].monsterID;
			Debug.Log ("Loaded Monster Weakness : " + monsterList[i].weakPoint);
            monster.transform.position = positionList[i];
        }
    }

	public Monster SetStats(int index, int nEnemyNumber)
    {
		baseMonster baseMon = monsterDatabase[nEnemyNumber];
		Debug.Log("SetStats : " + monsterDatabase[nEnemyNumber].Mon_WeakPoint);
        Monster newMonster = new Monster();

        newMonster.monsterID = index;
        newMonster.maxHp = baseMon.Mon_HP;
		newMonster.monsterExtName = baseMon.Mon_ExtName;
        newMonster.hp = baseMon.Mon_HP;
        newMonster.attackDamage = (int)baseMon.Mon_AttackDamage;
        newMonster.type = baseMon.Mon_Type;
        newMonster.currentChemicalState = baseMon.Mon_RoomTempStatus;
        newMonster.currentChemicalStateValue = 1;
        newMonster.solidStateValue = baseMon.Mon_SolidGauge;
        newMonster.liquidStateValue = baseMon.Mon_LiquidGauge;
        newMonster.gasStateValue = baseMon.Mon_GasGauge;
		newMonster.weakPoint = baseMon.Mon_WeakPoint;

        newMonster.Initialize();
        return newMonster;
    }

}
