using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System;
using UnityEngine.UI;
public class ChemistSkill : MonoBehaviour{
    private bool isActive;

	// Use this for initialization
	void Start () {
        isActive = false;
	}

    public void EnableButtons()
    {
        gameObject.transform.Find("CoolIcon").gameObject.SetActive(true);
        gameObject.transform.Find("HeatIcon").gameObject.SetActive(true);
        gameObject.transform.Find("AnalyzeIcon").gameObject.SetActive(true);
        gameObject.transform.Find("Change").gameObject.SetActive(true);
        isActive = true;
    }

    public void DisableButtons()
    {
        gameObject.transform.Find("CoolIcon").gameObject.SetActive(false);
        gameObject.transform.Find("HeatIcon").gameObject.SetActive(false);
        gameObject.transform.Find("AnalyzeIcon").gameObject.SetActive(false);
        gameObject.transform.Find("Change").gameObject.SetActive(false);
        isActive = false;
    }
	public void InteractAllButton()
	{
		GameObject.Find("Button").transform.Find("CoolIcon").GetComponent<Button>().interactable = true;
		GameObject.Find("Button").transform.Find("HeatIcon").GetComponent<Button>().interactable = true;
		GameObject.Find("Button").transform.Find("AnalyzeIcon").GetComponent<Button>().interactable = true;
		GameObject.Find("Button").transform.Find("Change").GetComponent<Button>().interactable = true;
	}
    public void Clicked()
    {
        if (!isActive)
        {
            EnableButtons();
        }
        else
        {
            DisableButtons();
        }
    }
}
