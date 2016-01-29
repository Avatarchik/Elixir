using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loader : MonoBehaviour {
    public TextAsset database;
    public List<Element> elementList = new List<Element>();
	// Use this for initialization
	void Awake () {
        Load();
	}

    void Load()
    {
        string[][] grid = CsvParser2.Parse(database.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Element element = new Element();

            element.id = grid[i][0];
            element.extName = grid[i][1];
            element.name = grid[i][2];
            element.chemicalSeries = grid[i][3];
            element.desription = grid[i][4];
            element.characterRoomTempState = grid[i][5];
            element.solidGauge = System.Convert.ToInt32(grid[i][6]);
            element.liquidGauge = System.Convert.ToInt32(grid[i][7]);
            element.gasGauge = System.Convert.ToInt32(grid[i][8]);
            element.roomTempPos = (float)System.Convert.ToDouble(grid[i][9]);
            element.elementCard1 = grid[i][10];
            element.elementCard2 = grid[i][11];
            element.elementCard3 = grid[i][12];
            element.enableChemSeriesCard = grid[i][13];
            element.enableCardType = grid[i][14];
            
            elementList.Add(element);
        }
    }
}

