using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class IonRecipeLoader : MonoBehaviour {

	public TextAsset ionRecipeDataFile;
	public List<IonRecipeData> ionRecipeList;
	// Use this for initialization
	void Start () 
	{
		ionRecipeList = new List<IonRecipeData> ();
		ParsingData(ionRecipeDataFile);
		printData ();
	}

	private void ParsingData(TextAsset ionRecipeDataFile)
	{
		string[][] grid = CsvParser2.Parse(ionRecipeDataFile.text);
		for (int i = 1; i < grid.Length; i++) {
			IonRecipeData row = new IonRecipeData ();
			row.SetIonID (System.Convert.ToInt32(grid[i][0]));
			row.SetIonExtName (grid[i][1]);
			row.SetIonName (grid[i][2]);

			row.SetIonIngredientInfo (0, INGREDIENT.MATERIAL, grid[i][3]);
			row.SetIonIngredientInfo (0, INGREDIENT.TYPE, grid[i][4]);
			row.SetIonIngredientInfo (0, INGREDIENT.ELECTRONARR, grid[i][5]);

			row.SetIonIngredientInfo (1, INGREDIENT.MATERIAL, grid[i][6]);
			row.SetIonIngredientInfo (1, INGREDIENT.TYPE, grid[i][7]);
			row.SetIonIngredientInfo (1, INGREDIENT.ELECTRONARR, grid[i][8]);

			row.SetIonIngredientInfo (2, INGREDIENT.MATERIAL, grid[i][9]);
			row.SetIonIngredientInfo (2, INGREDIENT.TYPE, grid[i][10]);
			row.SetIonIngredientInfo (2, INGREDIENT.ELECTRONARR, grid[i][11]);

			row.SetIonIngredientInfo (3, INGREDIENT.MATERIAL, grid[i][12]);
			row.SetIonIngredientInfo (3, INGREDIENT.TYPE, grid[i][13]);
			row.SetIonIngredientInfo (3, INGREDIENT.ELECTRONARR, grid[i][14]);

			row.SetIonIngredientInfo (4, INGREDIENT.MATERIAL, grid[i][15]);
			row.SetIonIngredientInfo (4, INGREDIENT.TYPE, grid[i][16]);
			row.SetIonIngredientInfo (4, INGREDIENT.ELECTRONARR, grid[i][17]);

			ionRecipeList.Add (row);
		}
	}

	private void printData()
	{
		Debug.Log (ionRecipeList.Count);
		foreach (IonRecipeData ed in ionRecipeList) 
		{
			Debug.Log (ed.GetIonID() + " " + ed.GetIonExtName() + " " + ed.GetIonName());
			Debug.Log (ed.GetIonIngredientInfo(0, INGREDIENT.MATERIAL) + " " + ed.GetIonIngredientInfo(0, INGREDIENT.TYPE) + " " + ed.GetIonIngredientInfo(0, INGREDIENT.ELECTRONARR));
			Debug.Log (ed.GetIonIngredientInfo(1, INGREDIENT.MATERIAL) + " " + ed.GetIonIngredientInfo(1, INGREDIENT.TYPE) + " " + ed.GetIonIngredientInfo(1, INGREDIENT.ELECTRONARR));
			Debug.Log (ed.GetIonIngredientInfo(2, INGREDIENT.MATERIAL) + " " + ed.GetIonIngredientInfo(2, INGREDIENT.TYPE) + " " + ed.GetIonIngredientInfo(2, INGREDIENT.ELECTRONARR));
			Debug.Log (ed.GetIonIngredientInfo(3, INGREDIENT.MATERIAL) + " " + ed.GetIonIngredientInfo(3, INGREDIENT.TYPE) + " " + ed.GetIonIngredientInfo(3, INGREDIENT.ELECTRONARR));
			Debug.Log (ed.GetIonIngredientInfo(4, INGREDIENT.MATERIAL) + " " + ed.GetIonIngredientInfo(4, INGREDIENT.TYPE) + " " + ed.GetIonIngredientInfo(4, INGREDIENT.ELECTRONARR));
		}
	}
}
