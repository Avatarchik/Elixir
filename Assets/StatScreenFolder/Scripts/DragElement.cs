using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragElement : MonoBehaviour, IDragHandler {
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Is Dragging");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
