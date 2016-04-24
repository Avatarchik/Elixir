using UnityEngine;
using System.Collections;

public class BondPanelCtrl : MonoBehaviour {
	private RaycastHit hit;
	private BoxCollider2D[] coll = null;
	private Transform [] trArray = null;
	private int clickedIndex;
	//private [,] objectMap = null;
	// Use this for initialization
	void Start ()
	{
		int i = 0;
		int count = this.transform.GetChild(0).transform.childCount;
		coll = new BoxCollider2D[count];
		trArray = new Transform[count];
		Debug.Log(count);

		while (i < count) 
		{
			trArray[i] = this.transform.GetChild(0).transform.GetChild(i);
			coll [i] = trArray [i].GetComponent<BoxCollider2D> ();

			Debug.Log(trArray[i].GetComponent<ElectronPanelData>().GetPosX() + "  " + trArray[i].GetComponent<ElectronPanelData>().GetPosY());
			i++;
		}
	}

	// Update is called once per frame
	void Update ()
	{       
		if (Input.GetMouseButtonDown (0)) 
		{
			if (Hit ()) 
			{
				Debug.Log (trArray[clickedIndex].GetComponent<ElectronPanelData>().GetPosX() + " " + trArray[clickedIndex].GetComponent<ElectronPanelData>().GetPosY());
			}
		}
	}

	private bool Hit()
	{
		int i = 0;
		Debug.Log ("MousePos : " + Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z + " " + coll.Length);
		Vector2 clickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		while (i < coll.Length)
		{

			if (coll [i].OverlapPoint(clickPos)) 
			{
				clickedIndex = i;
				return true;
			}
			i++;
		}
		return false;
	}
}
