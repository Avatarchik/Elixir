using UnityEngine;
using System.Collections;

[System.Serializable]
public class BondTableManager : MonoBehaviour {



	// Use this for initialization
	public GameObject ElectronPanel;
	private int i, j;
	public GameObject[,] objectMap = new GameObject[7, 8];

	void Start () {
		for (i = 0; i < 7; i++) 
		{
			for (j = 0; j < 8; j++)
			{
				CreateTable ();
			}
		}
		//objectMap [5, 5].transform.localPosition = new Vector3 ((100 * j) - 550, (100 * i) - 350, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateTable()
	{
		GameObject obj;
		obj = (GameObject)Instantiate (ElectronPanel);
		obj.transform.parent = GameObject.Find ("Canvas").transform;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = new Vector3((100 * j) - 550, (100 * i) - 350, 1);
		objectMap [i, j] = obj;
	}
}
