using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System;

public class UseSkill : MonoBehaviour {
    //private ChoosingManager choosingManager;
    private TurnBasedCombatStateMachine TBSMachine;

    //----SkillChosen Variables
    public bool isSkillInUse;
    public int skillIndex;
    public IEnumerator skillInUse;
    public IEnumerator waitForSelection;

    //----Shared Variables
    BaseCharacter player;
    GameObject[] selectedEnemy = new GameObject[4];
    GameObject selectedAlly;
    //----ChemistSkill Variables
    private bool isTargetEnemy;
    //----ElementSkill Variables
    int currentEquipElementIndex;
    int currentSkillIndex;
    baseCard currentSelectedCard = new baseCard();
    bool attackedCritical = false;

    // Use this for initialization
    void Start () {
        //choosingManager = GetComponent<ChoosingManager>();
        TBSMachine = GetComponent<TurnBasedCombatStateMachine>();
        player = GetComponent<PlayerPrefs>().player;
        isSkillInUse = false;
    }

    public void SkillChosen(int index)
    {
        //skillIndex = index;
        Debug.Log("Index: " + skillIndex);

        if (isSkillInUse)
        {
            Debug.Log("Already skill in use");
            if (skillIndex != index)
            {
                Debug.Log("Different Skill: Activate new skill");

                skillIndex = index;
                StopCurrentCoroutines();

                if (skillIndex >= 0 && skillIndex <= 2)
                {
                    skillInUse = ElementSkill();
                }
                else
                {
                    skillInUse = ChemistSkill();
                }
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
            isSkillInUse = true;
            skillIndex = index;
            if (skillIndex >=0 && skillIndex <= 2)
            {
                skillInUse = ElementSkill();
            }
            else
            {
                skillInUse = ChemistSkill();
            }
            Debug.Log(skillIndex);
            StartCoroutine(skillInUse);
        }
    }

    IEnumerator ElementSkill()
    {
        Debug.Log("Use Element Skill");

        currentEquipElementIndex = GameObject.Find("GameManager").GetComponent<PlayerPrefs>().currentEquipElementIndex;
        currentSkillIndex = skillIndex;
        currentSelectedCard = GameObject.Find("GameManager").GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][currentSkillIndex];
        string targetType = currentSelectedCard.Card_Target; // Distinguish number of attacks
        string targetRange = currentSelectedCard.Card_Range;

        //----------
        Highlight(targetType, targetRange);
        //----------
        if (targetRange == "Wide")//When target is Wide
        {
            Debug.Log("ElementSkill Wide");
            waitForSelection = WaitForSelection(targetType);
            yield return StartCoroutine(waitForSelection);
            selectedEnemy = GameObject.FindGameObjectsWithTag("Monster"); //Add all monsters in the selectedEnemy array
        }
        else
        {
            Debug.Log("ElementSkill Else");
            waitForSelection = WaitForSelection(targetType);
            yield return StartCoroutine(waitForSelection);
        }

        //---------
        Debug.Log("Unhighlight");
        UnHighlight();

        //---------
        if (targetType == "Ally")
        {
            ElementSkillToAlly();
        }
        else if (targetType == "Enemy")
        {
            if(targetRange == "Single")
            {
                ElementSkillToEnemy(1);
            }else if(targetRange == "Wide")
            {
                ElementSkillToEnemy(4);
            }
        }
        else if (targetType == "All")
        {
            ElementSkillToAlly();
            if (targetRange == "Single")
            {
                ElementSkillToEnemy(1);
            }
            else if (targetRange == "Wide")
            {
                ElementSkillToEnemy(4);
            }
        }

        //----------
        if (attackedCritical)
        {
            
            Debug.Log("Turn incremented");
            TBSMachine.incrementTurn();
            GameObject.Find("AddTurn").transform.Find("1More").gameObject.SetActive(true);
            attackedCritical = false;
        }

        //----------
        Debug.Log("Decrement Turn");
        TBSMachine.decrementTurn();

        if (TBSMachine.isTurnExhausted())
        {
            TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            TBSMachine.resetTurn();
        }

        isSkillInUse = false;
        yield return null;
    }

    IEnumerator ChemistSkill()
    {
        Debug.Log("Use Chemist Skill");

        //----------
        Highlight("All", "Single");

        //----------
        waitForSelection = WaitForSelection("All");
        yield return StartCoroutine(waitForSelection);

        //----------
        UnHighlight();

        //Activate Skill
        if (isTargetEnemy)
        {
            ChemistSkillToEnemy();
        }
        else
        {
            ChemistSkillToAlly();
        }

        Debug.Log("Decrement Turn");
        TBSMachine.decrementTurn();
        
        if (TBSMachine.isTurnExhausted())
        {
            TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            TBSMachine.resetTurn();
        }


        Debug.Log("End Skill");
        isSkillInUse = false;
        yield return null;
    }

    void Highlight(string targetType, string targetRange)
    {
        Debug.Log("Highlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject Ally = GameObject.FindGameObjectWithTag("Ally");

        //Reset first
        Ally.transform.Find("selectable").gameObject.SetActive(false);
        Ally.transform.Find("selected").gameObject.SetActive(false);
        foreach (GameObject Monster in Monsters)
        {
            Monster.transform.Find("selectable").gameObject.SetActive(false);
            Monster.transform.Find("selected").gameObject.SetActive(false);
        }
        //Highlight
        if (targetType == "Ally")//Skill targets Ally(ex: heal)
        {
            Ally.transform.Find("selectable").gameObject.SetActive(true);
        }
        else if (targetType == "Enemy" && targetRange == "Single")// Skill is Enemy Single attack
        {
            foreach (GameObject Monster in Monsters)
            {
                Monster.transform.Find("selectable").gameObject.SetActive(true);
            }
        }
        else if (targetType == "Enemy" && targetRange == "Wide")// Skill is Enemy Wide attack
        {
            foreach (GameObject Monster in Monsters)
            {
                Monster.transform.Find("selected").gameObject.SetActive(true);
            }
        }
        else if (targetType == "All" && targetRange == "Single")
        {
            Ally.transform.Find("selectable").gameObject.SetActive(true);
            foreach (GameObject Monster in Monsters)
            {
                Monster.transform.Find("selectable").gameObject.SetActive(true);
            }
        }
        else if (targetType == "All" && targetRange == "Wide")
        {
            Ally.transform.Find("selected").gameObject.SetActive(true);
            foreach (GameObject Monster in Monsters)
            {
                Monster.transform.Find("selected").gameObject.SetActive(true);
            }
        }
    }

    void UnHighlight()
    {
        GameObject Ally = GameObject.FindGameObjectWithTag("Ally");
        Ally.transform.Find("selectable").gameObject.SetActive(false);
        Ally.transform.Find("selected").gameObject.SetActive(false);

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
        foreach (GameObject monster in monsters)
        {
            monster.transform.Find("selectable").gameObject.SetActive(false);
            monster.transform.Find("selected").gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForSelection(string targetType)
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse Click");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if(hit.collider != null && 
                    targetType == "Ally" &&
                    hit.collider.gameObject.tag == "Ally")
                {
                    Debug.Log("Ally selected");
                    //selectedAlly = hit.collider.gameObject;
                    //selectedAlly.transform.Find("selectable").gameObject.SetActive(false);
                    //selectedAlly.transform.Find("selected").gameObject.SetActive(true);
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "Enemy" &&
                    hit.collider.gameObject.tag == "Monster")
                {
                    Debug.Log("Enemy selected");
                    //selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    //selectedEnemy[countArray].transform.Find("selectable").gameObject.SetActive(false);
                    //selectedEnemy[countArray].transform.Find("selected").gameObject.SetActive(true);
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "All" &&
                    hit.collider.gameObject.tag == "Ally")
                {
                    Debug.Log("Ally selected (All)");
                    //selectedAlly = hit.collider.gameObject;
                    //selectedAlly.transform.Find("selectable").gameObject.SetActive(false);
                    //selectedAlly.transform.Find("selected").gameObject.SetActive(true);
                    isTargetEnemy = false;
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "All" &&
                    hit.collider.gameObject.tag == "Monster")
                {
                    Debug.Log("Enemy selected (All)");
                    //selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    //selectedEnemy[countArray].transform.Find("selectable").gameObject.SetActive(false);
                    //selectedEnemy[countArray].transform.Find("selected").gameObject.SetActive(true);
                    isTargetEnemy = true;
                    yield break;
                }
            }
            yield return null;
        }
    }

    void ElementSkillToEnemy(int countArray)
    {
        Debug.Log("ElementSkillToEnemy");
        GameObject Ally = GameObject.Find("Player(Clone)");
        ChemicalStates criticalTarget = currentSelectedCard.Card_CriticalTarget;
        System.Random rand = new System.Random();
        double criticalRate;
        double stunRate;

        for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
        {
            if (selectedEnemy[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
            {
                criticalRate = 1.5f;
                attackedCritical = true;
            }
            else
            {
                criticalRate = 1f;
            }

            //Normal damage
            if (currentSelectedCard.Card_AttackDamage > 0)
            {
                selectedEnemy[i].GetComponent<Monster>().SetDamage((int)(currentSelectedCard.Card_AttackDamage * criticalRate));
            }
            //DotDamage
            if (currentSelectedCard.Card_DebuffName == "DoteDamage")
            {
                int DotDamageTurn = currentSelectedCard.Card_DotDamageTurn;
                int DotDamage = currentSelectedCard.Card_DotDamage;
                Debuff debuff = new Debuff(DebuffName.DoteDamage, DotDamageTurn, DotDamage);
                selectedEnemy[i].GetComponent<Monster>().SetDamage(DotDamage);//Inflict damage immediately in this turn                                                                              
                selectedEnemy[i].GetComponent<Monster>().AddDotDamage(debuff);//Add debuff to monster

            }
            //Stun
            if (currentSelectedCard.Card_DebuffName == "Stun")
            {
                stunRate = currentSelectedCard.Card_DebuffRate;
                if (selectedEnemy[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
                {
                    stunRate += 10f;
                }
                int chance = rand.Next(1, 101);
                //int chance = 20;
                Debug.Log("Stun Rate: " + stunRate + ", chance: " + chance);
                if (chance <= stunRate)
                {
                    Debug.Log("Stun Success");
                    Debuff debuff = new Debuff(DebuffName.Stun, currentSelectedCard.Card_DebuffTurn);
                    selectedEnemy[i].GetComponent<Monster>().AddStun(debuff);
                }
            }


        }
    }

    void ElementSkillToAlly()
    {
        Debug.Log("ElementSkillToAlly");

        float criticalRate;
        ChemicalStates criticalTarget = currentSelectedCard.Card_CriticalTarget;

        if (player.currentChemicalState == criticalTarget)
        {
            criticalRate = 1.5f;
            attackedCritical = true;
        }
        else
        {
            criticalRate = 1f;
        }

        if (currentSelectedCard.Card_BuffName == "Dodge")
        {
            Buff buff = new Buff(BuffName.Dodge, currentSelectedCard.Card_BuffTurn - 1);
            player.dodgeRate = (int)currentSelectedCard.Card_BuffRate;
            player.AddBuff(buff);
        }
        if (currentSelectedCard.Card_Heal > 0)
        {
            player.SetHeal((int)(currentSelectedCard.Card_Heal * criticalRate));
        }
    }

    void ChemistSkillToEnemy()
    {
        Debug.Log("ChemistSkillToEnemy");
        
        //Increase or Decrease enemy ChemicalStateValue
        if (skillIndex == 3)
        {
            selectedEnemy[0].GetComponent<Monster>().DecrementCSVal();
        }
        else if (skillIndex == 4)
        {
            selectedEnemy[0].GetComponent<Monster>().IncrementCSVal();
        }

    }
    void ChemistSkillToAlly()
    {
        Debug.Log("ChemistSkillToAlly");

        //Increase or Decrease enemy ChemicalStateValue
        if (skillIndex == 3)
        {
            player.DecrementCSVal();
        }
        else if (skillIndex == 4)
        {
            player.IncrementCSVal();
        }
    }


    public void ResetVariables()
    {
        Array.Clear(selectedEnemy, 0, selectedEnemy.Length);
        selectedAlly = null;
    //----ChemistSkill Variables
        isTargetEnemy = false;
    //----ElementSkill Variables
        currentSkillIndex = skillIndex;
        currentSelectedCard = null;
        attackedCritical = false;
}

    public void StopCurrentCoroutines()
    {
        Debug.Log("End SkillInUse");
        StopCoroutine(skillInUse);
        Debug.Log("End WaitForSelection");
        StopCoroutine(waitForSelection);
    }
}
