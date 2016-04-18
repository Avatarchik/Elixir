using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{       

	}


	private void OnGUI () 
	{      
		int tSelectionGridIndex = 0;
		string[] selectionStringArray = new string[]{"Grid 1", "Grid 2", "Grid 3", "Grid 4"};
		tSelectionGridIndex = GUI.SelectionGrid (new Rect (10, 10, 100, 100), tSelectionGridIndex, selectionStringArray, 2);

		if ( true == GUI.changed )
		{            
			Debug.Log ("The toolbar was clicked. The index is " + tSelectionGridIndex + ".");
		}

	}    
	private void WindowFunction (int windowID) 
	{
		// Draw any Controls inside the window here
	}

}
