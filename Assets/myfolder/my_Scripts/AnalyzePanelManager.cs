using UnityEngine;
using System.Collections;

public class AnalyzePanelManager : MonoBehaviour {
	public bool checkPress = false;
	public float timer;
	public GameObject AnalyzePanel;
	// Use this for initialization
	void Start () {

	}
	public void SetAnalyzePanel()
	{
		Debug.Log("asdasdasd");
		AnalyzePanel.SetActive (true);
	}

	void Update()
	{
		if (checkPress)
		{
			timer -= Time.time;
			if(timer <= 0)
				checkPress = false;
		}
	}

	public void CheckLongPress(int index)
	{
		checkPress = true;
		timer = 2.0f;
	}

}
