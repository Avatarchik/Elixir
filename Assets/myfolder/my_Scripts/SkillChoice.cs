using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using UnityEngine.UI;

public class SkillChoice : MonoBehaviour {
    private ChoosingManager choosingManager;
    private TurnBasedCombatStateMachine TBSMachine;

    private int skillIndex;
    public IEnumerator skillInUse;
    public IEnumerator waitForSelection;
    // Use this for initialization
    void Start () {
        choosingManager = GetComponent<ChoosingManager>();
        TBSMachine = GetComponent<TurnBasedCombatStateMachine>();
        choosingManager.isSkillInUse = false;
    }

    public void SkillChosen(int index)
    {
        skillIndex = index;
        Debug.Log("Index: " + skillIndex);

        if (choosingManager.isSkillInUse)
        {
            Debug.Log("Already skill in use");
            if (skillIndex != choosingManager.SelectedSkill || choosingManager.AttackMode != AttackMode.Element)
            {
                Debug.Log("Different Skill: Activate new skill");
                choosingManager.SelectedSkill = skillIndex;
                choosingManager.AttackMode = AttackMode.Element;
                StopCurrentCoroutines();

                skillInUse = GetComponent<SkillActivate>().SelectTarget(skillIndex); //Load new coroutine
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
            choosingManager.SelectedSkill = skillIndex;
            choosingManager.AttackMode = AttackMode.Element;
            skillInUse = GetComponent<SkillActivate>().SelectTarget(skillIndex);
            StartCoroutine(skillInUse);
        }
    }
    
    public void StopCurrentCoroutines()
    {
        StopCoroutine(skillInUse);
        StopCoroutine(waitForSelection);
    }
}
