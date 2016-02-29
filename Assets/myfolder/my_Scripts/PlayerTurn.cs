using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour {
    public Button skill1Btn;
    public Button skill2Btn;
    public Button skill3Btn;

    public int currentEquipElementIndex;

    public void GetSkills()
    {

        Debug.Log("GetSkill");
        GameObject.Find("SkillPanel").transform.Find("Skill1").GetComponent<Button>().interactable = true;
        GameObject.Find("SkillPanel").transform.Find("Skill2").GetComponent<Button>().interactable = true;
        GameObject.Find("SkillPanel").transform.Find("Skill3").GetComponent<Button>().interactable = true;
        GameObject.Find("Button").GetComponent<Button>().interactable = true;

        currentEquipElementIndex = GetComponent<PlayerPrefs>().currentEquipElementIndex;

        baseSkill skill1 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][0];
        baseSkill skill2 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][1];
        baseSkill skill3 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][2];

        skill1Btn.transform.GetChild(0).GetComponent<Text>().text = skill1.Skill_ExtName;
        skill2Btn.transform.GetChild(0).GetComponent<Text>().text = skill2.Skill_ExtName;
        skill3Btn.transform.GetChild(0).GetComponent<Text>().text = skill3.Skill_ExtName;
    }


}
