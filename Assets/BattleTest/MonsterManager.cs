using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class MonsterManager : MonoBehaviour {

    public GameObject monsterPrefab;

    List<Vector3> positions;
    Vector3 firstPosition = new Vector3(1.3f, 0.7f, -2); // left
    Vector3 secondPosition = new Vector3(3.4f, -0.5f, -4); // down
    Vector3 thirdPosition = new Vector3(3.3f, 2.4f, -1); // up
    Vector3 fourthPosition = new Vector3(5.4f, 1.2f, -3); // right
    
    List<Monster> monsters;
	public List<Monster> Monsters {
		get{ return monsters;}
		set{ monsters = value;}
	}

    // Use this for initialization
    public void Initialize()
    {
        MakePositionList();
        GenerateMonsters(4);
    }

    public Monster GetMonster(int index)
    {
        Assert.IsTrue(index < monsters.Count);
        return monsters[index];
    }

    void MakePositionList()
    {
        positions = new List<Vector3>();

        positions.Add(firstPosition);
        positions.Add(secondPosition);
        positions.Add(thirdPosition);
        positions.Add(fourthPosition);
    }
    
    void GenerateMonsters(int num)
    {
        monsters = new List<Monster>();
        
        if (num > 4)
        {
            Debug.Log("Generate too many monsters. Input num : " + num);
            num = 4;
        }
        
        List<baseMonster> monsterList = GetComponent<MonsterLoad>().monsterList;
        int currentMonsterListLength = monsterList.Count;
        int[] monsterIndexCounter = new int[currentMonsterListLength];
        List<int> respawnMonsterIndexes = new List<int>();
        for (int i = 0; i < monsterIndexCounter.Length; i++)
        {
            monsterIndexCounter[i] = 0;
        }
        
        for (int i = 0; i < num; i++)
        {
            int monsterIndex = Random.Range(0, currentMonsterListLength); //뽑고 
            // Debug.Log("Pick " + monsterIndex + "th monster.");
            while (monsterIndexCounter[monsterIndex] == monsterList[monsterIndex].Mon_MaxCount) // 꽉 찼으면 
            {
                monsterIndex = Random.Range(0, currentMonsterListLength); //다시 뽑고
                // Debug.Log("Re-pick " + monsterIndex + "th monster.");
            }
            monsterIndexCounter[monsterIndex] += 1; //카운터 올리고    
            respawnMonsterIndexes.Add(monsterIndex);          
        }

        for (int i = 0; i < num; i++)
        {
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monster.transform.position = positions[i];
            
            int monsterIndex = respawnMonsterIndexes[i];
            monster.GetComponent<InfoMonster>().MonsterInfo = GetComponent<MonsterLoad>().monsterList[monsterIndex];
            monster.GetComponent<Monster>().SetStat();
				            
            monsters.Add(monster.GetComponent<Monster>());
        }
    }


	
	// Update is called once per frame
	void Update () {

	}
}
