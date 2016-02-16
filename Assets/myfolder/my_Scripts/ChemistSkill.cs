using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChemistSkill : MonoBehaviour {
    private bool isActive;
    public GameObject currentChemistSkill;

	// Use this for initialization
	void Start () {
        isActive = false;
	}

    void OnMouseDown()
    {
        if (!isActive)
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(true);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(true);
            gameObject.transform.Find("AnalyzeIcon").gameObject.SetActive(true);
            isActive = true;
        }
        else
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(false);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(false);
            gameObject.transform.Find("AnalyzeIcon").gameObject.SetActive(false);
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
            foreach (GameObject monster in monsters)
            {
                monster.transform.Find("selectable").gameObject.SetActive(false);
                monster.transform.Find("selected").gameObject.SetActive(false);
            }
            isActive = false;
        }
    }
    
}
