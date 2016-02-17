using UnityEngine;
using System.Collections;

public class InfoCard : MonoBehaviour {
	
	private baseSkill skill;
	
	public baseSkill Skill
	{
		get{ return skill; }
		set{skill = value;}
	}
}
