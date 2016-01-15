using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using EnumsAndClasses;

public class HandSet : MonoBehaviour {
	private List<int> indexesOfSelectedCard;
	private int temp;
	public GameObject cardPrefab;
    
    public GameObject[] cards;
    public GameObject skillTextPanel;
	void Start(){
        
	}
	void Update(){

	}
	public void CardSet(){
		CardSelect ();
		CardDraw ();
	}
	void CardSelect(){
		int count;
		indexesOfSelectedCard = new List<int> ();
		for(int i = 0; i<=3; i++)
		{
			indexesOfSelectedCard.Add(i);
		}
		for (int i=0; i<=3; i++) {
			do {
				count=0;
				temp=Random.Range (0, 19);
				for(int x=0;x<i;x++){
					if(indexesOfSelectedCard[x]==temp)
						count++;
				}
			} while(count!=0);
			indexesOfSelectedCard[i]=temp;
		}
		
	}

    void ApplyCardInfo(GameObject card, int cardIndex)
    {
        card.GetComponent<InfoCard> ().Card = GetComponent<CardLoad> ().cardDeck [cardIndex];
        card.transform.FindChild ("CardName").GetComponent<Text> ().text = GetComponent<CardLoad> ().cardDeck [cardIndex].Card_ExtName + " / " + GetComponent<CardLoad>().cardDeck[cardIndex].Card_CriticalTarget;
		card.transform.FindChild ("CardStatement").GetComponent<Text> ().text = GetComponent<CardLoad> ().cardDeck [cardIndex].Card_Description;
    }
    
	void CardDraw(){
        skillTextPanel.SetActive(true);
		skillTextPanel.GetComponentInChildren<Text>().text = "";
        for (int i = 0; i < 4; i++)
        {
            cards[i].SetActive(true);
            cards[i].GetComponent<Button>().interactable = true;
            ApplyCardInfo(cards[i], indexesOfSelectedCard[i]);
        }
	}
}
