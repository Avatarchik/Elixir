using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UseSkill : MonoBehaviour {
    private TurnBasedCombatStateMachine TBSMachine;
    public MonsterPrefs monsterPrefs;
    //----SkillChosen Variables
    public bool isSkillInUse;
    public int skillIndex;
    public IEnumerator skillInUse;
    public IEnumerator waitForSelection;

    //----Shared Variables
    baseCharacter player;
    //GameObject selectedEnemy;
    //List<GameObject> enemyList = new List<GameObject>();
    int selectedEnemyIndex;
    List<int> enemyIndexList = new List<int>();
    GameObject selectedAlly;
    //----ChemistSkill Variables
    private bool isTargetEnemy;
    //----ElementSkill Variables
    int currentEquipElementIndex;
    int currentSkillIndex;
    baseSkill currentSelectedSkill = new baseSkill();
    bool attackedCritical = false;
    int highestDamage;

    // Use this for initialization
    void Start () {
        //choosingManager = GetComponent<ChoosingManager>();
        TBSMachine = GetComponent<TurnBasedCombatStateMachine>();
        player = GetComponent<PlayerPrefs>().player;
        monsterPrefs = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>();
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
        if(currentSelectedSkill.Skill_UserEffect != null && currentSelectedSkill.Skill_UserEffectTiming == "Before")
        {
            UserEffect();
        }

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
                ElementSkillToEnemy(enemyIndexList.Count);
            }
        }
        else if (targetType == "All")//Needs fix (case where All/Single)
        {
            ElementSkillToAlly();

            if (targetRange == "Single")
            {
                ElementSkillToEnemy(1);
            }
            else if (targetRange == "Wide")
            {
                ElementSkillToEnemy(enemyIndexList.Count);
            }
        }

        //---------
        if (currentSelectedSkill.Skill_UserEffect != null && currentSelectedSkill.Skill_UserEffectTiming == "After")
        {
            UserEffect();
        }

        //----------
        if (attackedCritical)
        {
            Debug.Log("Turn incremented");
            TBSMachine.incrementTurn();
			GameObject.Find ("TurnUI").transform.Find("AddTurn").gameObject.SetActive(true);
            attackedCritical = false;
        }

        //---------
        ResetVariables();
        Button skillBtn = GameObject.Find("SkillPanel").transform.GetChild(skillIndex).GetComponent<Button>();
        skillBtn.GetComponent<Button>().interactable = false;

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

        Debug.Log("End Skill");
        ResetVariables();
        
        yield return null;
    }

    public void UserEffect()
    {
        switch (currentSelectedSkill.Skill_UserEffect)
        {
            case "SelfDamage":
                //Temp damage
                Debug.Log("highest damage: " + highestDamage + ", self damage: " + (int)(highestDamage * currentSelectedSkill.Skill_SelfDamageRate / 100));
                player.SetDamage((int)(highestDamage * currentSelectedSkill.Skill_SelfDamageRate /100));
                break;
            case "ChangeStateToSolid":
                if(player.currentChemicalState != ChemicalStates.SOLID)
                {
                    player.ChangeState(ChemicalStates.SOLID);
                }
                break;

            default:
                break;
        }
        
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

                if (hit.collider != null &&
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
                    selectedEnemyIndex = hit.collider.gameObject.GetComponent<MonsterIndex>().MonsterID;
                    enemyIndexList.Add(selectedEnemyIndex);
                    foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
                    {
                        if (monster != hit.collider.gameObject)
                        {
                            enemyIndexList.Add(monster.GetComponent<MonsterIndex>().MonsterID);
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
                        enemyIndexList.Add(monster.GetComponent<MonsterIndex>().MonsterID);
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

                    selectedEnemyIndex = hit.collider.gameObject.GetComponent<MonsterIndex>().MonsterID;
                    enemyIndexList.Add(selectedEnemyIndex);
                    foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
                    {
                        if (monster != hit.collider.gameObject)
                        {
                            enemyIndexList.Add(monster.GetComponent<MonsterIndex>().MonsterID);
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
        ChemicalStates criticalTarget = GetComponent<PlayerPrefs>().currentEquipElement.characterRoomTempState;
        System.Random rand = new System.Random();
        float criticalRate;
        float stunRate;
        float silentRate;
        float blindRate;

        for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
        {
            if (monsterPrefs.monsterList[enemyIndexList[i]].weakPoint == criticalTarget)
            {
                criticalRate = 1.5f;
                if (!monsterPrefs.monsterList[enemyIndexList[i]].guarded && !monsterPrefs.monsterList[enemyIndexList[i]].shielded)
                {
                    attackedCritical = true;
                    monsterPrefs.monsterList[enemyIndexList[i]].guarded = true;
                    monsterPrefs.monsterObjectList[enemyIndexList[i]].transform.Find("guardIcon").gameObject.SetActive(true);
					monsterPrefs.monsterObjectList[enemyIndexList[i]].transform.Find("criticalEffect").gameObject.SetActive(true);
                }
            }
            else
            {
                criticalRate = 1;
            }

            //Normal damage
            if (currentSelectedSkill.Skill_AttackDamage > 0)
            {
                int tempDamage;
                Debug.Log("Player AD: " + player.AttackDamage + " 계수: " + currentSelectedSkill.Skill_AttackDamage);
                tempDamage = (int)((player.AttackDamage * currentSelectedSkill.Skill_AttackDamage / 100) * criticalRate);

                monsterPrefs.monsterList[enemyIndexList[i]].SetDamage((int)(player.AttackDamage * currentSelectedSkill.Skill_AttackDamage / 100), player.currentChemicalState);

                if(highestDamage < tempDamage)//Record highest damage dealt
                {
                    highestDamage = tempDamage;
                }
            }

            //DotDamage
            if (currentSelectedSkill.Skill_DebuffName == "DotDamage")
            {
                int DotDamageTurn = currentSelectedSkill.Skill_DotDamageTurn;
                float DotDamage = currentSelectedSkill.Skill_DotDamage;
                Debuff debuff = new Debuff(DebuffName.DoteDamage, DotDamageTurn, (int)(player.AttackDamage * DotDamage /100));
                monsterPrefs.monsterList[enemyIndexList[i]].SetDamage((int)(player.AttackDamage * DotDamage / 100));//Inflict damage immediately in this turn                                                                              
                monsterPrefs.monsterList[enemyIndexList[i]].AddDebuff(debuff);//Add debuff to monster
            }

            //Stun
            if (currentSelectedSkill.Skill_DebuffName == "Stun")
            {
                stunRate = currentSelectedSkill.Skill_DebuffRate;
				if (monsterPrefs.monsterList[enemyIndexList[i]].weakPoint == criticalTarget)
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
                    monsterPrefs.monsterList[enemyIndexList[i]].AddDebuff(debuff);
                }
            }
            //Silent
            if (currentSelectedSkill.Skill_DebuffName == "Silent")
            {
                silentRate = currentSelectedSkill.Skill_DebuffRate;
				if (monsterPrefs.monsterList[enemyIndexList[i]].weakPoint == criticalTarget)
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
                    monsterPrefs.monsterList[enemyIndexList[i]].AddDebuff(debuff);
                }
            }
            //Blind
            if (currentSelectedSkill.Skill_DebuffName == "Blind")
            {
                blindRate = currentSelectedSkill.Skill_DebuffRate;
				if (monsterPrefs.monsterList[enemyIndexList[i]].weakPoint == criticalTarget)
                {
                    blindRate += 10f;
                }
                int chance = rand.Next(1, 101);
                //int chance = 20;
                Debug.Log("Silent Rate: " + blindRate + ", chance: " + chance);
                if (chance <= blindRate)
                {
                    Debug.Log("Silent Success");
                    Debuff debuff = new Debuff(DebuffName.Blind, currentSelectedSkill.Skill_DebuffTurn, (int)currentSelectedSkill.Skill_DebuffRate);
                    monsterPrefs.monsterList[enemyIndexList[i]].AddDebuff(debuff);
                }
            }

        }
    }

    void ElementSkillToAlly()
    {
        Debug.Log("ElementSkillToAlly");

        //DotDamage
        if (currentSelectedSkill.Skill_DebuffName == "DotDamage")
        {
            int DotDamageTurn = currentSelectedSkill.Skill_DotDamageTurn;
            float DotDamage = currentSelectedSkill.Skill_DotDamage;
            Debuff debuff = new Debuff(DebuffName.DoteDamage, DotDamageTurn, (int)(player.AttackDamage * DotDamage / 100));
            player.SetDamage((int)(player.AttackDamage * DotDamage / 100));//Inflict damage immediately in this turn                                                                              
            player.AddDebuff(debuff);//Add debuff to player
        }

        //Dodge
        if (currentSelectedSkill.Skill_BuffName == "Dodge")
        {
            Buff buff = new Buff(BuffName.Dodge, currentSelectedSkill.Skill_BuffTurn, (int)currentSelectedSkill.Skill_BuffRate);
            player.AddBuff(buff);
        }
        //Heal
        if (currentSelectedSkill.Skill_Heal > 0)
        {
            player.SetHeal((int)currentSelectedSkill.Skill_Heal);
        }
        //DotHeal
        if(currentSelectedSkill.Skill_BuffName == "DotHeal")
        {
            Buff buff = new Buff(BuffName.DotHeal, currentSelectedSkill.Skill_BuffTurn - 1, (int)currentSelectedSkill.Skill_Heal);
            player.AddBuff(buff);
        }
        //ImmuneCriticalTarget
        if(currentSelectedSkill.Skill_BuffName == "ImmuneCriticalTarget")
        {
            Buff buff = new Buff(BuffName.ImmuneCriticalTarget, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
        //DebuffImmune
        if (currentSelectedSkill.Skill_BuffName == "DebuffImmune")
        {
            Buff buff = new Buff(BuffName.DebuffImmune, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
        //GuardStateChange
        if (currentSelectedSkill.Skill_BuffName == "GuardStateChange")
        {
            Buff buff = new Buff(BuffName.GuardStateChange, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
        //ImmuneHeat
        if (currentSelectedSkill.Skill_BuffName == "ImmuneHeat")
        {
            Buff buff = new Buff(BuffName.ImmuneHeat, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
        //ImmuneSkill
        if (currentSelectedSkill.Skill_BuffName == "ImmuneSkill")
        {
            Buff buff = new Buff(BuffName.ImmuneSkill, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
        //DamageResistance
        if (currentSelectedSkill.Skill_BuffName == "DamageResistance")
        {
            Buff buff = new Buff(BuffName.DamageResistance, currentSelectedSkill.Skill_BuffTurn);
            player.AddBuff(buff);
        }
    }

    void ChemistSkillToEnemy()
    {
        Debug.Log("ChemistSkillToEnemy");
        
        //Increase or Decrease enemy ChemicalStateValue
        if (skillIndex == 3)
        {
            monsterPrefs.monsterList[enemyIndexList[0]].DecrementCSVal();
        }
        else if (skillIndex == 4)
        {
            monsterPrefs.monsterList[enemyIndexList[0]].IncrementCSVal();
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
        //enemyList.Clear();
        enemyIndexList.Clear();

        selectedAlly = null;
    //----ChemistSkill Variables
        isTargetEnemy = false;
    //----ElementSkill Variables
        currentSkillIndex = skillIndex;
        currentSelectedSkill = null;
        attackedCritical = false;
        highestDamage = 0;

        //Decrement Turn
		if (currentSkillIndex >= 0 && currentSkillIndex <= 2)
			TBSMachine.decrementTurn ();
		else
		{
			if (TBSMachine.GetChemicalSkillCount () == 0)
				TBSMachine.decrementTurn ();
			else
			{
				TBSMachine.decrementChemistSkillCount ();
				GameObject.Find ("Button").GetComponent<ChemistSkill> ().InteractOffAllButton ();
			}
		}

        if (TBSMachine.isTurnExhausted())
        {
            TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            TBSMachine.resetTurn();
			TBSMachine.resetChemistSkillCount ();

            for(int i = 0; i < monsterPrefs.monsterList.Count; i++)
            {
                monsterPrefs.monsterList[i].guarded = false;
                monsterPrefs.monsterObjectList[i].transform.Find("guardIcon").gameObject.SetActive(false);
            }

            //foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))//Add all monsters in the selectedEnemy array
            //{
            //    monster.GetComponent<Monster>().guarded = false;
            //    monster.transform.Find("guardIcon").gameObject.SetActive(false);
            //}
        }

        GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();
        isSkillInUse = false;
    }

    public void StopCurrentCoroutines()
    {
        Debug.Log("End SkillInUse");
        StopCoroutine(skillInUse);
        Debug.Log("End WaitForSelection");
        StopCoroutine(waitForSelection);
    }
}
