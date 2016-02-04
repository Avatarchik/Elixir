using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    bool isElementSelected;

    Sprite normal;
    Sprite active;
    // Use this for initialization
    void Start () {
        normal = Resources.Load("EffectImages/EquipSlot(normal)", typeof(Sprite)) as Sprite;
        active = Resources.Load("EffectImages/EquipSlot(active)", typeof(Sprite)) as Sprite;
    }

	// Update is called once per frame
	void Update () {
	    if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }

        if (transform.childCount != 0 && transform.GetChild(0).GetComponent<DragHandler>().id == GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement)
        {
            GetComponent<Image>().sprite = active;
        }
        else if(transform.childCount != 0 && transform.GetChild(0).GetComponent<DragHandler>().id != GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement && GetComponent<Image>().sprite != normal)
        {
            Debug.Log("AAA");
            GetComponent<Image>().sprite = normal;
        }
    }
}
