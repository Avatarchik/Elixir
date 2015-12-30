using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {

    public GameObject monsterPrefab;

    List<Vector2> positions;
    Vector2 firstPosition = new Vector2(2.6f, 0.8f);
    Vector2 secondPosition = new Vector2(3.6f, -1.0f);
    Vector2 thirdPosition = new Vector2(4.0f, 2.0f);
    Vector2 fourthPosition = new Vector2(5.2f, 0.3f);
    
    List<Monster> monsters;

    public Monster GetMonster(int index)
    {
        Assert.IsTrue(index < monsters.Count);
        return monsters[index];
    }

    void MakePositionList()
    {
        positions = new List<Vector2>();

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
            monster.transform.position = new Vector3(positions[i].x, positions[i].y, -1);
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

        GetMonster(1).SetDamage(10);
        GetMonster(3).SetDamage(35);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
