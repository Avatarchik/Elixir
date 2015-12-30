using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {

    public GameObject monsterPrefab;

    List<Vector2> positions = new List<Vector2>();
    Vector2 firstPosition = new Vector2(2.6f, 0.6f);
    Vector2 secondPosition = new Vector2(4.0f, -3.0f);
    Vector2 thirdPosition = new Vector2(5.6f, 2.0f);
    Vector2 fourthPosition = new Vector2(7.0f, -1.3f);

    void MakePositionList()
    {
        positions.Add(firstPosition);
        positions.Add(secondPosition);
        positions.Add(thirdPosition);
        positions.Add(fourthPosition);
    }
    
    void GenerateMonsters(int num)
    {
        if (num > 4)
        {
            num = 4;
            Debug.Log("Too many monsters");
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
        }
    }

	// Use this for initialization
	void Start () {
        MakePositionList();
        GenerateMonsters(4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
