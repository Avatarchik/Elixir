using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class AnalyzeMonster : MonoBehaviour
{
    GameObject selectedEnemy;
    ChemistSkills currentChemistSkill;
    bool cardChanged = false;

    public IEnumerator SelectTarget()
    {
        currentChemistSkill = GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill;
        Highlight();
        yield return StartCoroutine(WaitForTargetSelect());

        if (cardChanged) //Check if the attack mode is changed in the middle of the process
        {
            cardChanged = false;
            yield break;
        }

        AnalyzeEnemy();
    }
    void Highlight()
    {
        Debug.Log("Highlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");

        //Reset first
        foreach (GameObject Monster in Monsters)
        {
            Monster.transform.Find("selectable").gameObject.SetActive(false);
            Monster.transform.Find("selected").gameObject.SetActive(false);
        }
        //Highlight
        foreach (GameObject Monster in Monsters)
        {
            if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
                Monster.transform.Find("selectable").gameObject.SetActive(true);
        }
    }
    IEnumerator WaitForTargetSelect()
    {
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
                }
            }

            if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Chemist ||
                GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill != currentChemistSkill) //Other card is selected / PROBLEM!!
            {
                Debug.Log("Coroutine stop");
                cardChanged = true;
                yield break;
            }
            yield return null;
        } 
    }

    void AnalyzeEnemy()
    {
        Debug.Log("Analyze Enemy");
    }
}
