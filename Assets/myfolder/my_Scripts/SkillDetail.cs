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
        Debug.Log(SkillName.GetComponent<RectTransform>().rect.width);
        SkillDescript.text = skill.Skill_Description;

    }

}
