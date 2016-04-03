using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefs : MonoBehaviour {
    public GameObject PlayerPrefab;

    public List<Element> party = new List<Element>();
    public baseCharacter player = new baseCharacter();
    public Element currentEquipElement;
    public int currentEquipElementIndex;
    public List<List<baseSkill>> skillList = new List<List<baseSkill>>();
    

	// Use this for initialization
	public void Initialize () {
        List<Element> elementList = GetComponent<Loader>().elementList;
 
        //Temporarily choose equipped elements

        party.Add(CloneElement(elementList[0]));
        party.Add(CloneElement(elementList[1]));
        party.Add(CloneElement(elementList[2]));
        party.Add(CloneElement(elementList[3]));

        //Create list of skills of each element in party
        List<baseSkill> cardList = GetComponent<SkillLoader>().skillList;
        for (int i = 0; i < party.Count; i++)
        {
            List<baseSkill> elementSkill = new List<baseSkill>();
            for(int j = 0; j < 3; j++)
            {
                
                elementSkill.Add(cardList.Find(x => x.Skill_Name == party[i].elementCard1));
                elementSkill.Add(cardList.Find(x => x.Skill_Name == party[i].elementCard2));
                elementSkill.Add(cardList.Find(x => x.Skill_Name == party[i].elementCard3));
            }
            skillList.Add(elementSkill);
        }

        //Initially equipped element
        currentEquipElementIndex = 0;
        currentEquipElement = party[currentEquipElementIndex];

        //Initialize Player info
        player.Initialize();
        player.HP = player.MAX_HP;
        player.dodgeRate = 0;
        player.level = 7;
        player.AttackDamage = 10;

        SetPlayerInfo();

        GeneratePlayer();
    }

    public Element CloneElement(Element original)
    {
        Element temp = new Element();
        temp.id = original.id;
        temp.extName = original.extName;
        temp.name = original.name;
        temp.chemicalSeries = original.chemicalSeries;
		temp.elementNumber = original.elementNumber;
		temp.properLevel = original.properLevel;
        temp.desription = original.desription;
		temp.meltingPoint = original.meltingPoint;
		temp.boilingPoint = original.boilingPoint;
        temp.characterRoomTempState = original.characterRoomTempState;
        temp.solidGauge = original.solidGauge;
        temp.liquidGauge = original.liquidGauge;
        temp.gasGauge = original.gasGauge;
        temp.roomTempPos = original.roomTempPos;
        temp.elementCard1 = original.elementCard1;
        temp.elementCard2 = original.elementCard2;
        temp.elementCard3 = original.elementCard3;
		temp.weakPoint = original.weakPoint;
        return temp;
}

    public void GeneratePlayer()
    {
		Vector3 PlayerPosition = new Vector3(-2.7f, .8f, 0);
        GameObject Player = Instantiate(PlayerPrefab);
        Player.transform.position = PlayerPosition;
    }

    public void SetPlayerInfo()
    {
        //Change player's stats according to current element
		player.weakPoint = currentEquipElement.weakPoint;
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
