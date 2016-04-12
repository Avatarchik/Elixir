using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangeElement : MonoBehaviour {
    public GameObject DetailPanel;
    public Text DetailLabel;
    public GameObject DetailSkill1;
    public GameObject DetailSkill2;
    public GameObject DetailSkill3;

    public List<GameObject> ElementPanel;

    private List<int> arrangeList;
    private PlayerPrefs playerPrefs;
    private MonsterPrefs monsterPrefs;

    private float timer;
    private bool checkPress = false;
    private int elementIndex;

    public void Initialize()
    {
        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();
        monsterPrefs = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>();
        int currentEquipped = playerPrefs.currentEquipElementIndex;
        List<Element> elementsInParty = playerPrefs.party;
        arrangeList = new List<int>();
        
        for(int i = 0; i < 4; i++)
        {
            if(i != currentEquipped)
            {
                arrangeList.Add(i);
            }
        }

        Debug.Log("Load Elements on each panel");

        //1st panel displays currnetly equipped element
        ElementPanel[0].transform.GetChild(0).GetComponent<Text>().text = elementsInParty[currentEquipped].extName;
        ElementPanel[0].transform.GetChild(1).GetComponent<ThermoBar>().GetData(elementsInParty, currentEquipped);
        
        for(int j = 0; j < 3; j++)
        {
            ElementPanel[j+1].transform.GetChild(0).GetComponent<Text>().text = elementsInParty[arrangeList[j]].extName;
            ElementPanel[j+1].transform.GetChild(1).GetComponent<ThermoBar>().GetData(elementsInParty, arrangeList[j]);
        }

    }

    void Update()
    {
        if (checkPress)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                DetailPanelInfo(elementIndex);
                checkPress = false;
            }
        }
    }

    public void CheckLongPress(int index)
    {
        checkPress = true;
        timer = 1.0f;
        elementIndex = index;
    }

    public void PressUp()
    {
        checkPress = false;
    }

    public void UnHighlight()
    {
        GameObject.FindGameObjectWithTag("Ally").transform.Find("selectable").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Ally").transform.Find("selected").gameObject.SetActive(false);
        foreach(GameObject monster in monsterPrefs.monsterObjectList)
        {
            monster.transform.Find("selectable").gameObject.SetActive(false);
            monster.transform.Find("selected").gameObject.SetActive(false);
        }
    }

    public void ElementChanged(int index)
    {
        playerPrefs.currentEquipElementIndex = arrangeList[index];
        playerPrefs.currentEquipElement = playerPrefs.party[playerPrefs.currentEquipElementIndex];
        playerPrefs.SetPlayerInfo();
        GameObject.Find("GameManager").GetComponent<PlayerTurn>().GetSkills();

        TurnBasedCombatStateMachine TBSMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();

        GameObject.Find("Button").GetComponent<ChemistSkill>().DisableButtons();

		if (TBSMachine.GetChemicalSkillCount () == 0)
			TBSMachine.decrementTurn ();
		else 
		{
			TBSMachine.decrementChemistSkillCount ();
			GameObject.Find ("Button").GetComponent<ChemistSkill> ().InteractOffAllButton ();
		}

        if (TBSMachine.isTurnExhausted())
        {
            TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            TBSMachine.resetTurn();
			TBSMachine.resetChemistSkillCount ();
            for(int i = 0; i < monsterPrefs.monsterList.Count; i++)
            {
                monsterPrefs.monsterList[i].guarded = false;
                monsterPrefs.monsterObjectList[i].transform.Find("guardIcon").gameObject.SetActive(false);
            }
        }
    }

    public void DetailPanelInfo(int index)
    {
        DetailPanel.SetActive(true);

        List<baseSkill> cardDatabase = GameObject.Find("GameManager").GetComponent<SkillLoader>().skillList;

        baseSkill skill1 = cardDatabase.Find(x => x.Skill_Name == playerPrefs.party[arrangeList[index]].elementCard1);
        baseSkill skill2 = cardDatabase.Find(x => x.Skill_Name == playerPrefs.party[arrangeList[index]].elementCard2);
        baseSkill skill3 = cardDatabase.Find(x => x.Skill_Name == playerPrefs.party[arrangeList[index]].elementCard3);

        DetailLabel.text = playerPrefs.party[arrangeList[index]].extName + " 스킬 정보";

        string imagePath1 = "SkillIcons/" + skill1.Skill_Name;
        string imagePath2 = "SkillIcons/" + skill2.Skill_Name;
        string imagePath3 = "SkillIcons/" + skill3.Skill_Name;

        DetailSkill1.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath1, typeof(Sprite)) as Sprite;
        DetailSkill1.transform.FindChild("Name").GetComponent<Text>().text = skill1.Skill_ExtName;
        DetailSkill1.transform.FindChild("Description").GetComponent<Text>().text = skill1.Skill_Description;

        DetailSkill2.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath2, typeof(Sprite)) as Sprite;
        DetailSkill2.transform.FindChild("Name").GetComponent<Text>().text = skill2.Skill_ExtName;
        DetailSkill2.transform.FindChild("Description").GetComponent<Text>().text = skill2.Skill_Description;

        DetailSkill3.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath3, typeof(Sprite)) as Sprite;
        DetailSkill3.transform.FindChild("Name").GetComponent<Text>().text = skill3.Skill_ExtName;
        DetailSkill3.transform.FindChild("Description").GetComponent<Text>().text = skill3.Skill_Description;

    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
