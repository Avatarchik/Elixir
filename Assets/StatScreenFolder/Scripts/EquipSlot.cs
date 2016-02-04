using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerClickHandler
{
    Sprite normal;
    Sprite active;

    void Start()
    {
        normal = Resources.Load("EffectImages/EquipSlot(normal)", typeof(Sprite)) as Sprite;
        active = Resources.Load("EffectImages/EquipSlot(active)", typeof(Sprite)) as Sprite;
    }

    public GameObject elementInside
    {
        get { if (transform.childCount > 0) { return transform.GetChild(0).gameObject; } return null; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!elementInside
            && GameObject.Find("GameManager").GetComponent<Inventory>().equipMode 
            && GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement != -1 
            && GameObject.Find("GameManager").GetComponent<Inventory>().elementButton[GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement].GetComponent<DragHandler>().equipped == false)
        {
            int id = GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement;
            Button elementButton = GameObject.Find("GameManager").GetComponent<Inventory>().elementButton[id];
            elementButton.transform.SetParent(transform);
            elementButton.transform.position = new Vector2(0, 0);
            GameObject.Find("GameManager").GetComponent<Inventory>().EquipMode();
            //GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement = -1;
            elementButton.GetComponent<DragHandler>().equipped = true;

            GameObject[] equipSlots = GameObject.FindGameObjectsWithTag("EquipSlot");

            foreach (GameObject equipSlot in equipSlots)
            {
                equipSlot.GetComponent<Image>().sprite = Resources.Load("EffectImages/EquipSlot(normal)", typeof(Sprite)) as Sprite;
            }

        }

    }

    void Update()
    {
        if (transform.childCount != 0 && transform.GetChild(0).GetComponent<DragHandler>().id == GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement)
        {
            GetComponent<Image>().sprite = active;
        }
        else if (transform.childCount != 0 && transform.GetChild(0).GetComponent<DragHandler>().id != GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement && GetComponent<Image>().sprite != normal)
        {
            GetComponent<Image>().sprite = normal;
        }else if(transform.childCount == 0 && GetComponent<Image>().sprite != normal)
        {
            GetComponent<Image>().sprite = normal;
        }
    }
}
