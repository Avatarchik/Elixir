using UnityEngine;
using System.Collections;

public class MonsterIndex : MonoBehaviour {
    private int monsterID;

    public int MonsterID
    {
        get
        {
            return monsterID;
        }

        set
        {
            monsterID = value;
        }
    }
}
