using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragHandler : MonoBehaviour , IPointerClickHandler{
    public static GameObject elementBeingDragged;
    public Text Name;
    public Text Description;
    public Image Skill1;
    public Image Skill2;
    public Image Skill3;
    public GameObject DetailPanel;

    baseCard card1;
    baseCard card2;
    baseCard card3;

    public int id;
    public bool equipped = false;

    public void Clicked()
    {
        Debug.Log("Clicked");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");

        if (GameObject.Find("GameManager").GetComponent<Inventory>().equipMode == true && equipped)
        {
            //parent of selected object
            Transform thatParent = GameObject.Find("GameManager").GetComponent<Inventory>().elementButton[GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement].transform.parent;
            GameObject.Find("GameManager").GetComponent<Inventory>().elementButton[GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement].transform.SetParent(transform.parent);
            transform.SetParent(thatParent);
            GameObject.Find("GameManager").GetComponent<Inventory>().elementButton[GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement].GetComponent<DragHandler>().equipped = true;
            equipped = false;
            GameObject[] equipSlots = GameObject.FindGameObjectsWithTag("EquipSlot");
            GameObject.Find("GameManager").GetComponent<Inventory>().equipMode = false;

            foreach (GameObject equipSlot in equipSlots)
            {
                equipSlot.GetComponent<Image>().sprite = Resources.Load("EffectImages/EquipSlot(normal)", typeof(Sprite)) as Sprite;
            }
        }

        GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement = id;

        if (equipped)
        {
            GameObject.Find("GameManager").GetComponent<Inventory>().unequipMode = true;
            GameObject.Find("DescriptPanel").transform.Find("EquipButton").Find("Text").GetComponent<Text>().text = "해제";
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<Inventory>().unequipMode = false;
            GameObject.Find("DescriptPanel").transform.Find("EquipButton").Find("Text").GetComponent<Text>().text = "장착";
        }

        Element thisElement = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[id];
        Name.text = thisElement.extName;
        Description.text = thisElement.desription;

        GameObject.FindGameObjectWithTag("DescriptPanel").transform.Find("Thermometer").GetComponent<ThermoBar>().GetData(GameObject.Find("GameManager").GetComponent<Inventory>().inventory, id);

        string imagePath1 = "SkillIcons/" + card1.Card_Name;
        string imagePath2 = "SkillIcons/" + card2.Card_Name;
        string imagePath3 = "SkillIcons/" + card3.Card_Name;

        Skill1.sprite = Resources.Load(imagePath1, typeof(Sprite)) as Sprite;
        Skill2.sprite = Resources.Load(imagePath2, typeof(Sprite)) as Sprite;
        Skill3.sprite = Resources.Load(imagePath3, typeof(Sprite)) as Sprite;

        
    }
    

    // Use this for initialization
    void Start () {
        Element element = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[id];
        List<baseCard> cardDatabase = GameObject.Find("GameManager").GetComponent<CardLoad>().cardDeck;

        card1 = cardDatabase.Find(x => x.Card_Name == element.elementCard1);
        card2 = cardDatabase.Find(x => x.Card_Name == element.elementCard2);
        card3 = cardDatabase.Find(x => x.Card_Name == element.elementCard3);

        Name = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(0).GetComponent<Text>();
        Description = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(1).GetComponent<Text>();
        Skill1 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(2).GetComponent<Image>();
        Skill2 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(3).GetComponent<Image>();
        Skill3 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(4).GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

}
