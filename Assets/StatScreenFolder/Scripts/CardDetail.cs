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
        List<baseCard> cardDatabase = GameObject.Find("GameManager").GetComponent<CardLoad>().cardDeck;

        Debug.Log(element.elementCard1);
        Debug.Log(element.elementCard2);

        baseCard card1 = cardDatabase.Find(x => x.Card_Name == element.elementCard1);
        baseCard card2 = cardDatabase.Find(x => x.Card_Name == element.elementCard2);
        baseCard card3 = cardDatabase.Find(x => x.Card_Name == element.elementCard3);

        Label.text = element.extName + "의 스킬 정보";

        Skill1Name.text = card1.Card_ExtName;
        Skill2Name.text = card2.Card_ExtName;
        Skill3Name.text = card3.Card_ExtName;

        Skill1Descript.text = card1.Card_Description;
        Skill2Descript.text = card2.Card_Description;
        Skill3Descript.text = card3.Card_Description;
        string imagePath1 = "SkillIcons/" + card1.Card_Name;
        string imagePath2 = "SkillIcons/" + card2.Card_Name;
        string imagePath3 = "SkillIcons/" + card3.Card_Name;

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
