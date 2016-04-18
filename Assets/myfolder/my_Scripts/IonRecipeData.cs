using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class IonRecipeData {

	private int ionID;
	private string ionExtName;
	private string ionName;

	private string[,] ionIngredientInfo = new string[5,(int)INGREDIENT.MAX];

	/*
	private string ingredient1;
	private string ingredient1Type;
	private string ingredient1ElectronArr;

	private string ingredient2;
	private string ingredient2Type;
	private string ingredient2ElectronArr;

	private string ingredient3;
	private string ingredient3Type;
	private string ingredient3ElectronArr;

	private string ingredient4;
	private string ingredient4Type;
	private string ingredient4ElectronArr;

	private string ingredient5;
	private string ingredient5Type;
	private string ingredient5ElectronArr;
	*/
	public void SetIonID(int ID)
	{
		ionID = ID;
	}

	public void SetIonExtName(string extName)
	{
		ionExtName = extName;
	}

	public void SetIonName(string name)
	{
		ionName = name;
	}

	public void SetIonIngredientInfo(int index, INGREDIENT ingredient, string info)
	{
		ionIngredientInfo [index, (int)ingredient] = info;
	}


	public int GetIonID()
	{
		return ionID;
	}

	public string GetIonExtName()
	{
		return ionExtName;
	}

	public string GetIonName()
	{
		return ionName;
	}

	public string GetIonIngredientInfo(int index, INGREDIENT ingredient)
	{
		return ionIngredientInfo [index, (int)ingredient];
	}
}
