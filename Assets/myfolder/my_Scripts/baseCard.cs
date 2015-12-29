using UnityEngine;
using System.Collections;

public class baseCard{

	private int card_ID;
	private string card_ExtName;
	private string card_Name;
	private string card_Chemical_Series;
	private string card_Type;
	private string card_Description;
	private string card_Attack_Range;
	private double card_Attack_Damage;
	private double card_HP_Damage;
	private double card_Max_Damage;
	private double card_Dot_Damage_Turn;
	private double card_Heal;
	private string card_Condition;
	private string card_Additional_Card;
	private string card_Effect1_Name;
	private double card_Effect1_Rate;
	private int card_Effect1_Turn;
	private string card_Effect2_Name;
	private double card_Effect2_Rate;
	private int card_Effect2_Turn;

	
	
	public int Card_ID{
		get{return card_ID;}
		set{card_ID = value;}
	}
	
	public string Card_ExtName{
		get{return card_ExtName;}
		set{card_ExtName = value;}
	}
	
	public string Card_Name{
		get{return card_Name;}
		set{card_Name = value;}
	}
	
	
	public string Card_Chemical_Series{
		get{return card_Chemical_Series;}
		set{card_Chemical_Series = value;}
	}
	
	public string Card_Type{
		get{return card_Type;}
		set{card_Type = value;}
	}
	
	public string Card_Description{
		get{ return card_Description;}
		set{ card_Description = value;}
	}

	public string Card_Attack_Range{
		get{return card_Attack_Range;}
		set{card_Attack_Range = value;}
	}
	public double Card_Attack_Damage{
		get{return card_Attack_Damage;}
		set{card_Attack_Damage = value;}
	}
	public double Card_HP_Damage{
		get{return card_HP_Damage;}
		set{card_HP_Damage = value;}
	}
	public double Card_Max_Damage{
		get{return card_Max_Damage;}
		set{card_Max_Damage = value;}
	}
	public double Card_Dot_Damage_Turn{
		get{return card_Dot_Damage_Turn;}
		set{card_Dot_Damage_Turn = value;}
	}
	public double Card_Heal{
		get{return card_Heal;}
		set{card_Heal = value;}
	}
	public string Card_Condition{
		get{return card_Condition;}
		set{card_Condition = value;}
	}
	public string Card_Additional_Card{
		get{return card_Additional_Card;}
		set{card_Additional_Card = value;}
	}
	public string Card_Effect1_Name{
		get{return card_Effect1_Name;}
		set{card_Effect1_Name = value;}
	}
	public double Card_Effect1_Rate{
		get{return card_Effect1_Rate;}
		set{card_Effect1_Rate = value;}
	}
	public int Card_Effect1_Turn{
		get{return card_Effect1_Turn;}
		set{card_Effect1_Turn = value;}
	}
	public string Card_Effect2_Name{
		get{return card_Effect2_Name;}
		set{card_Effect2_Name = value;}
	}
	public double Card_Effect2_Rate{
		get{return card_Effect2_Rate;}
		set{card_Effect2_Rate = value;}
	}
	public int Card_Effect2_Turn{
		get{return card_Effect2_Turn;}
		set{card_Effect2_Turn = value;}
	}
}
