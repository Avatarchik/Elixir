using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour , IPointerClickHandler{
    public static GameObject elementBeingDragged;
    public Text Name;
    public Text Element;
    public Text Type;
    public Text Description;
    public Text MeltPoint;
    public Text FreezePoint;
    public Text Skill1;
    public Text Skill2;
    public Text Skill3;
    public Text ChemicalSkill;
    public Text CompoundSkill;

    public int id;
    public bool equipped = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (equipped)
        {
            GameObject.Find("GameManager").GetComponent<Inventory>().unequipMode = true;
            GameObject.Find("SlotPanel").transform.Find("Button").Find("Text").GetComponent<Text>().text = "Unequip";
        }
        else
        {
            GameObject.Find("SlotPanel").transform.Find("Button").Find("Text").GetComponent<Text>().text = "Equip";
        }

        GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement = id;

        Element thisElement = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[id];
        Name.text = "명칭 : " + thisElement.extName;
        Element.text = "화학 기호 : " + thisElement.name;
        Type.text = "화학 분류 : " + thisElement.chemicalSeries;
        Description.text = "Description : " + thisElement.desription;
        Skill1.text = thisElement.elementCard1;
        Skill2.text = thisElement.elementCard2;
        Skill3.text = thisElement.elementCard3;
    }
    

    // Use this for initialization
    void Start () {
        Name = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(0).GetComponent<Text>();
        Element = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(1).GetComponent<Text>();
        Type = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(2).GetComponent<Text>();
        Description = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(3).GetComponent<Text>();
        MeltPoint = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(4).GetComponent<Text>();
        FreezePoint = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(5).GetComponent<Text>();
        Skill1 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(6).GetChild(0).GetComponent<Text>();
        Skill2 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(7).GetChild(0).GetComponent<Text>();
        Skill3 = GameObject.FindGameObjectWithTag("DescriptPanel").transform.GetChild(8).GetChild(0).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

}
