using UnityEngine;
using System.Collections;

public class ElectronData {
	private string electronID = null;
	private int electronCount = 0;
	private int[,] electronPosition = new int[3,3];
	public void SetElectronID(string elecID)
	{
		electronID = elecID;
	}

	public void SetElectronCount(int elecCount)
	{
		electronCount = elecCount;
	}

	public void SetElectronPosition(int haveElectron, int posX, int posY)
	{
		electronPosition [posX, posY] = haveElectron;
	}



	public string GetElectronID()
	{
		return electronID;
	}

	public int GetElectronCount()
	{
		return electronCount;
	}

	public int HaveElectron(int posX, int posY)
	{
		if (posX == 1 && posY == 1)
			return -1;

		return electronPosition [posX, posY];
	}



	public ElectronData GetInstance()
	{
		return this;
	}
}
