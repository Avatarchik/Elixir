using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class EquippedElementWeakPointLoad : MonoBehaviour {
	public GameObject WeakPoint;
	public ChemicalStates currentWeakPoint;
	// Use this for initialization
	void Start () {

	}
	public void SetWeakPoint(List<Element> elementList, int id)
	{
		Element element = elementList[id];
		currentWeakPoint = element.weakPoint;

		switch (currentWeakPoint)
		{
		case ChemicalStates.GAS:
			WeakPoint.transform.Find ("WeakPointGas").gameObject.SetActive (true);
			WeakPoint.transform.Find ("WeakPointLiquid").gameObject.SetActive (false);
			WeakPoint.transform.Find ("WeakPointSolid").gameObject.SetActive (false);
			break;
		case ChemicalStates.LIQUID:
			WeakPoint.transform.Find ("WeakPointGas").gameObject.SetActive (false);
			WeakPoint.transform.Find ("WeakPointLiquid").gameObject.SetActive (true);
			WeakPoint.transform.Find ("WeakPointSolid").gameObject.SetActive (false);
			break;
		case ChemicalStates.SOLID:
			WeakPoint.transform.Find ("WeakPointGas").gameObject.SetActive (false);
			WeakPoint.transform.Find ("WeakPointLiquid").gameObject.SetActive (false);
			WeakPoint.transform.Find ("WeakPointSolid").gameObject.SetActive (true);
			break;
		}
	}
	// Update is called once per frame
	/*void Update () {
	
	}*/
}
