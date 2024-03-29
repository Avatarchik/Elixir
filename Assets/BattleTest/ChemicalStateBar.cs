﻿using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChemicalStateBar : MonoBehaviour {
    public RectTransform solidTransform;
    public RectTransform liquidTransform;
    public RectTransform gasTransform;
    public RectTransform arrowTransform;
    private float minXValue;
    private float maxXValue;
    private ChemicalStates currentCState;
    private int currentCStateValue;
    private int valSolid;
    private int valLiquid;
    private int valGas;
    private float arrowVal;
    private int barSolid;
    private int barLiquid;
    private int barGas;

    private Monster monsterPref;

    void Start()
    {
        monsterPref = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>().monsterList[GetComponent<MonsterIndex>().MonsterID];
        currentCState = monsterPref.currentChemicalState;
        currentCStateValue = monsterPref.currentChemicalStateValue;
        maxXValue = gasTransform.localPosition.x;
        minXValue = gasTransform.localPosition.x - gasTransform.rect.width;

        //temporary inputs
        valSolid = monsterPref.solidStateValue;
        valLiquid = monsterPref.liquidStateValue;
        valGas = monsterPref.gasStateValue;
        barSolid = valSolid;
        barLiquid = valSolid + valLiquid;
        barGas = valSolid + valLiquid + valGas;

        solidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barSolid), gasTransform.localPosition.y);
        liquidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barLiquid), gasTransform.localPosition.y);

        MoveArrow();//Initialize Arrow
    }

    void Update()
    {
        if(monsterPref.currentChemicalState != currentCState || monsterPref.currentChemicalStateValue != currentCStateValue)
        {
            currentCState = monsterPref.currentChemicalState;
            currentCStateValue = monsterPref.currentChemicalStateValue;
            MoveArrow();
        }
    }
    private void MoveArrow()
    {
        switch (currentCState)
        {
            case ChemicalStates.SOLID:
                Debug.Log("Solid");
                arrowVal = currentCStateValue;
                break;
            case ChemicalStates.LIQUID:
                arrowVal = valSolid + currentCStateValue;
                break;
            case ChemicalStates.GAS:
                arrowVal = valSolid + valLiquid + currentCStateValue;
                break;
            default:
                break;
        }
        arrowTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, maxXValue, arrowVal), arrowTransform.localPosition.y);
    }
    private float MapValues(float barWidth, float minXVal, float barState)
    {
        return (barWidth / barGas) * barState + minXVal;
    }
}
