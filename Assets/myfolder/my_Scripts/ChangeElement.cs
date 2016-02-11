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

    private float timer;
    private bool checkPress = false;
    private int elementIndex;

    public void Initialize()
    {
        playerPrefs = GameObject.Find("GameManager").GetComponent<PlayerPrefs>();
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

    public void ElementChanged(int index)
    {
        playerPrefs.currentEquipElementIndex = arrangeList[index];
        playerPrefs.currentEquipElement = playerPrefs.party[playerPrefs.currentEquipElementIndex];
        playerPrefs.SetPlayerInfo();
        GameObject.Find("GameManager").GetComponent<PlayerTurn>().GetSkills();

        TurnBasedCombatStateMachine TBSMachine = GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>();
        TBSMachine.decrementTurn();
        if (TBSMachine.isTurnExhausted())
        {
            TBSMachine.currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
            TBSMachine.resetTurn();
        }
    }

    public void DetailPanelInfo(int index)
    {
        DetailPanel.SetActive(true);

        List<baseCard> cardDatabase = GameObject.Find("GameManager").GetComponent<CardLoad>().cardDeck;

        baseCard card1 = cardDatabase.Find(x => x.Card_Name == playerPrefs.party[arrangeList[index]].elementCard1);
        baseCard card2 = cardDatabase.Find(x => x.Card_Name == playerPrefs.party[arrangeList[index]].elementCard2);
        baseCard card3 = cardDatabase.Find(x => x.Card_Name == playerPrefs.party[arrangeList[index]].elementCard3);

        DetailLabel.text = playerPrefs.party[arrangeList[index]].extName + " 스킬 정보";

        string imagePath1 = "SkillIcons/" + card1.Card_Name;
        string imagePath2 = "SkillIcons/" + card2.Card_Name;
        string imagePath3 = "SkillIcons/" + card3.Card_Name;

        DetailSkill1.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath1, typeof(Sprite)) as Sprite;
        DetailSkill1.transform.FindChild("Name").GetComponent<Text>().text = card1.Card_ExtName;
        DetailSkill1.transform.FindChild("Description").GetComponent<Text>().text = card1.Card_Description;

        DetailSkill2.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath2, typeof(Sprite)) as Sprite;
        DetailSkill2.transform.FindChild("Name").GetComponent<Text>().text = card2.Card_ExtName;
        DetailSkill2.transform.FindChild("Description").GetComponent<Text>().text = card2.Card_Description;

        DetailSkill3.transform.FindChild("SkillImage").GetComponent<Image>().sprite = Resources.Load(imagePath3, typeof(Sprite)) as Sprite;
        DetailSkill3.transform.FindChild("Name").GetComponent<Text>().text = card3.Card_ExtName;
        DetailSkill3.transform.FindChild("Description").GetComponent<Text>().text = card3.Card_Description;

    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
