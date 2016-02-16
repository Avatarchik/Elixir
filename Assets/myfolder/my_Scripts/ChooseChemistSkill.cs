using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChooseChemistSkill : MonoBehaviour {
    ChoosingManager choosingManager;
    TurnBasedCombatStateMachine turnBasedCombatStateMachine;

    private ChemistSkills chemSkill;
    private IEnumerator skillInUse;
    private IEnumerator waitForSelection;
    // Use this for initialization
    void Start () {
        choosingManager = GameObject.Find("GameManager").GetComponent<ChoosingManager>();
        turnBasedCombatStateMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();
    }

    public void BtnClicked(int index)
    {
        chemSkill = ChemistSkills.Cool;
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

        if (choosingManager.isSkillInUse)
        {
            Debug.Log("Already skill in use");
            if (chemSkill != choosingManager.SelectedChemistSkill || choosingManager.AttackMode != AttackMode.Chemist)
            {
                Debug.Log("Different Skill: Activate new skill");

                choosingManager.SelectedChemistSkill = chemSkill;
                choosingManager.AttackMode = AttackMode.Chemist;

                StopCurrentCoroutines();

                skillInUse = GetComponent<ChooseTargetByChemist>().SelectTarget(); //Load new coroutine
                StartCoroutine(skillInUse);
            }
            else
            {
                Debug.Log("Same Skill: Do nothing");
            }
        }
        else
        {
            Debug.Log("New Skill");
            choosingManager.isSkillInUse = true;
            choosingManager.AttackMode = AttackMode.Chemist;
            choosingManager.SelectedChemistSkill = chemSkill;
            skillInUse = GetComponent<ChooseTargetByChemist>().SelectTarget();
            StartCoroutine(skillInUse);
        }

    }

    public void StopCurrentCoroutines()
    {
        StopCoroutine(skillInUse);
        StopCoroutine(waitForSelection);
    }

    //IEnumerator SkillActivated(ChemistSkills chemSkill)
    //{
    //    if (chemSkill == ChemistSkills.Analyze)
    //    {
    //        Debug.Log("Use Analyze");
    //        yield return StartCoroutine(gameObject.GetComponent<AnalyzeMonster>().SelectTarget());
    //    }
    //    else
    //    {
    //        yield return StartCoroutine(gameObject.GetComponent<ChooseTargetByChemist>().SelectTarget());
    //    }

    //    Debug.Log("Before Coroutine Stopped");
    //    //If Attack method is changed during the procedure, dismiss all actions
    //    if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Chemist ||
    //        GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill != chemSkill)
    //    {
    //        Debug.Log("Coroutine Stopped(Chemist)");
    //    }
    //    else
    //    {
    //        //Increment counter
    //        //Check if all turns are exhausted
    //        //If exhausted, change the state of TurnBasedCombatStateMachine
    //        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().decrementTurn();

    //        if (GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted())
    //        {
    //            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
    //            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().resetTurn();
    //        }

    //    }



    //}
}
