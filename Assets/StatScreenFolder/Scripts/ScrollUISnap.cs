using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollUISnap : MonoBehaviour {
    public RectTransform Panel;
    public RectTransform[] Elements;
    public RectTransform LeftAnchor;
    public RectTransform RightAnchor;

    public float[] lDistance;
    public float[] rDistance;
    public int lMinElement;
    public int rMinElement;
    private bool dragging = false;
    int imageDistance;
    

    // Use this for initialization
    void Start () {
        //int elementLength = Panel.transform.childCount;
        //Debug.Log(elementLength);
        //btnDistance = (int)Mathf.Abs(Elements[2].anchoredPosition.x - Elements[0].anchoredPosition.x);
        //Debug.Log(btnDistance);
    }

    public void Initialize()
    {
        Debug.Log("Initialize");

        int elementLength = Panel.transform.childCount;
        //System.Array.Clear(Elements,0,Elements.Length);
        //System.Array.Clear(distance, 0, distance.Length);

        Elements = new RectTransform[elementLength];
        lDistance = new float[elementLength];
        rDistance = new float[elementLength];
        for (int i = 0; i < elementLength; i++)
        {
            Elements[i] = Panel.transform.GetChild(i).GetComponent<RectTransform>();
        }

        if(Elements.Length > 2)
        {
            imageDistance = (int)Mathf.Abs(Elements[2].anchoredPosition.x - Elements[0].anchoredPosition.x);
        }
        else
        {
            imageDistance = 0;
        }
            
    }

    // Update is called once per frame
    void Update () {
	    if(Panel.transform.childCount != Elements.Length)
        {
            Initialize();
        }

        for(int i = 0; i < Elements.Length; i++)
        {
            lDistance[i] = Mathf.Abs(Elements[i].transform.position.x - LeftAnchor.transform.position.x);
            rDistance[i] = Mathf.Abs(Elements[i].transform.position.x - RightAnchor.transform.position.x);

            float lMinDistance = Mathf.Min(lDistance);
            float rMinDistance = Mathf.Min(rDistance);

            for(int a = 0; a < Elements.Length; a++)
            {
                if(lMinDistance == lDistance[a])
                {
                    lMinElement = a;
                }
                if (rMinDistance == rDistance[a])
                {
                    rMinElement = a;
                }
            }
            if (!dragging)
            {
                if(Elements.Length <= 10)//Always anchor 1stElement to LeftAnchor
                {
                    LerpToElement(RightAnchor.anchoredPosition.x);
                    Debug.Log("Left");
                }
                else if(Elements.Length > 10)
                {
                    if((Elements[0].transform.position.x - LeftAnchor.transform.position.x) < 0 &&
                        (Elements[Elements.Length - 1].transform.position.x - RightAnchor.transform.position.x) > 0)
                    {
                        Debug.Log("Middle");
                        //LerpToElement(LeftAnchor.anchoredPosition.x - (lMinElement / 2) * imageDistance);
                    }
                    else if(Elements[0].transform.position.x - LeftAnchor.transform.position.x > 0)
                    {
                        //Debug.Log("Left");
                        LerpToElement(LeftAnchor.anchoredPosition.x);
                    }else if (Elements[0].transform.position.x - LeftAnchor.transform.position.x < 0 &&
                        (Elements[Elements.Length - 1].transform.position.x - RightAnchor.transform.position.x) < 0)
                    {
                        Debug.Log("Right");
                        LerpToElement(LeftAnchor.anchoredPosition.x - (rMinElement/2) * imageDistance);
                    }
                }
            }
        }
	}

    void LerpToElement(float position)
    {
        float newX = Mathf.Lerp(Panel.anchoredPosition.x, position, Time.deltaTime * 1f);
        Vector2 newPosition = new Vector2(newX, Panel.anchoredPosition.y);

        Panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }
    public void EndDrag()
    {
        dragging = false;
    }
}
