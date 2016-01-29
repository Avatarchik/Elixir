using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDetail : MonoBehaviour {
    public Text CardName;
    public Text ChemState;
    public Text CardEffect;
    public Text CardDescription;

    public void DisplayInfo(int id)
    {
        int elementID = GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement;
        Element element = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[elementID];

        //CardName.text = 
        //ChemState.text = 
        //CardEffect.text = 
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
