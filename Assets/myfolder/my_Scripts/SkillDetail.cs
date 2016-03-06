using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using EnumsAndClasses;

public class SkillDetail : MonoBehaviour {
    public Text SkillName;
    public Text SkillCState;
    public Text SkillDescript;

    private PlayerPrefs playerPrefs;
    
    public void DisplayInfo(int skillindex)
    {

        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();

        int elementID = playerPrefs.currentEquipElementIndex;
        Element element = playerPrefs.currentEquipElement;
        baseSkill skill = playerPrefs.skillList[elementID][skillindex];
        string koreanChemicalState;

        switch (element.characterRoomTempState)
        {
            case ChemicalStates.SOLID:
                koreanChemicalState = "고체";
                break;
            case ChemicalStates.LIQUID:
                koreanChemicalState = "액체";
                break;
            case ChemicalStates.GAS:
                koreanChemicalState = "기체";
                break;
            default:
                koreanChemicalState = "몰라";
                break;
        }

        SkillName.text = skill.Skill_ExtName;
        SkillCState.text = "(" + koreanChemicalState + ")";
		baseCharacter player = playerPrefs.player;
		string tempDescript = skill.Skill_Description;
		tempDescript = tempDescript.Replace("AttackDamage", ((int)(skill.Skill_AttackDamage * player.AttackDamage / 100)).ToString());
		tempDescript = tempDescript.Replace("DotDamage", ((int)(skill.Skill_DotDamage * player.AttackDamage / 100)).ToString());
		tempDescript = tempDescript.Replace("Heal", ((int)(skill.Skill_Heal * player.MAX_HP)).ToString());
		tempDescript = tempDescript.Replace("Dodge", skill.Skill_BuffRate + "% 회피버프");
		tempDescript = tempDescript.Replace("SelfDamageRate", skill.Skill_SelfDamageRate.ToString());
		tempDescript = tempDescript.Replace("DebuffRate", skill.Skill_DebuffRate.ToString());
		tempDescript = tempDescript.Replace("DebuffTurn", skill.Skill_DebuffTurn.ToString());
		tempDescript = tempDescript.Replace("BuffRate", skill.Skill_BuffRate.ToString());
		tempDescript = tempDescript.Replace("BuffTurn", skill.Skill_BuffTurn.ToString());
        
		SkillDescript.text = tempDescript;

    }

}
