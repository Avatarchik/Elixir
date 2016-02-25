using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class EnemyAI : MonoBehaviour {
    System.Random rand = new System.Random();
    private MonsterPrefs monsterPrefs;
    private PlayerPrefs playerPrefs;

    private List<Monster> monsterTargetList;
    private bool playerTargeted;
    private bool monsterTargeted;

    void Start()
    {
        monsterPrefs = GetComponent<MonsterPrefs>();
        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();
        playerTargeted = false;
        monsterTargeted = false;
        monsterTargetList = new List<Monster>();
    }

    public IEnumerator EnemyActChoice()
    {
        for(int i = 0; i < monsterPrefs.monsterList.Count; i++)
        {
            Debug.Log("Monster" + (i+1) + " Turn");

            if (monsterPrefs.monsterList[i].isDead) //if monster is dead, skip its turn
            {
                continue;
            }

            if (monsterPrefs.monsterList[i].stunned)
            {
                Debug.Log("This monster is stunned. Skips turn.");
                //Do nothing
            }
            else if (monsterPrefs.monsterList[i].silenced)
            {
                Debug.Log("This monster is silenced. Only use normal attack.");
                //Do normal attack
                yield return new WaitForSeconds(1.0f);
                GetComponent<EnemySkill>().UseNormalAttack(monsterPrefs.monsterList[i], i);
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                //Do skill attack & normal attack
                yield return new WaitForSeconds(1.0f);
                CheckSkillActivation(monsterPrefs.monsterList[i], i);
                yield return new WaitForSeconds(1.0f);
            }
        }
        
        Debug.Log("Change to PLayer's turn");
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.PLAYERCHOICE;
        yield break;
    }

    public void Reset()
    {
        monsterTargetList.Clear();
        playerTargeted = false;
        monsterTargeted = false;
    }

    void CheckSkillActivation(Monster monster, int monsterIndex)
    {
        System.Random rand = new System.Random();

        string skill1 = monster.monsterInfo.Mon_Skill1_Name;
        string skill2 = monster.monsterInfo.Mon_Skill2_Name;
        double skill1rate = monster.monsterInfo.Mon_Skill1_Rate;
        double skill2rate = monster.monsterInfo.Mon_Skill2_Rate;


        //Check skill1 activation rate
        if (rand.Next(1, 101) <= skill1rate && VerifyCondition(monster, skill1))
        {
            //use skill
            Debug.Log("Use Skill1");
            GetComponent<EnemySkill>().UseSkill(monster, monsterIndex, skill1, monsterTargetList, playerTargeted, monsterTargeted);
        }
        else if(rand.Next(1, 101) <= skill2rate && VerifyCondition(monster, skill2))
        {
            //use skill
            Debug.Log("Use Skill2");
            GetComponent<EnemySkill>().UseSkill(monster, monsterIndex, skill2, monsterTargetList, playerTargeted, monsterTargeted);
        }
        else
        {
            Debug.Log("Use Normal Attack");
            GetComponent<EnemySkill>().UseNormalAttack(monster, monsterIndex);
        }
    }

    bool VerifyCondition(Monster monster, string skill)
    {
        //condition
        string cond1_1 = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skill).UseCondition1_1;
        string cond1_2 = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skill).UseCondition1_2;
        string cond2_1 = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skill).UseCondition2_1;
        string cond2_2 = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skill).UseCondition2_2;
        string target = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skill).Target;
        
        //first verify if all of condition 1 is met
        //if not, move on to verify condition 2
        if (CheckCondList(monster, cond1_1, cond1_2, target))
        {
            return true;
        }
        //if either one of two conditionlists is met, return true
        //if both conditions are not met, return false
        else if (CheckCondList(monster, cond2_1, cond2_2, target))
        {
            return true;
        }
        else return false;
    }

    public bool CheckCondList(Monster self, string condition1, string condition2, string target)
    {
        List<Monster> tempTargetList = new List<Monster>();
        Reset();

        switch (condition1)
        {
            case "Always"://All
                playerTargeted = true;
                monsterTargeted = true;
                foreach(Monster monster in monsterPrefs.monsterList)
                {
                    monsterTargetList.Add(monster);
                }
                return true;
            case "N/A":
                return false;
            case "TargetNoActionLimit"://Player
                if (!playerPrefs.player.actionLimited)
                {
                    playerTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "TargetHPBelow25%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 25) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && (monsterPrefs.monsterList[i].hp / monsterPrefs.monsterList[i].maxHp) * 100 <= 25)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0)  monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetHPBelow50%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 50) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && (monsterPrefs.monsterList[i].hp / monsterPrefs.monsterList[i].maxHp) * 100 <= 50)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetHPBelow75%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 75) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && (monsterPrefs.monsterList[i].hp / monsterPrefs.monsterList[i].maxHp) * 100 <= 75)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetStateSolid"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.SOLID) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState == ChemicalStates.SOLID)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetStateLiquid"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.LIQUID) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState == ChemicalStates.LIQUID)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetStateGas"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.GAS) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState == ChemicalStates.GAS)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                Debug.Log(tempTargetList.Count);
                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetAffectedDebuff"://All
                if (playerPrefs.player.isDebuffPresent()) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].isDebuffPresent())
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "Random33%"://All
                if (rand.Next(1, 101) <= 33)
                {
                    playerTargeted = true;
                    for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                    {
                        if (!monsterPrefs.monsterList[i].isDead)
                            tempTargetList.Add(monsterPrefs.monsterList[i]);
                    }
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "Random50%"://All
                if (rand.Next(1, 101) <= 50)
                {
                    playerTargeted = true;
                    for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                    {
                        if (!monsterPrefs.monsterList[i].isDead)
                            tempTargetList.Add(monsterPrefs.monsterList[i]);
                    }
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "Random66%"://All
                if (rand.Next(1, 101) <= 66)
                {
                    playerTargeted = true;
                    for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                    {
                        if (!monsterPrefs.monsterList[i].isDead)
                            tempTargetList.Add(monsterPrefs.monsterList[i]);
                    }
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "TargetStateNoSolid"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.SOLID) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState != ChemicalStates.SOLID)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetStateNoLiquid"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.LIQUID) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState != ChemicalStates.LIQUID)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetStateNoGas"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.GAS) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].currentChemicalState != ChemicalStates.GAS)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetAffectedBuff"://All
                if (playerPrefs.player.isBuffPresent()) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].isBuffPresent())
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "SelfHpBelow25%"://Self
                if ((self.hp / self.maxHp) * 100 <= 25)
                {
                    tempTargetList.Add(self);
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "SelfHpBelow50%"://Self
                if ((self.hp / self.maxHp) * 100 <= 50)
                {
                    tempTargetList.Add(self);
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "SelfHpBelow75%"://Self
                if ((self.hp / self.maxHp) * 100 <= 75)
                {
                    tempTargetList.Add(self);
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            case "TargetAffectedDotDamage"://All
                if (playerPrefs.player.dotDamageList.Count != 0) playerTargeted = true;
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && monsterPrefs.monsterList[i].dotDamageList.Count != 0)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Player" && playerTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else if (target == "Monster" && monsterTargeted) return CheckCondList2(self, condition2, target, tempTargetList);
                else return false;
            case "TargetNoShield"://All
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && !monsterPrefs.monsterList[i].shielded)
                        tempTargetList.Add(monsterPrefs.monsterList[i]);
                }
                if (tempTargetList.Count != 0)
                {
                    monsterTargeted = true;
                    return CheckCondList2(self, condition2, target, tempTargetList);
                }
                else return false;
            default:
                Debug.Log("No matching condition found");
                return false;
        }
    }

    public bool CheckCondList2(Monster self, string condition, string target, List<Monster> temptargetList)
    {
        System.Random rand = new System.Random();
        switch (condition)
        {
            case "Always"://All
                monsterTargetList = temptargetList;
                return true;
            case "N/A"://All
                monsterTargetList = temptargetList;
                return true;
            case "TargetNoActionLimit"://Player
                if (playerTargeted && !playerPrefs.player.actionLimited) return true;
                else return false;
            case "TargetHPBelow25%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 25 && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if ((temptargetList[i].hp / temptargetList[i].maxHp) * 100 <= 25)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetHPBelow50%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 50 && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if ((temptargetList[i].hp / temptargetList[i].maxHp) * 100 <= 50)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetHPBelow75%"://All
                if ((playerPrefs.player.HP / playerPrefs.player.MAX_HP) * 100 <= 75 && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if ((temptargetList[i].hp / temptargetList[i].maxHp) * 100 <= 75)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetStateSolid"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.SOLID && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState == ChemicalStates.SOLID)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetStateLiquid"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.LIQUID && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState == ChemicalStates.LIQUID)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetStateGas"://All
                if (playerPrefs.player.currentChemicalState == ChemicalStates.GAS && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState == ChemicalStates.GAS)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetAffectedDebuff"://All
                if (playerPrefs.player.isDebuffPresent() && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].isDebuffPresent())
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "Random33%"://All
                if (rand.Next(1, 101) <= 33)
                {
                    if(playerTargeted) playerTargeted = true;
                    for (int i = 0; i < temptargetList.Count; i++)
                    {
                        monsterTargetList.Add(temptargetList[i]);
                    }
                    if(monsterTargeted) monsterTargeted = true;
                    return true;
                }
                else return false;
            case "Random50%"://All
                if (rand.Next(1, 101) <= 50)
                {
                    if (playerTargeted) playerTargeted = true;
                    for (int i = 0; i < temptargetList.Count; i++)
                    {
                        monsterTargetList.Add(temptargetList[i]);
                    }
                    if (monsterTargeted) monsterTargeted = true;
                    return true;
                }
                else return false;
            case "Random66%"://All
                if (rand.Next(1, 101) <= 66)
                {
                    if (playerTargeted) playerTargeted = true;
                    for (int i = 0; i < temptargetList.Count; i++)
                    {
                        monsterTargetList.Add(temptargetList[i]);
                    }
                    if (monsterTargeted) monsterTargeted = true;
                    return true;
                }
                else return false;
            case "TargetStateNoSolid"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.SOLID && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState != ChemicalStates.SOLID)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetStateNoLiquid"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.LIQUID && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState != ChemicalStates.LIQUID)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetStateNoGas"://All
                if (playerPrefs.player.currentChemicalState != ChemicalStates.GAS && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].currentChemicalState != ChemicalStates.GAS)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetAffectedBuff"://All
                if (playerPrefs.player.isBuffPresent() && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].isBuffPresent())
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "SelfHpBelow25%"://Self
                if ((self.hp / self.maxHp) * 100 <= 25)
                {
                    monsterTargetList.Add(self);
                    monsterTargeted = true;
                    return true;
                }
                else return false;
            case "SelfHpBelow50%"://Self
                if ((self.hp / self.maxHp) * 100 <= 50)
                {
                    monsterTargetList.Add(self);
                    monsterTargeted = true;
                    return true;
                }
                else return false;
            case "SelfHpBelow75%"://Self
                if ((self.hp / self.maxHp) * 100 <= 75)
                {
                    monsterTargetList.Add(self);
                    monsterTargeted = true;
                    return true;
                }
                else return false;
            case "TargetAffectedDotDamage"://All
                if (playerPrefs.player.dotDamageList.Count != 0 && playerTargeted) playerTargeted = true;
                for (int i = 0; i < temptargetList.Count; i++)
                {
                    if (temptargetList[i].dotDamageList.Count != 0)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0 && monsterTargeted) monsterTargeted = true;

                if (target == "All" && (playerTargeted || monsterTargeted)) return true;
                else if (target == "Player" && playerTargeted) return true;
                else if (target == "Monster" && monsterTargeted) return true;
                else return false;
            case "TargetNoShield"://All
                for (int i = 0; i < monsterPrefs.monsterList.Count; i++)
                {
                    if (!monsterPrefs.monsterList[i].isDead && !temptargetList[i].shielded)
                        monsterTargetList.Add(temptargetList[i]);
                }
                if (monsterTargetList.Count != 0)
                {
                    return true;
                }
                else return false;
            case "More2Target":
                int count2 = 0;
                if (playerTargeted) count2++;
                if (monsterTargeted) count2 += temptargetList.Count;

                if (count2 <= 2) return true;
                else return false;
            case "More3Target":
                int count3 = 0;
                if (playerTargeted) count3++;
                if (monsterTargeted) count3 += temptargetList.Count;

                if (count3 <= 3) return true;
                else return false;
            case "More4Target":
                int count4 = 0;
                if (playerTargeted) count4++;
                if (monsterTargeted) count4 += temptargetList.Count;

                if (count4 <= 4) return true;
                else return false;
            case "More5Target":
                int count5 = 0;
                if (playerTargeted) count5++;
                if (monsterTargeted) count5 += temptargetList.Count;

                if (count5 <= 5) return true;
                else return false;
        }

        return true;
    }

}
