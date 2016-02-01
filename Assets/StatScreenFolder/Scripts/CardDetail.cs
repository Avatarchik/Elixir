using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardDetail : MonoBehaviour {
    public Text CardName;
    public Text ChemState;
    public Text CardEffect;
    public Text CardDescription;

    public void DisplayInfo(int id)
    {
        int elementID = GameObject.Find("GameManager").GetComponent<Inventory>().SelectedElement;
        Element element = GameObject.Find("GameManager").GetComponent<Inventory>().inventory[elementID];
        List<baseCard> cardDatabase = GameObject.Find("GameManager").GetComponent<CardLoad>().cardDeck;

        baseCard card = cardDatabase.Find(x => x.Card_Name == element.elementCard1);
        Debug.Log(card.Card_ExtName);
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
