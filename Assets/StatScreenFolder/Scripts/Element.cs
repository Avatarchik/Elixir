using UnityEngine;
using System.Collections;
using EnumsAndClasses;
[System.Serializable]
public class Element{
    public string id;
    public string extName;
    public string name;
    public string chemicalSeries;
	public int elementNumber;
	public int properLevel;
    public string desription;
	public float meltingPoint;
	public float boilingPoint;
    public ChemicalStates characterRoomTempState;
    public int solidGauge;
    public int liquidGauge;
    public int gasGauge;
    public float roomTempPos;
    public string elementCard1;
    public string elementCard2;
    public string elementCard3;
}
