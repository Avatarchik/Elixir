using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;
public class ElectronMapLoader : MonoBehaviour {

	public TextAsset electronDataFile;
	public List<ElectronData> electronMap;
	// Use this for initialization
	void Start () 
	{
		electronMap = new List<ElectronData> ();
		ParsingData(electronDataFile);
		//printData ();
	}
	
	private void ParsingData(TextAsset electronDataFile)
	{
		string[][] grid = CsvParser2.Parse(electronDataFile.text);
		for (int i = 1; i < grid.Length; i++) {
			ElectronData row = new ElectronData ();
			row.SetElectronID (grid [i] [0]);
			row.SetElectronCount (System.Convert.ToInt32 (grid [i] [1]));
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [2]), 0, 0);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [3]), 0, 1);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [4]), 0, 2);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [5]), 1, 0);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [6]), 1, 2);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [7]), 2, 0);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [8]), 2, 1);
			row.SetElectronPosition (System.Convert.ToInt32 (grid [i] [9]), 2, 2);
			row.SetElectronPosition (-1, 1, 1);
			electronMap.Add (row);
		}
	}

	private void printData()
	{
		Debug.Log (electronMap.Count);
		foreach (ElectronData ed in electronMap) 
		{
			Debug.Log (ed.GetElectronID() + " " + ed.GetElectronCount());
			Debug.Log (ed.HaveElectron (0, 0) + " " + ed.HaveElectron (0, 1) + " " + ed.HaveElectron (0, 2));
			Debug.Log (ed.HaveElectron (1, 0) + " " + ed.HaveElectron (1, 1) + " " + ed.HaveElectron (1, 2));
			Debug.Log (ed.HaveElectron (2, 0) + " " + ed.HaveElectron (2, 1) + " " + ed.HaveElectron (2, 2));
			Debug.Log ("");
		}
	}
}
