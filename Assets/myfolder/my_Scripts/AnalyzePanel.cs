using UnityEngine;
using System.Collections;

public class AnalyzePanel : MonoBehaviour {
	public GameObject analyzePanel;
	// Use this for initialization
	void Start () {
	
	}

	public void OpenAnalyzePanel(Monster analyzeMonster)
	{
		analyzePanel.SetActive (true);
	}
}
