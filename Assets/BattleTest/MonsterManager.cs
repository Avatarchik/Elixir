using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {

    List<Vector2> positions = new List<Vector2>();
    Vector2 firstPosition = new Vector2(2.6f, 0.6f);
    Vector2 secondPosition = new Vector2(4.0f, -3.0f);
    Vector2 thirdPosition = new Vector2(5.6f, 2.0f);
    Vector2 fourthPosition = new Vector2(7.0f, 1.3f);

    void MakePositionList()
    {
        positions.Add(firstPosition);
        positions.Add(secondPosition);
        positions.Add(thirdPosition);
        positions.Add(fourthPosition);
    }

	// Use this for initialization
	void Start () {
        MakePositionList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
