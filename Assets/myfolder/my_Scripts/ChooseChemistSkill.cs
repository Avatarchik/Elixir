using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChooseChemistSkill : MonoBehaviour {
    ChoosingManager choosingManager;
    TurnBasedCombatStateMachine turnBasedCombatStateMachine;
    ChemistSkills chemistSkill;

    // Use this for initialization
    void Start () {
        choosingManager = GameObject.Find("GameManager").GetComponent<ChoosingManager>();
        turnBasedCombatStateMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();
        switch (this.gameObject.name)
        {
            case "CoolIcon":
                chemistSkill = ChemistSkills.Cool;
                break;
            case "HeatIcon":
                chemistSkill = ChemistSkills.Heat;
                break;
            case "AnalyzeIcon":
                chemistSkill = ChemistSkills.Analyze;
                break;
        }
    }

    void OnMouseDown()
    {
        choosingManager.AttackMode = AttackMode.Chemist;
        choosingManager.SelectedCard = null;
        choosingManager.SelectedChemistSkill = chemistSkill;
        StartCoroutine(cardActivated());
    }

    IEnumerator cardActivated()
    {
        if (chemistSkill == ChemistSkills.Analyze)
        {
            Debug.Log("Use Analyze");
            yield return StartCoroutine(gameObject.GetComponent<AnalyzeMonster>().SelectTarget());
        }
        else
        {
            yield return StartCoroutine(gameObject.GetComponent<ChooseTargetByChemist>().SelectTarget());
        }

        Debug.Log("Before Coroutine Stopped");
        //If Attack method is changed during the procedure, dismiss all actions
        if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Chemist ||
            GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill != chemistSkill)
        {
            Debug.Log("Coroutine Stopped(Chemist)");
        }
        else
        {
            //Increment counter
            //Check if all turns are exhausted
            //If exhausted, change the state of TurnBasedCombatStateMachine
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().decrementTurn();

            if (GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted())
            {
                GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
                GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().resetTurn();
            }

        }



    }
}
