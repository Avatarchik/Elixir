using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class PlayerChemicalStateBar : MonoBehaviour {
    public RectTransform solidTransform;
    public RectTransform liquidTransform;
    public RectTransform gasTransform;
    public RectTransform arrowTransform;
    private float minXValue;
    private float maxXValue;
    public ChemicalStates currentCState;
    public int currentCStateValue;
    public int valSolid;
    public int valLiquid;
    public int valGas;
    public float arrowVal;
    public int barSolid;
    public int barLiquid;
    public int barGas;
    public int currentEquipped;
    private PlayerPrefs playerPrefs;

    void Start()
    {
        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();
        Initialize();
    }

    void Initialize()
    {
        baseCharacter player = playerPrefs.player;
        currentEquipped = playerPrefs.currentEquipElementIndex;
        currentCState = player.currentChemicalState;
        currentCStateValue = player.currentChemicalStateValue;
        maxXValue = gasTransform.localPosition.x;
        minXValue = gasTransform.localPosition.x - gasTransform.rect.width;

        //temporary inputs
        valSolid = player.solidStateValue;
        valLiquid = player.liquidStateValue;
        valGas = player.gasStateValue;
        barSolid = valSolid;
        barLiquid = valSolid + valLiquid;
        barGas = valSolid + valLiquid + valGas;

        solidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barSolid), gasTransform.localPosition.y);
        liquidTransform.localPosition = new Vector2(MapValues(gasTransform.rect.width, minXValue, barLiquid), gasTransform.localPosition.y);

        MoveArrow();//Initialize Arrow
    }

    void Update()
    {
        if(currentEquipped != playerPrefs.currentEquipElementIndex)
        {
            Initialize();
        }
        else if (playerPrefs.player.currentChemicalState != currentCState || playerPrefs.player.currentChemicalStateValue != currentCStateValue)
        {
            currentCState = playerPrefs.player.currentChemicalState;
            currentCStateValue = playerPrefs.player.currentChemicalStateValue;
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
