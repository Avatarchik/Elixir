using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class ThermoBar : MonoBehaviour {
    public GameObject ChemicalState;
    public RectTransform solidTransform;
    public RectTransform liquidTransform;
    public RectTransform gasTransform;
    public RectTransform pointerTransform;
    private float minXValue;
    private float maxXValue;
    public int valSolid;
    public int valLiquid;
    public int valGas;

	//2016-03-19 Changed int -> float
    public float pointerVal;
	//
    public int barSolid;
    public int barLiquid;
    public int barGas;
    public ChemicalStates currentCState;
    
	//2016-03-19 Changed int -> float
	public float currentCStateValue;

    // Use this for initialization
    void Start () {
        //maxXValue = gasTransform.localPosition.x;
        //minXValue = gasTransform.localPosition.x - gasTransform.rect.width;

    }
	public void GetData(List<Element> elementList, int id)
    {
        maxXValue = gasTransform.localPosition.x;
        minXValue = gasTransform.localPosition.x - gasTransform.rect.width;

        Element element = elementList[id];
        currentCState = element.characterRoomTempState;
        currentCStateValue = element.roomTempPos;
        valSolid = element.solidGauge;
        valLiquid = element.liquidGauge;
        valGas = element.gasGauge;
        barSolid = valSolid;
        barLiquid = valSolid + valLiquid;
        barGas = valSolid + valLiquid + valGas;
        solidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barSolid), gasTransform.localPosition.y);
        liquidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barLiquid), gasTransform.localPosition.y);

        MovePointer();

        switch (currentCState)
        {
            case ChemicalStates.GAS:
                ChemicalState.transform.Find("Gas").gameObject.SetActive(true);
                ChemicalState.transform.Find("Liquid").gameObject.SetActive(false);
                ChemicalState.transform.Find("Solid").gameObject.SetActive(false);
                break;
            case ChemicalStates.LIQUID:
                ChemicalState.transform.Find("Gas").gameObject.SetActive(false);
                ChemicalState.transform.Find("Liquid").gameObject.SetActive(true);
                ChemicalState.transform.Find("Solid").gameObject.SetActive(false);
                break;
            case ChemicalStates.SOLID:
                ChemicalState.transform.Find("Gas").gameObject.SetActive(false);
                ChemicalState.transform.Find("Liquid").gameObject.SetActive(false);
                ChemicalState.transform.Find("Solid").gameObject.SetActive(true);
                break;
        }
    }

    private void MovePointer()
    {
        switch (currentCState)
        {
            case ChemicalStates.SOLID:
                pointerVal = currentCStateValue;
                break;
            case ChemicalStates.LIQUID:
                pointerVal = valSolid + currentCStateValue;
                break;
            case ChemicalStates.GAS:
                pointerVal = valSolid + valLiquid + currentCStateValue;
                break;
            default:
                break;
        }
        pointerTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, maxXValue, pointerVal), pointerTransform.localPosition.y);
    }

    private float MapValues(float barWidth, float minXVal, float barState)
    {
        Debug.Log((barWidth / barGas) * barState + minXVal);
        return (barWidth / barGas) * barState + minXVal;
    }
}
