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
        //Temporarily add Monsters in list
        monsterList.Add(SetStats(0));
        monsterList.Add(SetStats(1));
        monsterList.Add(SetStats(2));
        monsterList.Add(SetStats(3));

        Debug.Log(monsterList[0].maxHp);
        //Summon Monsters into field
        GenerateMonsters();
    }

    public void GenerateMonsters()
    {
		Vector3 firstPosition = new Vector3(0.73f, 0.8f, -2); // left
		Vector3 secondPosition = new Vector3(3f, -0.19f, -4); // down
		Vector3 thirdPosition = new Vector3(3f, 2.68f, -1); // up
		Vector3 fourthPosition = new Vector3(5.4f, 0.8f, -3); // right

        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(firstPosition);
        positionList.Add(secondPosition);
        positionList.Add(thirdPosition);
        positionList.Add(fourthPosition);

        for (int i = 0; i < 4; i++)
        {
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monsterObjectList.Add(monster);

            string imagePath = "MonsterImage/" + monsterDatabase[monsterList[i].monsterID].Mon_Name;
            monster.transform.Find("MonsterSprite").GetComponent<SpriteRenderer>().sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
            monster.GetComponent<MonsterIndex>().MonsterID = monsterList[i].monsterID;
            monster.transform.position = positionList[i];
        }
    }

    public Monster SetStats(int index)
    {
        baseMonster baseMon = monsterDatabase[index];
        Debug.Log(monsterDatabase[index].Mon_ExtName);
        Monster newMonster = new Monster();

        newMonster.monsterID = index;
        newMonster.maxHp = baseMon.Mon_HP;
        newMonster.hp = baseMon.Mon_HP;
        newMonster.attackDamage = (int)baseMon.Mon_AttackDamage;
        newMonster.type = baseMon.Mon_Type;
        newMonster.currentChemicalState = baseMon.Mon_RoomTempStatus;
        newMonster.currentChemicalStateValue = 1;
        newMonster.solidStateValue = baseMon.Mon_SolidGauge;
        newMonster.liquidStateValue = baseMon.Mon_LiquidGauge;
        newMonster.gasStateValue = baseMon.Mon_GasGauge;
        newMonster.criticalTarget = baseMon.Mon_CriticalTarget;

        newMonster.Initialize();
        return newMonster;
    }

}
