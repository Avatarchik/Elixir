using UnityEngine;
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
	
    void Start()
    {
        currentCState = this.GetComponent<Monster>().ChemicalState;
        currentCStateValue = this.GetComponent<Monster>().ChemicalStateValue;
        maxXValue = gasTransform.localPosition.x;
        minXValue = gasTransform.localPosition.x - gasTransform.rect.width;

        //temporary inputs
        valSolid = 1;
        valLiquid = 2;
        valGas = 3;
        barSolid = valSolid;
        barLiquid = valSolid + valLiquid;
        barGas = valSolid + valLiquid + valGas;

        solidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barSolid), gasTransform.localPosition.y);
        liquidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barLiquid), gasTransform.localPosition.y);

        switch (currentCState)
        {
            case ChemicalStates.SOLID:
                Debug.Log("Solid");
                arrowVal = currentCStateValue - 0.5f;
                break;
            case ChemicalStates.LIQUID:
                arrowVal = valSolid + currentCStateValue - 0.5f;
                break;
            case ChemicalStates.GAS:
                arrowVal = valSolid + valLiquid + currentCStateValue - 0.5f;
                break;
            default:
                break;
        }

        arrowTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, arrowVal), gasTransform.localPosition.y);
    }

    void Update()
    {
        
    }

    private float MapValues(float barWidth, float minXVal, float barState)
    {
        return (barWidth / barGas) * barState + minXVal;
    }
}
