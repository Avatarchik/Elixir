using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefs : MonoBehaviour {
    public GameObject PlayerPrefab;

    public List<Element> party = new List<Element>();
    public Element currentEquipElement;
    public int currentEquipElementIndex;
    public List<List<baseSkill>> skillList = new List<List<baseSkill>>();
    public baseCharacter player = new baseCharacter();

	// Use this for initialization
	public void Initialize () {
        List<Element> elementList = GetComponent<Loader>().elementList;

        //Temporarily choose equipped elements
        party.Add(elementList[0]);
        party.Add(elementList[0]);
        party.Add(elementList[1]);
        party.Add(elementList[1]);

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
        player.HP = player.MAX_HP;
        player.dodgeRate = 0;
        player.level = 7;
        player.AttackDamage = 10;

        SetPlayerInfo();

        GeneratePlayer();
    }

    public void GeneratePlayer()
    {
        Vector3 PlayerPosition = new Vector3(-4.36f, 1.35f, 0);
        GameObject Player = Instantiate(PlayerPrefab);
        Player.transform.position = PlayerPosition;
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
