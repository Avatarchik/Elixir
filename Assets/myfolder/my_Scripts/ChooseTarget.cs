using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChooseTarget : MonoBehaviour {
    GameObject selectedAlly;
    GameObject[] selectedEnemy = new GameObject[4];
    GameObject currentSelectedCard;
    int countArray = 0;
    int countAttack = 0;
    int enemyAlive;
    bool cardChanged = false;

    public IEnumerator SelectTarget()
    {
        Debug.Log("SelectTarget");
        enemyAlive = GameObject.FindGameObjectsWithTag("Monster").Length;
        currentSelectedCard = GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard;
        string targetType = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Target; // Distinguish number of attacks
        string targetRange = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Range;

        if (targetType == "Ally") countAttack = 1;
        if(targetType == "Enemy" && targetRange == "Single") countAttack = 1;
        if (targetType == "Enemy" && targetRange == "Wide") countAttack = enemyAlive;


        Highlight(targetType, targetRange);//Highlight units according to targettype

        if(targetType == "Ally")
        {
            yield return StartCoroutine(WaitForTargetSelect(targetType));
            if (cardChanged)
            {
                cardChanged = false;
                yield break;
            }
            HealAlly();
        }
        if (targetType == "Enemy" && targetRange == "Single")//When target is Single
        {
            while (countAttack > 0)
            {
                yield return StartCoroutine(WaitForTargetSelect(targetType));
                if (cardChanged)
                {
                    cardChanged = false;
                    yield break;
                }
            }
            AttackEnemy();
        }
        if (targetType == "Enemy" && targetRange == "Wide")//When target is Wide
        {
            yield return StartCoroutine(WaitForTargetSelect(targetType));
            if (cardChanged)
            {
                cardChanged = false;
                yield break;
            }
            selectedEnemy = GameObject.FindGameObjectsWithTag("Monster"); //Add all monsters in the selectedEnemy array
            countArray = enemyAlive;
            AttackEnemy();
        }
        
        countArray = 0;// Reset the counter
        //GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard = null;// Reset ChoosingManager
        yield return null;
    }

    void Highlight(string targetType, string targetRange)
    {
        Debug.Log("Highlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject Ally = GameObject.Find("Player(Clone)");

        if (targetType == "Ally")//Skill is ally heal
        {
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
        Debug.Log("WaitForTargetSelect");
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
                    //Do it later
                    selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    countArray++;

                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
                    countAttack--;
                    bRepeat = false;
                }
            }

            if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Card ||
                GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard != currentCard) //Other card is selected / PROBLEM!!
            {
                Debug.Log("Coroutine stop");
                cardChanged = true;
                yield break;
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
            if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffName == "DoteDamage")
            {
                int debuffTurn = currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffTurn;
                int debuffDamage = currentSelectedCard.GetComponent<InfoCard>().Card.Card_DotDamage;
                Debuff debuff = new Debuff(DebuffName.DoteDamage, debuffTurn, debuffDamage);
                selectedEnemy[i].GetComponent<Monster>().SetDamage(debuffDamage);//Inflict damage immediately in this turn
                selectedEnemy[i].GetComponent<Monster>().AddDebuff(debuff);//Add debuff to monster
            }
            //Stun
			if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffName=="Stun")
			{
				selectedEnemy[i].GetComponent<Monster>().AddDebuff (new Debuff(EnumsAndClasses.DebuffName.Stun,currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffTurn));
			}

            //Check if Enemy's hp = 0
            //If so, KILL IT!
            if (selectedEnemy[i].GetComponent<Monster>().hp == 0)
            {
                Destroy(selectedEnemy[i]);
            }
        }
    }
    void HealAlly()
    {
        selectedAlly.transform.Find("selectable").gameObject.SetActive(false);//Unactivate all selectable/selected logos
        selectedAlly.transform.Find("selected").gameObject.SetActive(false);

        selectedAlly.GetComponent<BaseCharacter>().SetHeal((int)currentSelectedCard.GetComponent<InfoCard>().Card.Card_Heal);
    }
}
