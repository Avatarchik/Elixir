using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefs : MonoBehaviour {
    public List<Element> party = new List<Element>();
    public Element currentEquipElement;
    public int currentEquipElementIndex;
    public List<List<baseCard>> skillList = new List<List<baseCard>>();
    public BaseCharacter player = new BaseCharacter();

	// Use this for initialization
	public void Initialize () {
        List<Element> elementList = GetComponent<Loader>().elementList;

        //Temporarily choose equipped elements
        party.Add(elementList[0]);
        party.Add(elementList[0]);
        party.Add(elementList[1]);
        party.Add(elementList[1]);

        //Create list of skills of each element in party
        List<baseCard> cardList = GetComponent<CardLoad>().cardDeck;
        for (int i = 0; i < party.Count; i++)
        {
            List<baseCard> elementSkill = new List<baseCard>();
            for(int j = 0; j < 3; j++)
            {
                
                elementSkill.Add(cardList.Find(x => x.Card_Name == party[i].elementCard1));
                elementSkill.Add(cardList.Find(x => x.Card_Name == party[i].elementCard2));
                elementSkill.Add(cardList.Find(x => x.Card_Name == party[i].elementCard3));
            }
            skillList.Add(elementSkill);
        }

        //Initially equipped element
        currentEquipElementIndex = 0;
        currentEquipElement = party[currentEquipElementIndex];

        //Initialize Player info
        player.HP = player.MAX_HP;
        player.dodgeRate = 0;

        SetPlayerInfo();
    }
	
    public void SetPlayerInfo()
    {
        //Change player's stats according to current element
        player.currentChemicalState = currentEquipElement.characterRoomTempState;
        player.currentChemicalStateValue = currentEquipElement.roomTempPos;

        player.solidStateValue = currentEquipElement.solidGauge;
        player.liquidStateValue = currentEquipElement.liquidGauge;
        player.gasStateValue = currentEquipElement.gasGauge;
    }

    public void ChangeElement(int index)
    {
        currentEquipElement = party[index];
        SetPlayerInfo();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
