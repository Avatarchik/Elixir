using UnityEngine;
using System.Collections;

public class InfoMonster : MonoBehaviour {

	private baseMonster monsterInfo;
	
	public baseMonster MonsterInfo
	{
		get{ return monsterInfo; }
		set{monsterInfo = value;}
	}
}
