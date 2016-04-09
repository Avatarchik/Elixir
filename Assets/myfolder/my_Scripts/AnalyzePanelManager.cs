using UnityEngine;
using System.Collections;

public class AnalyzePanelManager : MonoBehaviour {
	public bool checkPress = false;
	public float timer;
	public GameObject AnalyzePanel;
	// Use this for initialization
	void Start () {
		Debug.Log("asdasdasd");
		AnalyzePanel.SetActive (true);
	}
	public void SetAnalyzePanel()
	{
		Debug.Log("asdasdasd");
		AnalyzePanel.SetActive (true);
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
