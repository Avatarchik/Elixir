using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour {
    public Button skill1Btn;
    public Button skill2Btn;
    public Button skill3Btn;

    public int currentEquipElementIndex;

    private List<baseCard> cardList;
    public void GetSkills()
    {

        Debug.Log("GetSkill");
        GameObject.Find("SkillPanel").transform.GetChild(0).GetComponent<Button>().interactable = true;
        GameObject.Find("SkillPanel").transform.GetChild(1).GetComponent<Button>().interactable = true;
        GameObject.Find("SkillPanel").transform.GetChild(2).GetComponent<Button>().interactable = true;

        currentEquipElementIndex = GetComponent<PlayerPrefs>().currentEquipElementIndex;

        baseCard skill1 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][0];
        baseCard skill2 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][1];
        baseCard skill3 = GetComponent<PlayerPrefs>().skillList[currentEquipElementIndex][2];

        skill1Btn.transform.GetChild(0).GetComponent<Text>().text = skill1.Card_ExtName;
        skill2Btn.transform.GetChild(0).GetComponent<Text>().text = skill2.Card_ExtName;
        skill3Btn.transform.GetChild(0).GetComponent<Text>().text = skill3.Card_ExtName;
    }


}
