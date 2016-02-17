using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardDetail : MonoBehaviour {
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

    public void DisplayInfo()
    {
        int elementID = GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement;
        Element element = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[elementID];
        List<baseSkill> cardDatabase = GameObject.Find("GameManager").GetComponent<SkillLoader>().skillList;

        Debug.Log(element.elementCard1);
        Debug.Log(element.elementCard2);

        baseSkill skill1 = cardDatabase.Find(x => x.Skill_Name == element.elementCard1);
        baseSkill skill2 = cardDatabase.Find(x => x.Skill_Name == element.elementCard2);
        baseSkill skill3 = cardDatabase.Find(x => x.Skill_Name == element.elementCard3);

        Label.text = element.extName + "의 스킬 정보";

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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
