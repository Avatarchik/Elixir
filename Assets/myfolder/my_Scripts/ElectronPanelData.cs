using UnityEngine;
using System.Collections;

public class ElectronPanelData : MonoBehaviour {

	private int posX;
	private int posY;

	public void SetPos(int x, int y)
	{
		posX = x;
		posY = y;
	}

	public int GetPosX()
	{
		return posX;
	}

	public int GetPosY()
	{
		return posY;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
