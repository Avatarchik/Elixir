using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class Loader : MonoBehaviour {
    public TextAsset database;
    public List<Element> elementList = new List<Element>();
	// Use this for initialization
	void Awake () {
        //Load();
	}

    public void Load()
    {
        string[][] grid = CsvParser2.Parse(database.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Element element = new Element();

            element.id = grid[i][0];
            element.extName = grid[i][1];
            element.name = grid[i][2];
            element.chemicalSeries = grid[i][3];
			element.elementNumber = System.Convert.ToInt32 (grid[i][4]);
			element.properLevel = System.Convert.ToInt32 (grid[i][5]);
            element.desription = grid[i][6];
			element.meltingPoint = System.Convert.ToSingle(grid[i][7]);
			element.boilingPoint = System.Convert.ToSingle(grid[i][8]);
			switch (grid[i][9])
            {
                case "Gas":
                    element.characterRoomTempState = ChemicalStates.GAS;
                    break;
                case "Liquid":
                    element.characterRoomTempState = ChemicalStates.LIQUID;
                    break;
                case "Solid":
                    element.characterRoomTempState = ChemicalStates.SOLID;
                    break;
            }
            element.solidGauge = System.Convert.ToInt32(grid[i][10]);
            element.liquidGauge = System.Convert.ToInt32(grid[i][11]);
            element.gasGauge = System.Convert.ToInt32(grid[i][12]);
			element.roomTempPos = (System.Convert.ToSingle(grid[i][13]));
            element.elementCard1 = grid[i][14];
            element.elementCard2 = grid[i][15];
            element.elementCard3 = grid[i][16];
            //element.enableChemSeriesCard = grid[i][13];
            //element.enableCardType = grid[i][14];
            
            elementList.Add(element);
        }
    }
}

