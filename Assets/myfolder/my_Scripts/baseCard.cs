using UnityEngine;
using System.Collections;

public class baseCard{

	private int card_ID;
	private string card_ExtName;
	private string card_Name;
	private string card_ChemicalSeries;
	private string card_Type;
	private string card_Description;
	private int card_Target;
	private string card_Range;
	private double card_AttackDamage;
	private double card_IncreaseDamage;
	private double card_AllyAdditionalDamageRate;
	private double card_Heal;
	private string card_ConditionCard;
	private int card_ConditionState;
	private int card_ConditionTurn;
	private int card_ConditionState1;
	private int card_ConditionState2;
	private string card_AdditionalCard;
	private string card_DebuffName;
	private double card_DebuffRate;
	private int card_DebuffTurn;
	private double card_DotDamage;
	private string card_BuffName;
	private double card_BuffRate;
	private int card_BuffTurn;
	private double card_SummonedCreatureHP;
	private double card_SummonedCreatureAttackDamage;
	private string card_EliminateDebuff;
	private string card_EliminateBuff;


	
	
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
	
	
	public string Card_ChemicalSeries{
		get{return card_ChemicalSeries;}
		set{card_ChemicalSeries = value;}
	}
	
	public string Card_Type{
		get{return card_Type;}
		set{card_Type = value;}
	}
	
	public string Card_Description{
		get{ return card_Description;}
		set{ card_Description = value;}
	}

	public string Card_Target {
		get{ return card_Target;}
		set{ card_Target = value;}
	}

	public string Card_Range{
		get{return card_Range;}
		set{card_Range = value;}
	}

	public double Card_AttackDamage{
		get{return card_AttackDamage;}
		set{card_AttackDamage = value;}
	}

	public double Card_IncreaseDamage{
		get{ return card_IncreaseDamage;}
		set{card_IncreaseDamage=value;}
	}
	public double Card_AllyAdditionalDamageRate {
		get{ return card_AllyAdditionalDamageRate;}
		set{ card_AllyAdditionalDamageRate = value;}
	}

	public double Card_Heal{
		get{return card_Heal;}
		set{card_Heal = value;}
	}

	public string Card_ConditionCard{
		get{return card_ConditionCard;}
		set{card_ConditionCard = value;}
	}
	public int Card_ConditionTurn{
		get{return card_ConditionTurn;}
		set{card_ConditionTurn = value;}
	}

	public int Card_ConditionState1 {
		get{ return card_ConditionState1;}
		set{ card_ConditionState1 = value;}
	}
	public int Card_ConditionState2 {
		get{ return card_ConditionState2;}
		set{ card_ConditionState2 = value;}
	}
	public string Card_AdditionalCard {
		get{ return card_AdditionalCard;}
		set{ card_AdditionalCard = value;}
	}

	public string Card_DebuffName{
		get{return card_DebuffName;}
		set{card_DebuffName = value;}
	}
	public double Card_DebuffRate{
		get{return card_DebuffRate;}
		set{card_DebuffRate = value;}
	}
	public int Card_DebuffTurn{
		get{return card_DebuffTurn;}
		set{card_DebuffTurn = value;}
	}
	public double Card_DotDamage {
		get{ return card_DotDamage;}
		set{ card_DotDamage = value;}
	}
	public string Card_BuffName{
		get{return card_BuffName;}
		set{card_BuffName = value;}
	}
	public double Card_BuffRate{
		get{return card_BuffRate;}
		set{card_BuffRate = value;}
	}
	public int Card_BuffTurn{
		get{return card_BuffTurn;}
		set{card_BuffTurn = value;}
	}
	public double Card_SummonedCreatureHP{
		get{return card_SummonedCreatureHP;}
		set{card_SummonedCreatureHP=value;}
	}
	public double Card_SummonedCreatureAttackDamage{
		get{ return card_SummonedCreatureAttackDamage;}
		set{ card_SummonedCreatureAttackDamage = value;}
	}
	public string Card_EliminateDebuff{
		get{return card_EliminateDebuff;}
		set{ card_EliminateDebuff=value;}
	}
	public string Card_EliminateBuff{
		get{return card_EliminateBuff;}
		set{card_EliminateBuff=value;}
	}
}
