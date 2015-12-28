using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

// FIXME : move to 'Element' class.
public enum ElementName
{
	None,
	Hydrogen,
	Carbon,
	Silver,
	Argon
}

public enum TargetType
{
	None,
	Ally,
	Enemy
}

public enum Effect
{
	None,
	Purify,
	Stun,
	Provoke
}

public class Skill {

	public ElementName name;
	public string skillName;
	public TargetType targetType;
	public int numOfTargets;
	public int power;
	public int requireTurns;
	public Effect skillEffect;
	public int remainTurns;

	public Skill (string data)
	{
		string[] stringList = data.Split(',');
		Assert.IsTrue(stringList.Length == 8);

		this.name = (ElementName)Enum.Parse(typeof(ElementName), stringList[0]);
		this.skillName = stringList[1];
		this.targetType = (TargetType)Enum.Parse(typeof(TargetType), stringList[2]);
		this.numOfTargets = Int32.Parse(stringList[3]);
		this.power = Int32.Parse(stringList[4]);
		this.requireTurns = Int32.Parse(stringList[5]);
		this.skillEffect = (Effect)Enum.Parse(typeof(Effect), stringList[6]);
		this.remainTurns = Int32.Parse(stringList[7]);
	}
}
