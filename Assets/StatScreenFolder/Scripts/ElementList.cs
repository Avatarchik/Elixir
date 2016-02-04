using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElementList : MonoBehaviour {
    public RectTransform ScrollPanel;
    public Vector2 initialAnchor;
    // Use this for initialization
    void Start () {
        //ScrollPanel.sizeDelta = new Vector2(ScrollPanel.transform.localPosition.x, 200f);
        initialAnchor = ScrollPanel.anchoredPosition;
	}
	
    public void ScrollPanelPos()
    {
        Debug.Log(ScrollPanel.anchoredPosition.y);
    }

	// Update is called once per frame
	void Update () {
	if(ScrollPanel.transform.childCount <= 8)
        {
            ScrollPanel.anchoredPosition = initialAnchor;
            GetComponent<ScrollRect>().vertical = false;
        }
        else
        {
            GetComponent<ScrollRect>().vertical = true;
        }
	}
}
