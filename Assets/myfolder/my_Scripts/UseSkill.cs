﻿using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UseSkill : MonoBehaviour {
    //private ChoosingManager choosingManager;
    private TurnBasedCombatStateMachine TBSMachine;

    //----SkillChosen Variables
    public bool isSkillInUse;
    public int skillIndex;
    public IEnumerator skillInUse;
    public IEnumerator waitForSelection;

    //----Shared Variables
    baseCharacter player;
    GameObject selectedEnemy;
    List<GameObject> enemyList = new List<GameObject>();
    GameObject selectedAlly;
    //----ChemistSkill Variables
    private bool isTargetEnemy;
    //----ElementSkill Variables
    int currentEquipElementIndex;
    int currentSkillIndex;
    baseSkill currentSelectedSkill = new baseSkill();
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
        attackedCritical = false;
        currentEquipElementIndex = GameObject.Find("GameManager").GetComponent<PlayerPrefs>().currentEquipElementIndex;
        currentSkillIndex = skillIndex;
        currentSelectedSkill = GameObject.Find("GameManager").GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][currentSkillIndex];
        string targetType = currentSelectedSkill.Skill_Target; // Distinguish number of attacks
        string targetRange = currentSelectedSkill.Skill_Range;

        Debug.Log("TargetType: " + targetType + " TargetRange: " + targetRange);

        //----------
        Highlight(targetType, targetRange);
        //----------
        Debug.Log("ElementSkill Wide");
        waitForSelection = WaitForSelection(targetType);
        yield return StartCoroutine(waitForSelection);

        //---------
        Debug.Log("Unhighlight");
        UnHighlight();

        //---------
        if (targetType == "Player")
        {
            ElementSkillToAlly();
        }
        else if (targetType == "Monster")
        {
            if(targetRange == "Single")
            {
                ElementSkillToEnemy(1);
            }else if(targetRange == "Wide")
            {
                ElementSkillToEnemy(enemyList.Count);
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
                ElementSkillToEnemy(enemyList.Count);
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

            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
            {
                monster.GetComponent<Monster>().guarded = false;
                monster.transform.Find("guardIcon").gameObject.SetActive(false);
            }
        }

        //---------
        ResetVariables();
        Button skillBtn = GameObject.Find("SkillPanel").transform.GetChild(skillIndex).GetComponent<Button>();
        skillBtn.GetComponent<Button>().interactable = false;
        GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();
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

            foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
            {
                monster.GetComponent<Monster>().guarded = false;
                monster.transform.Find("guardIcon").gameObject.SetActive(false);
            }
        }


        Debug.Log("End Skill");
        ResetVariables();
        GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();
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
        if (targetType == "Player")//Skill targets Ally(ex: heal)
        {
            Ally.transform.Find("selectable").gameObject.SetActive(true);
        }
        else if (targetType == "Monster" && targetRange == "Single")// Skill is Enemy Single attack
        {
            foreach (GameObject Monster in Monsters)
            {
                Monster.transform.Find("selectable").gameObject.SetActive(true);
            }
        }
        else if (targetType == "Monster" && targetRange == "Wide")// Skill is Enemy Wide attack
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
                    targetType == "Player" &&
                    hit.collider.gameObject.tag == "Ally")
                {
                    Debug.Log("Ally selected");
                    selectedAlly = hit.collider.gameObject;
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "Monster" &&
                    hit.collider.gameObject.tag == "Monster")
                {
                    Debug.Log("Enemy selected");
                    selectedEnemy = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    enemyList.Add(selectedEnemy);
                    foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
                    {
                        if (monster != selectedEnemy)
                        {
                            enemyList.Add(monster);
                        }
                    }
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "All" &&
                    hit.collider.gameObject.tag == "Ally")
                {
                    Debug.Log("Ally selected (All)");
                    selectedAlly = hit.collider.gameObject;
                    foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
                    {
                        enemyList.Add(monster);
                    }
                    isTargetEnemy = false;
                    yield break;
                }
                else if (hit.collider != null &&
                    targetType == "All" &&
                    hit.collider.gameObject.tag == "Monster")
                {
                    Debug.Log("Enemy selected (All)");
                    selectedAlly = GameObject.FindGameObjectWithTag("Ally");
                    selectedEnemy = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    enemyList.Add(selectedEnemy);
                    foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
                    {
                        if (monster != selectedEnemy)
                        {
                            enemyList.Add(monster);
                        }
                    }
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
        ChemicalStates criticalTarget = GetComponent<PlayerPrefs>().currentEquipElement.characterRoomTempState;
        System.Random rand = new System.Random();
        float criticalRate;
        float stunRate;
        float silentRate;
        float blindRate;

        for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
        {
            if (enemyList[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
            {
                criticalRate = 1.5f;
                
                if (!enemyList[i].GetComponent<Monster>().guarded)
                {
                    attackedCritical = true;
                    enemyList[i].GetComponent<Monster>().guarded = true;
                    enemyList[i].transform.Find("guardIcon").gameObject.SetActive(true);
                }
            }
            else
            {
                criticalRate = 1f;
            }

            //Normal damage
            if (currentSelectedSkill.Skill_AttackDamage > 0)
            {
                Debug.Log("Player AD: " + player.AttackDamage + " 계수: " + currentSelectedSkill.Skill_AttackDamage);
                enemyList[i].GetComponent<Monster>().SetDamage((int)((player.AttackDamage * currentSelectedSkill.Skill_AttackDamage /100) * criticalRate));
            }

            //DotDamage
            if (currentSelectedSkill.Skill_DotDamage > 0)
            {
                int DotDamageTurn = currentSelectedSkill.Skill_DotDamageTurn;
                float DotDamage = currentSelectedSkill.Skill_DotDamage;
                Debuff debuff = new Debuff(DebuffName.DoteDamage, DotDamageTurn, (int)(player.AttackDamage * DotDamage /100));
                enemyList[i].GetComponent<Monster>().SetDamage((int)(player.AttackDamage * DotDamage / 100));//Inflict damage immediately in this turn                                                                              
                enemyList[i].GetComponent<Monster>().AddDotDamage(debuff);//Add debuff to monster
            }

            //Stun
            if (currentSelectedSkill.Skill_DebuffName == "Stun")
            {
                stunRate = currentSelectedSkill.Skill_DebuffRate;
                if (enemyList[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
                {
                    stunRate += 10f;
                }
                int chance = rand.Next(1, 101);
                //int chance = 20;
                Debug.Log("Stun Rate: " + stunRate + ", chance: " + chance);
                if (chance <= stunRate)
                {
                    Debug.Log("Stun Success");
                    Debuff debuff = new Debuff(DebuffName.Stun, currentSelectedSkill.Skill_DebuffTurn);
                    enemyList[i].GetComponent<Monster>().AddStun(debuff);
                }
            }

            if (currentSelectedSkill.Skill_DebuffName == "Silent")
            {
                silentRate = currentSelectedSkill.Skill_DebuffRate;
                if (enemyList[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
                {
                    silentRate += 10f;
                }
                int chance = rand.Next(1, 101);
                //int chance = 20;
                Debug.Log("Silent Rate: " + silentRate + ", chance: " + chance);
                if (chance <= silentRate)
                {
                    Debug.Log("Silent Success");
                    Debuff debuff = new Debuff(DebuffName.Silent, currentSelectedSkill.Skill_DebuffTurn);
                    enemyList[i].GetComponent<Monster>().AddSilent(debuff);
                }
            }

            if (currentSelectedSkill.Skill_DebuffName == "Blind")
            {
                blindRate = currentSelectedSkill.Skill_DebuffRate;
                if (enemyList[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
                {
                    blindRate += 10f;
                }
                int chance = rand.Next(1, 101);
                //int chance = 20;
                Debug.Log("Silent Rate: " + blindRate + ", chance: " + chance);
                if (chance <= blindRate)
                {
                    Debug.Log("Silent Success");
                    Debuff debuff = new Debuff(DebuffName.Blind, currentSelectedSkill.Skill_DebuffTurn);
                    enemyList[i].GetComponent<Monster>().AddBlind(debuff);
                }
            }

        }
    }

    void ElementSkillToAlly()
    {
        Debug.Log("ElementSkillToAlly");

        float criticalRate;
        ChemicalStates criticalTarget = GetComponent<PlayerPrefs>().currentEquipElement.characterRoomTempState;

        if (player.currentChemicalState == criticalTarget)
        {
            criticalRate = 1.5f;
        }
        else
        {
            criticalRate = 1f;
        }

        if (currentSelectedSkill.Skill_BuffName == "Dodge")
        {
            Buff buff = new Buff(BuffName.Dodge, currentSelectedSkill.Skill_BuffTurn - 1);
            player.dodgeRate = (int)currentSelectedSkill.Skill_BuffRate;
            player.AddDodge(buff);
        }
        if (currentSelectedSkill.Skill_Heal > 0)
        {
            player.SetHeal((int)(currentSelectedSkill.Skill_Heal * criticalRate));
        }
    }

    void ChemistSkillToEnemy()
    {
        Debug.Log("ChemistSkillToEnemy");
        
        //Increase or Decrease enemy ChemicalStateValue
        if (skillIndex == 3)
        {
            enemyList[0].GetComponent<Monster>().DecrementCSVal();
        }
        else if (skillIndex == 4)
        {
            enemyList[0].GetComponent<Monster>().IncrementCSVal();
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
        enemyList.Clear();

        selectedAlly = null;
    //----ChemistSkill Variables
        isTargetEnemy = false;
    //----ElementSkill Variables
        currentSkillIndex = skillIndex;
        currentSelectedSkill = null;
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
