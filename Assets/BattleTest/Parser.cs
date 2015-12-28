using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser : MonoBehaviour {

	List<Skill> skills = new List<Skill>();

	void Print () 
	{
		foreach (var skill in skills)
		{
			Debug.Log(skill.name + ", " + skill.power);
		}
	}

	// Use this for initialization
	void Start () {
		TextAsset csvFile = Resources.Load("chem_skill") as TextAsset;
		string csvText = csvFile.text;
		string[] unparsedSkillStrings = csvText.Split('\n');
		Debug.Log(unparsedSkillStrings.Length);
		for (int i = 1; i < unparsedSkillStrings.Length; i++)
		{
			Debug.Log(unparsedSkillStrings[i]);
			Skill skill = new Skill(unparsedSkillStrings[i]);
			skills.Add(skill);
		}
		
		// Using test.
		Print();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
