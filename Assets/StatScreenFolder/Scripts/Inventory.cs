using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public RectTransform InventoryPanel;
    public GameObject Panel;
    public Image ElementImage;
    public Button ButtonElement;
    public List<Element> inventory = new List<Element>();
    public List<Image> elementImage = new List<Image>();
    public List<Button> elementButton = new List<Button>();
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
        //Initialize Loaders
        GetComponent<Loader>().Load();
        GetComponent<SkillLoader>().Load();

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
            clonePanel.transform.SetParent(InventoryPanel.transform,false);
            //Image cloneElement = (Image)Instantiate(ElementImage);
            Button cloneElement = (Button)Instantiate(ButtonElement);
            cloneElement.transform.SetParent(clonePanel.transform,false);
            cloneElement.GetComponent<DragHandler>().id = i;
            //elementImage.Add(cloneElement);
            elementButton.Add(cloneElement);
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
            if (!elementButton[selectedElementID].GetComponent<DragHandler>().equipped)
            {
                equipMode = true;
                //Highlight Slots
                GameObject[] equipSlots = GameObject.FindGameObjectsWithTag("EquipSlot");
                foreach(GameObject equipSlot in equipSlots)
                {
                    equipSlot.GetComponent<Image>().sprite = Resources.Load("EffectImages/EquipSlot(active)", typeof(Sprite)) as Sprite;
                }
            }
        }

        if (unequipMode)
        {
            Debug.Log("Unequip Element");
            GameObject clonePanel = (GameObject)Instantiate(Panel, new Vector3(0, 0, 0), Quaternion.identity);
            clonePanel.transform.SetParent(InventoryPanel.transform, false);
            elementButton[SelectedElement].transform.SetParent(clonePanel.transform, false);
            GameObject.Find("DescriptPanel").transform.Find("EquipButton").Find("Text").GetComponent<Text>().text = "장착";
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
