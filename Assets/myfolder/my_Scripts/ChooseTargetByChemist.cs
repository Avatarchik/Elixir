using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChooseTargetByChemist : MonoBehaviour {
    BaseCharacter player;
    ChoosingManager choosingManager;
    GameObject selectedEnemy;
    ChemistSkills currentChemistSkill;
    bool cardChanged = false;
    bool isTargetEnemy;

    public IEnumerator SelectTarget()
    {
        Debug.Log("SelectTarget");
        player = GetComponent<PlayerPrefs>().player;
        choosingManager = GetComponent<ChoosingManager>();

        currentChemistSkill = GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill;
        Highlight();
        yield return StartCoroutine(WaitForTargetSelect());

        if (cardChanged) //Check if the attack mode is changed in the middle of the process
        {
            cardChanged = false;
            yield break;
        }
        if (isTargetEnemy)
        {
            AttackEnemy();
        }
        else
        {
            AttackAlly();
        }
        
    }
    void Highlight()
    {
        Debug.Log("Highlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject Ally = GameObject.Find("Player(Clone)");

        //Reset first
        Ally.transform.Find("selectable").gameObject.SetActive(false);
        Ally.transform.Find("selected").gameObject.SetActive(false);
        foreach (GameObject Monster in Monsters)
        {
            Monster.transform.Find("selectable").gameObject.SetActive(false);
            Monster.transform.Find("selected").gameObject.SetActive(false);
        }
        //Highlight
        Ally.transform.Find("selectable").gameObject.SetActive(true);
        foreach (GameObject Monster in Monsters)
        {
            if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
                Monster.transform.Find("selectable").gameObject.SetActive(true);
        }
    }
    IEnumerator WaitForTargetSelect()
    {
        Debug.Log("WaitForTargetSelect");
        bool bRepeat = true;
        while (bRepeat)
        {
            // Select a Target
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null && hit.collider.gameObject.tag == "Monster") //When the skill targets Enemy, and Enemy is selected
                {
                    selectedEnemy = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array

                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
                    bRepeat = false;
                    isTargetEnemy = true;
                }
                if (hit.collider != null && hit.collider.gameObject.tag == "Ally") //When the skill targets Ally
                {
                    GameObject Ally = GameObject.Find("Player(Clone)");
                    Ally.transform.Find("selectable").gameObject.SetActive(false);
                    Ally.transform.Find("selected").gameObject.SetActive(false);

                    bRepeat = false;
                    isTargetEnemy = false;
                }
            }

            if (choosingManager.AttackMode != AttackMode.Chemist ||
                choosingManager.SelectedChemistSkill != currentChemistSkill) //Other card is selected / PROBLEM!!
            {
                Debug.Log("(Chemist)Coroutine stop");
                cardChanged = true;
                yield break;
            }
            yield return null;
        }
    }
    void AttackEnemy()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
        foreach (GameObject monster in monsters)
        {
            monster.transform.Find("selectable").gameObject.SetActive(false);
            monster.transform.Find("selected").gameObject.SetActive(false);
        }
        //Increase or Decrease enemy ChemicalStateValue
        if (currentChemistSkill == ChemistSkills.Cool)
        {
            selectedEnemy.GetComponent<Monster>().DecrementCSVal();
        }
        else if(currentChemistSkill == ChemistSkills.Heat)
        {
            selectedEnemy.GetComponent<Monster>().IncrementCSVal();
        }
        
    }
    void AttackAlly()
    {
        GameObject Ally = GameObject.Find("Player(Clone)");
        Ally.transform.Find("selectable").gameObject.SetActive(false);
        Ally.transform.Find("selected").gameObject.SetActive(false);

        //Increase or Decrease enemy ChemicalStateValue
        if (currentChemistSkill == ChemistSkills.Cool)
        {
            player.DecrementCSVal();
        }
        else if (currentChemistSkill == ChemistSkills.Heat)
        {
            player.IncrementCSVal();
        }
    }
}
