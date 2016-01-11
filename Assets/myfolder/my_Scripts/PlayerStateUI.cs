using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class PlayerStateUI : MonoBehaviour {
    ChemicalStates currentState;
    public GameObject Solid;
    public GameObject Liquid;
    public GameObject Gas;
    // Use this for initialization
    void Start()
    {
        currentState = gameObject.GetComponent<BaseCharacter>().currentChemicalState;
        changeState();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BaseCharacter>().currentChemicalState != currentState)
        {
            currentState = gameObject.GetComponent<BaseCharacter>().currentChemicalState;
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
