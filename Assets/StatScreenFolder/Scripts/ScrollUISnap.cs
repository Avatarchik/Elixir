using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollUISnap : MonoBehaviour {
    public RectTransform Panel;
    public Image[] Elements;
    public RectTransform LeftAnchor;
    public RectTransform RightAnchor;
    public Image Element;

    private float[] distance;
    private bool dragging = false;
    private float topElemPosition;
    private float bottomElemPosition;
    private float horizontalElemDist;

    // Use this for initialization
    void Start () {
        //topElemPosition = LeftAnchor.localPosition.y;
        //Debug.Log(topElemPosition);
        //bottomElemPosition = - topElemPosition;
        //horizontalElemDist = Panel.rect.width;
        ////Instantiate several images
        //Image clone = (Image)Instantiate(Element, new Vector3(0, 0, 0), Quaternion.identity);
        //clone.rectTransform.sizeDelta = new Vector2(30, 30);
        //clone.transform.SetParent(Panel.gameObject.transform.GetChild(0),false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
