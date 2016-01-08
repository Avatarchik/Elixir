using UnityEngine;
using System.Collections;

public class ChemistSkill : MonoBehaviour {
    private bool isActive;
	// Use this for initialization
	void Start () {
        isActive = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.gameObject.tag == "ChemistSkill") //When the skill targets Ally, and Ally is selected
            {
                Debug.Log(hit.collider.gameObject);
                Highlight();
            }
        }
        if (!isActive)
        {
            UnHighlight();
        }
    }

    void Highlight()
    {
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject Monster in Monsters)
        {
            if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
                Monster.transform.Find("selectable").gameObject.SetActive(true);
        }
    }
    void UnHighlight()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
        foreach (GameObject monster in monsters)
        {
            monster.transform.Find("selectable").gameObject.SetActive(false);
            monster.transform.Find("selected").gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (!isActive)
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(true);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(true);
            isActive = true;
        }
        else
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(false);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(false);
            isActive = false;
        }
        
    }
}
