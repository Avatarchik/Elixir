//using UnityEngine;
//using System.Collections;
//using EnumsAndClasses;
//using System;

//public class ChooseTarget : MonoBehaviour {
//    GameObject selectedAlly;
//    GameObject[] selectedEnemy = new GameObject[4];
//    GameObject currentSelectedCard;
//    int countArray = 0;
//    int countAttack = 0;
//    int enemyAlive;
//    bool cardChanged = false;
//    bool attackedCritical = false;

//    public IEnumerator SelectTarget()
//    {
//        Debug.Log("SelectTarget");
//        enemyAlive = GameObject.FindGameObjectsWithTag("Monster").Length;
//        currentSelectedCard = GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard;
//        string targetType = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Target; // Distinguish number of attacks
//        string targetRange = currentSelectedCard.GetComponent<InfoCard>().Card.Card_Range;

//        if (targetType == "Ally") countAttack = 1;
//        if(targetType == "Enemy" && targetRange == "Single") countAttack = 1;
//        if (targetType == "Enemy" && targetRange == "Wide") countAttack = enemyAlive;


//        Highlight(targetType, targetRange);//Highlight units according to targettype

//        if(targetType == "Ally")
//        {
//            yield return StartCoroutine(WaitForTargetSelect(targetType));
//        }
//        if (targetType == "Enemy" && targetRange == "Single")//When target is Single
//        {
//            while (countAttack > 0)
//            {
//                yield return StartCoroutine(WaitForTargetSelect(targetType));
//            }
//        }
//        if (targetType == "Enemy" && targetRange == "Wide")//When target is Wide
//        {
//            yield return StartCoroutine(WaitForTargetSelect(targetType));
//            selectedEnemy = GameObject.FindGameObjectsWithTag("Monster"); //Add all monsters in the selectedEnemy array
//            countArray = enemyAlive;
//        }

//        if (cardChanged) //Check if the attack mode is changed in the middle of the process
//        {
//            cardChanged = false;
//            countArray = 0;
//            yield break;
//        }

//        if (targetType == "Ally")
//        {
//            TargetAlly();
//        }
//        if (targetType == "Enemy" && targetRange == "Single")//When target is Single
//        {
//            AttackEnemy();
//        }
//        if (targetType == "Enemy" && targetRange == "Wide")//When target is Wide
//        {
//            AttackEnemy();
//        }
//        if (attackedCritical)
//        {
//            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().incrementTurn();
//            Debug.Log("Turn incremented");
//			Debug.Log (GameObject.Find ("AddTurn"));
//            GameObject.Find("AddTurn").transform.Find("1More").gameObject.SetActive(true);
//            attackedCritical = false;
//        }
//        else
//        {
//            GameObject.Find("AddTurn").transform.Find("1More").gameObject.SetActive(false);
//        }
        

//        countArray = 0;// Reset the counter
//        yield return null;
//    }

//    void Highlight(string targetType, string targetRange)
//    {
//        Debug.Log("Highlight");
//        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
//        GameObject Ally = GameObject.Find("Player(Clone)");

//        //Reset first
//        Ally.transform.Find("selectable").gameObject.SetActive(false);
//        Ally.transform.Find("selected").gameObject.SetActive(false);
//        foreach (GameObject Monster in Monsters)
//        {
//            Monster.transform.Find("selectable").gameObject.SetActive(false);
//            Monster.transform.Find("selected").gameObject.SetActive(false);
//        }

//        if (targetType == "Ally")//Skill is ally heal
//        {
//            Ally.transform.Find("selectable").gameObject.SetActive(true);
//        }
//        else if (targetType == "Enemy" && targetRange == "Single")// Skill is Enemy Single attack
//        {
//            foreach(GameObject Monster in Monsters)
//            {
//                if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
//                    Monster.transform.Find("selectable").gameObject.SetActive(true);
//            }
//        }
//        else if (targetType == "Enemy" && targetRange == "Wide")// Skill is Enemy Wide attack
//        {
//            foreach (GameObject Monster in Monsters)
//            {
//                if (Monster.GetComponent<Monster>().hp > 0)//If the monster is dead, do not activate selectable
//                    Monster.transform.Find("selected").gameObject.SetActive(true);
//            }
//        }
//    }

//    IEnumerator WaitForTargetSelect(string targetType)
//    {
//        Debug.Log("WaitForTargetSelect");
//        GameObject currentCard = currentSelectedCard;
//        bool bRepeat = true;
//        while (bRepeat)
//        {
//            // Select a Target
//            if (Input.GetMouseButtonDown(0))
//            {
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

//                if (hit.collider != null && targetType == "Ally" && hit.collider.gameObject.tag == "Ally") //When the skill targets Ally, and Ally is selected
//                {
//                    selectedAlly = hit.collider.gameObject;
//                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
//                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
//                    countAttack--;
//                    bRepeat = false;
//                }
//                if (hit.collider != null && (targetType == "Enemy") && hit.collider.gameObject.tag == "Monster") //When the skill targets Enemy, and Enemy is selected
//                {

//                    //Need to verify if the selected monster is already in the array
//                    //Do it later
//                    Debug.Log("countArray: " + countArray);
//                    selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
//                    countArray++;

//                    hit.collider.gameObject.transform.Find("selectable").gameObject.SetActive(false);
//                    hit.collider.gameObject.transform.Find("selected").gameObject.SetActive(true);
//                    countAttack--;
//                    bRepeat = false;
//                }
//            }

//            if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Card ||
//                GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard != currentCard) //Other card is selected / PROBLEM!!
//            {
//                Debug.Log("Coroutine stop");
//                cardChanged = true;
//                countAttack = 0;
//                yield break;
//            }
//            yield return null;
//        }
//    }

//    void AttackEnemy()
//    {
//        Debug.Log("Attack Enemy");
//        GameObject Ally = GameObject.Find("Player(Clone)");
//        ChemicalStates criticalTarget = currentSelectedCard.GetComponent<InfoCard>().Card.Card_CriticalTarget;
//        System.Random rand = new System.Random();
//        double criticalRate;
//        double stunRate;

//        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
//        foreach (GameObject monster in monsters)
//        {
//            monster.transform.Find("selectable").gameObject.SetActive(false);
//            monster.transform.Find("selected").gameObject.SetActive(false);
//        }

//        for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
//        {
//            if(selectedEnemy[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
//            {
//                criticalRate = 1.5f;
//                attackedCritical = true;
//            }
//            else
//            {
//                criticalRate = 1f;
//            }

//            //Normal damage
//            if (currentSelectedCard.GetComponent<InfoCard>().Card.Card_AttackDamage > 0) 
//            {
//                selectedEnemy[i].GetComponent<Monster>().SetDamage((int)(currentSelectedCard.GetComponent<InfoCard>().Card.Card_AttackDamage * criticalRate));
//            }
//            //DotDamage
//            if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffName == "DoteDamage")
//            {
//                int DotDamageTurn = currentSelectedCard.GetComponent<InfoCard>().Card.Card_DotDamageTurn;
//                int DotDamage = currentSelectedCard.GetComponent<InfoCard>().Card.Card_DotDamage;
//                Debuff debuff = new Debuff(DebuffName.DoteDamage, DotDamageTurn, DotDamage);
//                selectedEnemy[i].GetComponent<Monster>().SetDamage(DotDamage);//Inflict damage immediately in this turn
//                //selectedEnemy[i].GetComponent<Monster>().AddDebuff(debuff);//Add debuff to monster
//                selectedEnemy[i].GetComponent<Monster>().AddDotDamage(debuff);//Add debuff to monster

//                //selectedEnemy[i].transform.Find("dotDamageIcon").gameObject.SetActive(true);//Activate dotDamageIcon
//            }
//            //Stun
//			if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffName=="Stun")
//			{
//                stunRate = currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffRate;
//                if (selectedEnemy[i].GetComponent<Monster>().currentChemicalState == criticalTarget)
//                {
//                    stunRate += 10f;
//                }
//                int chance = rand.Next(1, 101);
//                //int chance = 20;
//                Debug.Log("Stun Rate: " + stunRate + ", chance: " + chance);
//                if(chance <= stunRate)
//                {
//                    Debug.Log("Stun Success");
//                    Debuff debuff = new Debuff(DebuffName.DoteDamage, currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffTurn);
//                    selectedEnemy[i].GetComponent<Monster>().AddStun(debuff);
//                    //selectedEnemy[i].transform.Find("stun").gameObject.SetActive(true);
//                    //selectedEnemy[i].GetComponent<Monster>().AddDebuff (new Debuff(EnumsAndClasses.DebuffName.Stun,currentSelectedCard.GetComponent<InfoCard>().Card.Card_DebuffTurn));
//                }
//			}

//            //Check if Enemy's hp = 0
//            //If so, KILL IT!
            
//            //if (selectedEnemy[i].GetComponent<Monster>().hp == 0)
//            //{
//            //    Destroy(selectedEnemy[i]);
//            //}
            
//        }
//    }
//    void TargetAlly()
//    {
//        float criticalRate;
//        GameObject Ally = GameObject.Find("Player(Clone)");
//        ChemicalStates criticalTarget = currentSelectedCard.GetComponent<InfoCard>().Card.Card_CriticalTarget;

//        selectedAlly.transform.Find("selectable").gameObject.SetActive(false);//Unactivate all selectable/selected logos
//        selectedAlly.transform.Find("selected").gameObject.SetActive(false);

//        if (Ally.GetComponent<BaseCharacter>().currentChemicalState == criticalTarget)
//        {
//            criticalRate = 1.5f;
//            attackedCritical = true;
//        }
//        else
//        {
//            criticalRate = 1f;
//        }

//        if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_BuffName == "Dodge")
//        {
//            Buff buff = new Buff(BuffName.Dodge, currentSelectedCard.GetComponent<InfoCard>().Card.Card_BuffTurn - 1);
//            selectedAlly.GetComponent<BaseCharacter>().dodgeRate = (int)currentSelectedCard.GetComponent<InfoCard>().Card.Card_BuffRate;
//            selectedAlly.GetComponent<BaseCharacter>().AddBuff(buff);
//        }
//        if(currentSelectedCard.GetComponent<InfoCard>().Card.Card_Heal > 0)
//        {
//            selectedAlly.GetComponent<BaseCharacter>().SetHeal((int)(currentSelectedCard.GetComponent<InfoCard>().Card.Card_Heal * criticalRate));
//        }
        
//    }
//}
