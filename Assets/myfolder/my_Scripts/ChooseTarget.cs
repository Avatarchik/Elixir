using UnityEngine;
using System.Collections;

public class ChooseTarget : MonoBehaviour {
    GameObject selectedAlly;
    GameObject[] selectedEnemy = new GameObject[4];
    GameObject currentSelectedCard;
    int countArray = 0;
    int countAttack = 0;
    bool cardChanged = false;

    public IEnumerator SelectTarget()
    {
        CardReselect:
        currentSelectedCard = GetComponent<ChoosingManager>().SelectedCard;
        string targetType = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Target; // Distinguish number of attacks
        string targetRange = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Range;

        Debug.Log("targetType: " + targetType + " targetRange: " + targetRange);
        if (targetType == "Ally") countAttack = 1;
        if(targetType == "Enemy" && targetRange == "Single") countAttack = 1;
        if (targetType == "Enemy" && targetRange == "Wide") countAttack = 4;


        Highlight(targetType, targetRange);//Highlight units according to targettype

        if(targetType == "Ally")
        {
            yield return StartCoroutine(WaitForTargetSelect(targetType));
            if (cardChanged == true) //Check if card is changed. If changed, restart the coroutine
            {
                cardChanged = false;
                Debug.Log("Goto Activate");
                goto CardReselect;
            }
            HealAlly();
        }
        if (targetType == "Enemy" && targetRange == "Single")//When target is Single
        {
            while (countAttack > 0)
            {
                yield return StartCoroutine(WaitForTargetSelect(targetType));
                if (cardChanged == true) //Check if card is changed. If changed, restart the coroutine
                {
                    cardChanged = false;
                    Debug.Log("Goto Activate");
                    goto CardReselect;
                }
            }
            AttackEnemy();
        }
        if (targetType == "Enemy" && targetRange == "Wide")//When target is Wide
        {
            yield return StartCoroutine(WaitForTargetSelect(targetType));
            if (cardChanged == true) //Check if card is changed. If changed, restart the coroutine
            {
                cardChanged = false;
                Debug.Log("Goto Activate");
                goto CardReselect;
            }
            selectedEnemy = GameObject.FindGameObjectsWithTag("Monster"); //Add all monsters in the selectedEnemy array
            countArray = 4;
            AttackEnemy();
        }

        countArray = 0;// Reset the counter
        GetComponent<ChoosingManager>().SelectedCard = null;// Reset ChoosingManager
        yield return null;
    }

    void Highlight(string targetType, string targetRange)
    {
        Debug.Log("Hightlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject Ally = GameObject.Find("Player(Clone)");

        if (targetType == "Ally")//Skill is ally heal
        {
            Debug.Log(Ally);
            Ally.transform.Find("selectable").gameObject.SetActive(true);
        }
        else if (targetType == "Enemy" && targetRange == "Single")// Skill is Enemy Single attack
        {
            foreach(GameObject Monster in Monsters)
            {
                if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
                    Monster.transform.Find("selectable").gameObject.SetActive(true);
            }
        }
        else if (targetType == "Enemy" && targetRange == "Wide")// Skill is Enemy Wide attack
        {
            foreach (GameObject Monster in Monsters)
            {
                if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
                    Monster.transform.Find("selected").gameObject.SetActive(true);
            }
        }
    }

    IEnumerator WaitForTargetSelect(string targetType)
    {
        GameObject currentCard = currentSelectedCard;
        bool bRepeat = true;
        while (bRepeat)
        {
            // Select a Target
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null && targetType == "Ally" && hit.collider.gameObject.tag == "Ally") //When the skill targets Ally, and Ally is selected
                {
                    selectedAlly = hit.collider.gameObject;
                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
                    countAttack--;
                    bRepeat = false;
                }
                if (hit.collider != null && (targetType == "Enemy") && hit.collider.gameObject.tag == "Monster") //When the skill targets Enemy, and Enemy is selected
                {

                    //Need to verify if the selected monster is already in the array
                    //Nedd to verify if the selected monster is already dead
                    //Do it later
                    selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    countArray++;

                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
                    countAttack--;
                    bRepeat = false;
                }
            }

            if (GetComponent<ChoosingManager>().SelectedCard != currentCard) //Other card is selected / PROBLEM!!
            {
                Debug.Log("Card changed");

                GameObject ally = GameObject.Find("Player(Clone)");
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
                ally.transform.Find("selectable").gameObject.SetActive(false);
                ally.transform.Find("selectable").gameObject.SetActive(false);
                foreach (GameObject monster in monsters)
                {
                    monster.transform.Find("selectable").gameObject.SetActive(false);
                    monster.transform.Find("selected").gameObject.SetActive(false);
                }

                //Reset variables of coroutine
                countArray = 0;
                cardChanged = true;
                bRepeat = false;
                break;
            }

            yield return null;
        }
    }

    void AttackEnemy()
    {
        Debug.Log("Attack Enemy");

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
        foreach (GameObject monster in monsters)
        {
            monster.transform.Find("selectable").gameObject.SetActive(false);
            monster.transform.Find("selected").gameObject.SetActive(false);
        }

        for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
        {
            //Normal damage
            if (currentSelectedCard.GetComponent<InfoCard>().Card.Card_AttackDamage > 0) 
            {
                selectedEnemy[i].GetComponent<Monster>().SetDamage((int)currentSelectedCard.GetComponent<InfoCard>().Card.Card_AttackDamage);
            }
            //DotDamage
            //Stun
        }
    }
    void HealAlly()
    {
        selectedAlly.transform.Find("selectable").gameObject.SetActive(false);//Unactivate all selectable/selected logos
        selectedAlly.transform.Find("selected").gameObject.SetActive(false);

        selectedAlly.GetComponent<BaseCharacter>().SetHeal((int)currentSelectedCard.GetComponent<InfoCard>().Card.Card_Heal);
    }
}
