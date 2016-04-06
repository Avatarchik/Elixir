using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillPanelManager : MonoBehaviour {
	public Sprite skillImage1;
	public Sprite skillImage2;
	public Sprite skillImage3;
	// Use this for initialization
	void Start () {
	
	}
	public void SetSkillPanel(Element currentEquippedElement)
	{
		Debug.Log (currentEquippedElement.elementCard1 + " " + currentEquippedElement.elementCard2 + " " + currentEquippedElement.elementCard3);
		Debug.Log ("SkillPanel Child Count : " + GameObject.Find("SkillPanel").transform.childCount);

		string imagePath1 = "SkillIcons/" + currentEquippedElement.elementCard1;
		string imagePath2 = "SkillIcons/" + currentEquippedElement.elementCard2;
		string imagePath3 = "SkillIcons/" + currentEquippedElement.elementCard3;

		skillImage1 = Resources.Load<Sprite> (imagePath1);
		skillImage2 = Resources.Load<Sprite> (imagePath2);
		skillImage3 = Resources.Load<Sprite> (imagePath3);

		GameObject.Find("SkillPanel").transform.Find("Skill1").GetComponent<Image>().overrideSprite = skillImage1;
		GameObject.Find("SkillPanel").transform.Find("Skill2").GetComponent<Image>().overrideSprite = skillImage2;
		GameObject.Find("SkillPanel").transform.Find("Skill3").GetComponent<Image>().overrideSprite = skillImage3;
	}
	// Update is called once per frame
	/*void Update () {
	
	}*/
}
