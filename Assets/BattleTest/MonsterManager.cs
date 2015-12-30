using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class MonsterManager : MonoBehaviour {

    public GameObject monsterPrefab;

    List<Vector3> positions;
    Vector3 firstPosition = new Vector3(1.9f, 0.8f, -2);
    Vector3 secondPosition = new Vector3(3.4f, -1.0f, -4);
    Vector3 thirdPosition = new Vector3(3.4f, 2.0f, -1);
    Vector3 fourthPosition = new Vector3(4.9f, 0.3f, -3);
    
    List<Monster> monsters;

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

        for (int i = 0; i < num; i++)
        {
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monster.transform.position = positions[i];
            if (i%2 == 0)
            {
                monster.GetComponent<Monster>().SetStat("MonsterImage/newWhite");
                Debug.Log("Respawn white enemy");
            }
            else
            {
                monster.GetComponent<Monster>().SetStat();
                Debug.Log("Respawn red enemy");
            }
            
            monsters.Add(monster.GetComponent<Monster>());
        }
    }

	// Use this for initialization
	void Start () {
        MakePositionList();
        GenerateMonsters(4);

        // Damage Test.
        // GetMonster(1).SetDamage(10);
        // GetMonster(3).SetDamage(35);
        
        // Debuff Test.
        // Debuff stunDebuff = new Debuff(DebuffName.Stun, 3);
        // GetMonster(1).AddDebuff(stunDebuff);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
