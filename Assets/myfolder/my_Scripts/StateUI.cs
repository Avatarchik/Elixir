using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class StateUI : MonoBehaviour {
    ChemicalStates currentState;
    public GameObject Solid;
    public GameObject Liquid;
    public GameObject Gas;

    private Monster monsterPref;
    // Use this for initialization
    void Start () {
        monsterPref = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>().monsterList[GetComponent<MonsterIndex>().MonsterID];
        currentState = monsterPref.currentChemicalState;
        changeState();
    }
	
	// Update is called once per frame
	void Update () {
	if(monsterPref.currentChemicalState != currentState)
        {
            currentState = monsterPref.currentChemicalState;
            changeState();
        }
	}
    private void changeState()
    {
        switch (currentState)
        {
            case ChemicalStates.SOLID:
                Solid.SetActive(true);
                Liquid.SetActive(false);
                Gas.SetActive(false);
                break;
            case ChemicalStates.LIQUID:
                Solid.SetActive(false);
                Liquid.SetActive(true);
                Gas.SetActive(false);
                break;
            case ChemicalStates.GAS:
                Solid.SetActive(false);
                Liquid.SetActive(false);
                Gas.SetActive(true);
                break;
        }
    }
}
