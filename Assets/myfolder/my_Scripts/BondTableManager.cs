using UnityEngine;
using System.Collections;

[System.Serializable]
public class BondTableManager : MonoBehaviour {



	// Use this for initialization
	public GameObject ElectronPanel;
	private int tableRow = 7;
	private int tableCol = 8;
	public GameObject[] objectMap;

	void Awake () {
		objectMap = new GameObject[(tableRow*tableCol)];
		for (int i = 0; i < tableRow; i++) 
		{
			for (int j = 0; j < tableCol; j++)
			{
				CreateTable (i, j);
			}
		}
		//objectMap [5, 5].transform.localPosition = new Vector3 ((100 * j) - 550, (100 * i) - 350, 1);
	}

	// Update is called once per frame
	void Update () {
		
	}
	void SetTableSize(int row, int col)
	{
		tableRow = row;
		tableCol = col;
	}
	void CreateTable(int i, int j)
	{
		GameObject obj;
		obj = (GameObject)Instantiate (ElectronPanel);
		obj.transform.SetParent (GameObject.Find("BondPanel").transform);
		obj.transform.localScale = Vector3.one;
		//obj.transform.localPosition = new Vector3((100 * j) - 550, (100 * i) - 350, 1);
		obj.transform.localPosition = new Vector3((100 * j) - 350, (100 * i) - 300, 1);
		objectMap [(i * tableCol) + j] = obj;
		objectMap [(i * tableCol) + j].GetComponent<ElectronPanelData> ().SetPos(i, j);
	}
}
