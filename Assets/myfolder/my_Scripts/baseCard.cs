using UnityEngine;
using System.Collections;

public class baseCard{

	private int cardNum;
	private string cardName;
	private string cardStatement;
	private int attack;
	private string type;
	private string effect;
	
	
	public enum CardTypes
	{
		OXYGEN,
		HYDROGEN,
		GOLD,
		SILVER,
		URANIUM,
		CARBON,
		FLUORINE
	}
	
	
	public int CardNum{
		get{return cardNum;}
		set{cardNum = value;}
	}
	
	public string CardName{
		get{return cardName;}
		set{cardName = value;}
	}
	
	public string CardStatement{
		get{return cardStatement;}
		set{cardStatement = value;}
	}
	
	
	public int Attack{
		get{return attack;}
		set{attack = value;}
	}
	
	public string Type{
		get{return type;}
		set{type = value;}
	}
	
	public string Effect{
		get{ return effect;}
		set{ effect = value;}
	}
}
