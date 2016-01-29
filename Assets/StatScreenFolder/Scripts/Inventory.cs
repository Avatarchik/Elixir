using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public RectTransform InventoryPanel;
    public GameObject Panel;
    public Image ElementImage;
    public List<Element> inventory = new List<Element>();
    public List<Image> elementImage = new List<Image>();
    public List<Element> elementDatabase;
    public Mode mode = Mode.Null;

    public int selectedElementID;
    public bool equipMode = false;
    public bool unequipMode = false;
    public enum Mode
    {
        Card, Element, Null
    }

	// Use this for initialization
	void Start () {

        selectedElementID = -1;
        elementDatabase = GetComponent<Loader>().elementList;
        Debug.Log(GetComponent<Loader>().elementList.Count);
        AddElement(0);
        AddElement(1);
        AddElement(2);
        AddElement(3);
        AddElement(0);
        AddElement(1);
        AddElement(2);
        AddElement(3);
        AddElement(0);
        AddElement(1);
        AddElement(2);
        
        ShowInventory();
    }
	
    void ShowInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            GameObject clonePanel = (GameObject)Instantiate(Panel, new Vector3(0, 0, 0), Quaternion.identity);
            clonePanel.transform.SetParent(InventoryPanel.transform);
            Image cloneElement = (Image)Instantiate(ElementImage, new Vector3(0, 0, 0), Quaternion.identity);
            cloneElement.transform.SetParent(clonePanel.transform);
            cloneElement.rectTransform.sizeDelta = new Vector2(30, 30);
            cloneElement.GetComponent<DragHandler>().id = i;
            elementImage.Add(cloneElement);
        }
    }

    public int SelectedElement
    {
        get { return selectedElementID; }
        set { selectedElementID = value; }
    }

    public void EquipMode()
    {
        if (equipMode)
        {
            equipMode = false;
        }
        else
        {
            if (!elementImage[selectedElementID].GetComponent<DragHandler>().equipped)
            {
                equipMode = true;
            }
        }

        if (unequipMode)
        {
            Debug.Log("Unequip Element");
            GameObject clonePanel = (GameObject)Instantiate(Panel, new Vector3(0, 0, 0), Quaternion.identity);
            clonePanel.transform.SetParent(InventoryPanel.transform);
            elementImage[SelectedElement].transform.SetParent(clonePanel.transform);
            GameObject.Find("SlotPanel").transform.Find("Button").Find("Text").GetComponent<Text>().text = "Equip";
            unequipMode = false;
        }
        
    }

    void AddElement(int id)
    {
        inventory.Add(elementDatabase[id]);
    }

    public void DeleteElement(int id)
    {
        inventory.RemoveAt(id);
        for(int i = 0; i < elementImage.Count; i++)
        {
            Destroy(elementImage[i].gameObject);
        }
        elementImage.Clear();
        ShowInventory();
    }


	// Update is called once per frame
	void Update () {
	
	}
}
