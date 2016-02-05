using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChooseChemistSkill : MonoBehaviour {
    ChoosingManager choosingManager;
    TurnBasedCombatStateMachine turnBasedCombatStateMachine;

    // Use this for initialization
    void Start () {
        choosingManager = GameObject.Find("GameManager").GetComponent<ChoosingManager>();
        turnBasedCombatStateMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();
    }

    public void BtnClicked(int index)
    {
        ChemistSkills chemSkill = ChemistSkills.Cool;
        switch (index)
        {
            case 0:
                chemSkill = ChemistSkills.Cool;
                break;
            case 1:
                chemSkill = ChemistSkills.Heat;
                break;
            case 2:
                chemSkill = ChemistSkills.Analyze;
                break;
        }

        choosingManager.AttackMode = AttackMode.Chemist;
        choosingManager.SelectedChemistSkill = chemSkill;
        StartCoroutine(SkillActivated(chemSkill));

    }

    IEnumerator SkillActivated(ChemistSkills chemSkill)
    {
        if (chemSkill == ChemistSkills.Analyze)
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
            GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill != chemSkill)
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
