using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

public class EnemySkill : MonoBehaviour {
    MonsterPrefs monsterPrefs;
    PlayerPrefs playerPrefs;
    baseCharacter player;

    //MonsterSkillRow skill;
    //Monster attacker;

    void Start()
    {
        monsterPrefs = GetComponent<MonsterPrefs>();
        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();
        player = playerPrefs.player;
    }

    //public void UseSkill2(Monster self, int monsterIndex, string skillName, List<Monster> monstertargetList, bool playerTargeted, bool monsterTargeted)
    //{
    //    System.Random rand = new System.Random();
    //    skill = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skillName);
    //    attacker = self;
    //    string targetType = skill.Target;
    //    string targetRange = skill.Range;

    //    switch (targetType)
    //    {
    //        case "Player":
    //            SkillToPlayer();
    //            break;
    //        case "Monster":
    //            if(targetRange == "Single")
    //            {
    //                int target;
    //                target = rand.Next(0, monstertargetList.Count);
    //                SkillToMonster(monstertargetList[target]);
    //            }else if(targetRange == "Wide")
    //            {
    //                foreach (Monster monster in monstertargetList)
    //                {
    //                    SkillToMonster(monster);
    //                }
    //            }
    //            break;
    //        case "All":
    //            if(targetRange == "Single")
    //            {
    //                int target;
    //                if(playerTargeted && monsterTargeted)
    //                {
    //                    target = rand.Next(0, monstertargetList.Count + 1);
    //                    if(target == monstertargetList.Count)
    //                    {
    //                        SkillToPlayer();
    //                    }
    //                    else
    //                    {
    //                        SkillToMonster(monstertargetList[target]);
    //                    }

    //                }else if(!playerTargeted && monsterTargeted)
    //                {
    //                    target = rand.Next(0, monstertargetList.Count);
    //                    SkillToMonster(monstertargetList[target]);
    //                }
    //                else if(playerTargeted && !monsterTargeted)
    //                {
    //                    SkillToPlayer();
    //                }
    //            }
    //            else
    //            {
    //                SkillToPlayer();
    //                foreach(Monster monster in monstertargetList)
    //                {
    //                    SkillToMonster(monster);
    //                }
    //            }
    //            break;
    //    }

    //}

    //public void SkillToPlayer()
    //{
    //    System.Random rand = new System.Random();
    //    //DamageFactor
    //    if (skill.DamageFactor > 0)
    //    {
    //        player.SetDamage((int)(attacker.attackDamage * skill.DamageFactor / 100), attacker.currentChemicalState);
    //    }
    //    //Action Limit
    //    if(skill.DebuffName == "ActionLimit")
    //    {
    //        Debuff actionLimit = new Debuff(DebuffName.ActionLimit, skill.DebuffTurn);
    //        if (rand.Next(1, 101) <= skill.DebuffRate)
    //        {
    //            player.AddDebuff(actionLimit);
    //        }
    //    }
    //    //Remove Buff
    //    if(skill.DebuffName == "RemoveBuff")
    //    {
    //        player.RemoveDodge();
    //        player.RemoveImmuneCriticalTarget();
    //        player.RemoveDebuffImmune();
    //        player.RemoveGuardStateChange();
    //        player.RemoveImmuneHeat();
    //        player.RemoveDamageResistance();
    //        player.RemoveDotHeal();
    //    }
    //    if(skill.TargetTempChange )
    //}

    //public void SkillToMonster(Monster monster)
    //{

    //}

    public void UseSkill(Monster self, int monsterIndex, string skillName, List<Monster> monstertargetList, bool playerTargeted, bool monsterTargeted)
    {
        System.Random rand = new System.Random();
        Animator animator = monsterPrefs.monsterObjectList[monsterIndex].GetComponent<Animator>();
        MonsterSkillRow skill = GetComponent<MonsterSkillLoad>().Find_MonsterSkillID(skillName);
        int targetIndex;
        int randSeed;

        animator.SetTrigger("Attack");
        if (self.blinded)
        {
            Debug.Log("This monster is blinded. Attack failed");
            return;
        }
        switch (skillName)
        {
            case "SingleAttack":
                Debug.Log("SingleAttack");
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                break;
            case "StrongAttack":
                Debug.Log("StrongAttack");
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                break;
            case "AttackNActionlimit":
                Debug.Log("AttackNActionlimit");
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                Debuff actionLimit = new Debuff(DebuffName.ActionLimit, skill.DebuffTurn);
                if (rand.Next(1, 101) <= skill.DebuffRate)
                {
                    player.AddDebuff(actionLimit);
                }
                break;
            case "Heal":
                Debug.Log("Heal");
                targetIndex = rand.Next(0, monstertargetList.Count); //Randomly pick target to heal
                monstertargetList[targetIndex].SetHeal(skill.Heal);
                break;
            case "Heat":
                Debug.Log("Heat");
                if (playerTargeted && !monsterTargeted)
                {
                    player.IncrementCSVal();
                }
                else if (!playerTargeted && monsterTargeted)
                {
                    targetIndex = rand.Next(0, monstertargetList.Count);
                    monstertargetList[targetIndex].IncrementCSVal();
                }
                else
                {
                    randSeed = monstertargetList.Count + 1;
                    targetIndex = rand.Next(0, randSeed);

                    if (targetIndex == monstertargetList.Count) player.IncrementCSVal();
                    else monstertargetList[targetIndex].IncrementCSVal();
                }
                break;
            case "Freeze":
                Debug.Log("Freeze");
                if (playerTargeted && !monsterTargeted)
                {
                    player.DecrementCSVal();
                }
                else if (!playerTargeted && monsterTargeted)
                {
                    targetIndex = rand.Next(0, monstertargetList.Count);
                    monstertargetList[targetIndex].DecrementCSVal();
                }
                else
                {
                    randSeed = monstertargetList.Count + 1;
                    targetIndex = rand.Next(0, randSeed);

                    if (targetIndex == monstertargetList.Count) player.DecrementCSVal();
                    else monstertargetList[targetIndex].DecrementCSVal();
                }
                break;
            case "Shield":
                Debug.Log("Shield");
                targetIndex = rand.Next(0, monstertargetList.Count);
                Buff shield = new Buff(BuffName.Shield, skill.BuffTurn);
                monstertargetList[targetIndex].AddBuff(shield);
                break;
            case "Purify":
                Debug.Log("Purify");
                targetIndex = rand.Next(0, monstertargetList.Count);
                monstertargetList[targetIndex].RemoveBlind();
                monstertargetList[targetIndex].RemoveStun();
                monstertargetList[targetIndex].RemoveDotDamage();
                monstertargetList[targetIndex].RemoveSilent();
                break;
            case "FromLiquidToSolid":
                Debug.Log("FromLiquidToSolid");
                player.ChangeState(ChemicalStates.SOLID);
                break;
            case "FromLiquidToSolidAll":
                Debug.Log("FromLiquidToSolidAll");
                player.ChangeState(ChemicalStates.SOLID);
                foreach (Monster monster2 in monstertargetList)
                {
                    monster2.ChangeState(ChemicalStates.SOLID);
                }
                break;
            case "SingleAttackDotDamage":
                Debug.Log("SingleAttackDotDamage");
                Debuff dotDamage = new Debuff(DebuffName.DoteDamage, skill.DotDamageTurn, skill.DotDamage);
                player.AddDebuff(dotDamage);
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                break;
            case "RemoveDotDamage":
                Debug.Log("RemoveDotDamage");
                targetIndex = rand.Next(0, monstertargetList.Count);
                monstertargetList[targetIndex].RemoveDotDamage();
                break;
            case "Crash":
                Debug.Log("Crash");
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                break;
            case "ShieldAll":
                Buff shield2 = new Buff(BuffName.Shield, skill.BuffTurn);
                foreach (Monster monster3 in monstertargetList)
                {
                    monster3.AddBuff(shield2);
                }
                break;
            case "DotDamageSingle":
                Debug.Log("DotDamageSingle");
                Debuff dotDamage2 = new Debuff(DebuffName.DoteDamage, skill.DotDamageTurn, skill.DotDamage);
                player.AddDebuff(dotDamage2);
                break;
            case "EnemyTransSolid":
                Debug.Log("EnemyTransSolid");
                player.ChangeState(ChemicalStates.SOLID);
                break;
            case "EnemyTransLiquid":
                Debug.Log("EnemyTransLiquid");
                player.ChangeState(ChemicalStates.LIQUID);
                break;
            case "EnemyTransGas":
                Debug.Log("EnemyTransLiquid");
                player.ChangeState(ChemicalStates.GAS);
                break;
            case "RemoveBuff":
                Debug.Log("RemoveBuff");
                player.RemoveDodge();
                player.RemoveImmuneCriticalTarget();
                player.RemoveDebuffImmune();
                player.RemoveGuardStateChange();
                player.RemoveImmuneHeat();
                player.RemoveDamageResistance();
                player.RemoveDotHeal();
                break;
            case "SuicideBombing":
                Debug.Log("SuicideBombing");
                self.SetDamage(self.maxHp);
                player.SetDamage(self.attackDamage * skill.DamageFactor / 100, self.currentChemicalState);
                break;
        }

    }

    public void UseNormalAttack(Monster self, int monsterIndex)
    {
        Animator animator = monsterPrefs.monsterObjectList[monsterIndex].GetComponent<Animator>();
        if (GameObject.Find("Player(Clone)") != null)
        {
            Debug.Log("Monster Attack");
            if (self.blinded)
            {
                animator.SetTrigger("Attack");
                //Miss effect
            }
            else
            {
				playerPrefs.player.SetDamage(self.attackDamage, self.currentChemicalState);
                animator.SetTrigger("Attack");
            }

        }
    }

    //public void UseSkill(GameObject monster,string SkillName, string KoreanSkillName, List<GameObject> TargetList){
    //	Debug.Log (monster + " use " + SkillName);
    //	//Create Skill popup
    //	GameObject.Find ("GameManager").GetComponent<PopupManager>().CreateMonsterSkillPopup (monster.transform,KoreanSkillName);

    //	//set skill info
    //	MonsterSkillRow skillrow = GetComponent<MonsterSkillLoad> ().Find_MonsterSkillID (SkillName);

    //	//make a targetlist
    //	foreach (GameObject target in TargetList) {
    //		Debug.Log (target.name);
    //	}

    //	//스킬 구현 
    //	if (skillrow.DamageFactor != 0) {
    //		// 스킬 공격력 계수(%)
    //		foreach (GameObject target in TargetList) {
    //			int monsterdamage=monster.GetComponent<Monster>().attackDamage;
    //               GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.SetDamage(monsterdamage*skillrow.DamageFactor/100);
    //		}
    //	}
    //	if (skillrow.Heal != 0) {
    //		//힐 
    //		foreach (GameObject target in TargetList) {
    //			target.GetComponent<Monster>().SetHeal(skillrow.Heal);
    //		}
    //	}
    //	if (skillrow.TargetTempChange != 0) {
    //		// 온도 변화 

    //	}
    //	if (skillrow.TargetStateChange != "N/A") {
    //		// 상태 변화 
    //	}
    //	if (skillrow.DebuffName != "N/A" && skillrow.DebuffRate >= Random.Range (1, 100)) {
    //		// 디버프 
    //	}
    //	if (skillrow.BuffName != "N/A" && skillrow.BuffRate >= Random.Range (1, 100)) {
    //		//버프 
    //	}




    //}
    //public void UseAttack(GameObject monster){
    //	//평타 구현 
    //	Animator animator=monster.GetComponent<Animator>();
    //	if (GameObject.Find ("Player(Clone)") != null) {
    //		Debug.Log ("Monster Attack");
    //           if (monster.GetComponent<Monster>().blinded)
    //           {
    //               animator.SetTrigger("Attack");
    //               //Miss effect
    //           }
    //           else
    //           {
    //               GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.SetDamage (monster.GetComponent<Monster> ().attackDamage);
    //		    animator.SetTrigger ("Attack");
    //           }

    //	}
    //}

}
