using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillDetail : MonoBehaviour {
    public Text Label;
    public Text Skill1Name;
    public Text Skill2Name;
    public Text Skill3Name;
    public Image Skill1Icon;
    public Image Skill2Icon;
    public Image Skill3Icon;
    public Text Skill1Descript;
    public Text Skill2Descript;
    public Text Skill3Descript;

    private PlayerPrefs playerPrefs;
    
    public void DisplayInfo()
    {

        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();

        int elementID = playerPrefs.currentEquipElementIndex;
        Element element = playerPrefs.currentEquipElement;

        baseSkill skill1 = playerPrefs.skillList[elementID][0];
        baseSkill skill2 = playerPrefs.skillList[elementID][1];
        baseSkill skill3 = playerPrefs.skillList[elementID][2];

        Label.text = element.extName + "의 스킬 정보";
        Debug.Log(skill1.Skill_ExtName);
        Skill1Name.text = skill1.Skill_ExtName;
        Skill2Name.text = skill2.Skill_ExtName;
        Skill3Name.text = skill3.Skill_ExtName;

        Skill1Descript.text = skill1.Skill_Description;
        Skill2Descript.text = skill2.Skill_Description;
        Skill3Descript.text = skill3.Skill_Description;
        string imagePath1 = "SkillIcons/" + skill1.Skill_Name;
        string imagePath2 = "SkillIcons/" + skill2.Skill_Name;
        string imagePath3 = "SkillIcons/" + skill3.Skill_Name;

        Skill1Icon.sprite = Resources.Load(imagePath1, typeof(Sprite)) as Sprite;
        Skill2Icon.sprite = Resources.Load(imagePath2, typeof(Sprite)) as Sprite;
        Skill3Icon.sprite = Resources.Load(imagePath3, typeof(Sprite)) as Sprite;

    }

}
