using UnityEngine;
using System.Collections;

public class AnalyzePanelManager : MonoBehaviour {
	public bool checkPress = false;
	public float timer;
	// Use this for initialization
	void Start () {
	
	}
	public void SetAnalyzePanel()
	{
		Debug.Log("asdasdasd");
	}
	public void CheckLongPress(int index)
	{
		checkPress = true;
		timer = 1.0f;
		//elementIndex = index;
	}
	// Update is called once per frame
	/*
	void Update () {
	
	}*/
}
