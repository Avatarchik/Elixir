using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using UnityEngine.UI;

public class SkillChoice : MonoBehaviour {
    private ChoosingManager choosingManager;
    private TurnBasedCombatStateMachine TBSMachine;
    // Use this for initialization
    void Start () {
        choosingManager = GetComponent<ChoosingManager>();
        TBSMachine = GetComponent<TurnBasedCombatStateMachine>();
    }


    public void SkillChosen(int skillIndex)
    {
        Debug.Log("Index: " + skillIndex);
        choosingManager.AttackMode = AttackMode.Element;
        choosingManager.SelectedSkill = skillIndex;

        StartCoroutine(SkillActivate(skillIndex));
    }

    IEnumerator SkillActivate(int skillIndex)
    {
        Button skillBtn = GameObject.Find("SkillPanel").transform.GetChild(skillIndex).GetComponent<Button>();
        //Activate skill
        yield return StartCoroutine(GetComponent<SkillActivate>().SelectTarget(skillIndex));

        //Dismiss this Coroutine if player chooses another skill or chemist skill
        if (choosingManager.AttackMode != AttackMode.Element ||
                    choosingManager.SelectedSkill != skillIndex)
        {
            Debug.Log("Coroutine Stopped");
            yield return null;
        }
        else
        {
            //Increment counter
            //Check if all turns are exhausted
            //If exhausted, change the state of TurnBasedCombatStateMachine
            Debug.Log("Decrement Turn");
            TBSMachine.decrementTurn();

            if (TBSMachine.isTurnExhausted())
            {
                TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
                TBSMachine.resetTurn();
            }

            skillBtn.GetComponent<Button>().interactable = false;
        }
        yield return null;
    }
}
