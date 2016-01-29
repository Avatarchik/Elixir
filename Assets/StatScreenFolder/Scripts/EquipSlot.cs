using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject elementInside
    {
        get { if (transform.childCount > 0) { return transform.GetChild(0).gameObject; } return null; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.Find("GameManager").GetComponent<Inventory>().equipMode 
            && GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement != -1 
            && GameObject.Find("GameManager").GetComponent<Inventory>().elementImage[GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement].GetComponent<DragHandler>().equipped == false)
        {
            int id = GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement;
            Image elementImage = GameObject.Find("GameManager").GetComponent<Inventory>().elementImage[id];
            elementImage.transform.SetParent(transform);
            elementImage.transform.position = new Vector2(0, 0);
            elementImage.rectTransform.sizeDelta = new Vector2(30, 30);
            GameObject.Find("GameManager").GetComponent<Inventory>().EquipMode();
            GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement = -1;
            elementImage.GetComponent<DragHandler>().equipped = true;

        }
    }
}
