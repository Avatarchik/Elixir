using UnityEngine;
using System.Collections;

public class ChooseEnemy : MonoBehaviour {
    public GameObject selectable;
    GameObject[] selectedEnemy = new GameObject[4];
    int countArray = 0;
    int countAttack = 0;
    bool cardChanged = false;

    //public IEnumerator SelectEnemy()
    //{
    //    CardReselect:
    //    GameObject cardObject = GetComponent<ChoosingManager>().SelectedCard;
    //    string targetType = cardObject.GetComponent<InfoCard>().Card.Card_Target; // Distinguish number of attacks

    //    if (targetType == 2) countAttack = 2;
    //    if (targetType == 4) countAttack = 4;

    //    HighlightEnemy();

    //    if (targetType == 2)//When target is Single
    //    {
    //        while(countAttack > 0)
    //        {
    //            yield return StartCoroutine(WaitForEnemySelect(cardObject));
    //            if(cardChanged == true) //Check if card is changed. If changed, restart the coroutine
    //            {
    //                cardChanged = false;
    //                Debug.Log("Goto Activate");
    //                goto CardReselect;
    //            }
    //        }
    //        AttackEnemy(cardObject);
    //    }

    //    if(targetType == 4)//When target is Wide
    //    {
    //        yield return StartCoroutine(WaitForEnemySelect(cardObject));
    //        if (cardChanged == true) //Check if card is changed. If changed, restart the coroutine
    //        {
    //            cardChanged = false;
    //            Debug.Log("Goto Activate");
    //            goto CardReselect;
    //        }
    //        selectedEnemy = GameObject.FindGameObjectsWithTag("Monster"); //Add all monsters in the selectedEnemy array
    //        countArray = 4;
    //        AttackEnemy(cardObject);
    //    }

    //    countArray = 0;// Reset the counter
    //    GetComponent<ChoosingManager>().SelectedCard = null;// Reset ChoosingManager
    //    yield return null;
    //}

    void HighlightEnemy()
    {
        Debug.Log("Hightlight");
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject Monster in Monsters)
        {
            if (Monster.GetComponent<Monster>().hp > 0)
            {
                GameObject Selectable = (GameObject)Instantiate(selectable);
                Selectable.transform.parent = Monster.transform;
                Selectable.transform.position = Monster.transform.position;
            }
        }
    }
    IEnumerator WaitForEnemySelect(GameObject cardObject)
    {
        bool bRepeat = true;
        while (bRepeat)
        {
            // Select a Monster
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null && hit.collider.gameObject.tag == "Monster")
                {

                    //Need to verify if the selected monster is already in the array
                    //Nedd to verify if the selected monster is already dead
                    //Do it later
                    selectedEnemy[countArray] = hit.collider.gameObject;//Add the selected monster in the selectedEnemy array
                    countArray++;
                    GameObject selectableToDestroy = hit.collider.gameObject.transform.Find("selectable(Clone)").gameObject; //Find the selectable of selected gameobject
                    Destroy(selectableToDestroy); //Destroy that selectable

                    countAttack--;
                    bRepeat = false;
                }
            }

            if (GetComponent<ChoosingManager>().SelectedCard != cardObject) //Other card is selected / PROBLEM!!
            {
                Debug.Log("Card changed");
                GameObject[] selectables = GameObject.FindGameObjectsWithTag("selectable");
                foreach (GameObject selectable in selectables)
                {
                    Destroy(selectable);
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

    void AttackEnemy(GameObject cardObject)
    {
        Debug.Log("Attack Enemy");

        GameObject[] selectables = GameObject.FindGameObjectsWithTag("selectable");
        foreach (GameObject selectable in selectables)
        {
            Destroy(selectable);
        }

       /* for (int i = 0; i < countArray; i++) //Repeat procedure for every selected enemies listed in selectedEnemy array
        {
            if(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage > 0) //HP percent damage
            {
                if (((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage)/100) * selectedEnemy[i].GetComponent<Monster>().maxHp >=
                    cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage) //If the final damage is greater than the threshold damage
                {
                    selectedEnemy[i].GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage);
                }
                else //If the final damage is less than the threshold damage
                {
                    selectedEnemy[i].GetComponent<Monster>().SetDamage(((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage)/100) * selectedEnemy[i].GetComponent<Monster>().maxHp);
                }
            }
            if (cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage > 0) //normal damage
            {
                selectedEnemy[i].GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage);
            }
            //DotDamage
            //Stun
        }*/


    }

	
    //public GameObject selectable;
    //GameObject selectedEnemy;


    //public IEnumerator SelectEnemy(GameObject cardObject){.
    //	Debug.Log ("SelectEnemy");
    //       int targetType = cardObject.GetComponent<InfoCard>().Card.Card_Target;
    //       selectedEnemy = null;

    //       HighlightEnemy ();

    //	yield return StartCoroutine (WaitForEnemySelect (cardObject));
    //	//if(selectedEnemy!=null)
    //	AttackEnemy (cardObject, selectedEnemy);
    //}

    //IEnumerator WaitForEnemySelect(GameObject cardObject){
    //	while (true) {
    //	    // Select a Monster
    //		if (Input.GetMouseButtonDown (0)) {
    //			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

    //			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
    //			if (hit.collider!=null&&hit.collider.gameObject.tag == "Monster") {
    //				GameObject[] selectables = GameObject.FindGameObjectsWithTag ("selectable");
    //				selectedEnemy=hit.collider.gameObject;
    //				foreach (GameObject selectable in selectables) {
    //					Destroy (selectable);
    //				}
    //				Debug.Log (hit.collider.gameObject);
    //				break;

    //			}else{
    //				Debug.Log ("Nothing");
    //			}
    //		}
    //           // Destroy all "Selectables"
    //		if (GetComponent<ChoosingManager>().SelectedCard!=cardObject) {
    //			Debug.Log("changed");
    //			GameObject[] Monsters = GameObject.FindGameObjectsWithTag ("Monster");
    //			foreach (GameObject Monster in Monsters) {
    //				Destroy (Monster.transform.FindChild ("selectable(Clone)").gameObject);
    //			}
    //			break;
    //		}

    //		yield return null;
    //	}
    //}

    //void HighlightEnemy(){
    //	Debug.Log ("Hightlight");
    //	GameObject[] Monsters=GameObject.FindGameObjectsWithTag ("Monster");
    //	foreach (GameObject Monster in Monsters) {
    //           if(Monster.GetComponent<Monster>().hp > 0)
    //           {
    //               GameObject Selectable = (GameObject)Instantiate (selectable);
    //		    Selectable.transform.parent = Monster.transform;
    //		    Selectable.transform.position = Monster.transform.position;
    //           }
    //	}
    //}

    //   void AttackEnemy(GameObject cardObject, GameObject SelectedEnemy)
    //   {
    //       if (cardObject.GetComponent<InfoCard>().Card.Card_Target == 4)
    //       {

    //           GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");

    //           if (cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage > 0)
    //           {
    //               foreach (GameObject Monster in Monsters)
    //               {
    //                   Monster.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage);
    //                   Debug.Log(SelectedEnemy.GetComponent<Monster>().hp);
    //               }
    //           }
    //           else if (cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage > 0)
    //           {

    //               foreach (GameObject Monster in Monsters)
    //               {
    //                   if ((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage) * Monster.GetComponent<Monster>().maxHp / 100 >= cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage)
    //                   {
    //                       Monster.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage);
    //                   }
    //                   else
    //                   {
    //                       Monster.GetComponent<Monster>().SetDamage((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage) * SelectedEnemy.GetComponent<Monster>().maxHp / 100);
    //                   }
    //               }
    //           }
    //       }
    //       else if (cardObject.GetComponent<InfoCard>().Card.Card_Target == 2)
    //       {
    //           if (cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage > 0)
    //           {
    //               SelectedEnemy.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Attack_Damage);
    //               Debug.Log(SelectedEnemy.GetComponent<Monster>().hp);
    //           }
    //           else if (cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage > 0)
    //           {
    //               if ((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage) * SelectedEnemy.GetComponent<Monster>().maxHp / 100 >= cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage)
    //               {
    //                   SelectedEnemy.GetComponent<Monster>().SetDamage((int)cardObject.GetComponent<InfoCard>().Card.Card_Max_Damage);
    //               }
    //               else
    //               {
    //                   SelectedEnemy.GetComponent<Monster>().SetDamage((int)(cardObject.GetComponent<InfoCard>().Card.Card_HP_Damage) * SelectedEnemy.GetComponent<Monster>().maxHp / 100);
    //               }
    //           }
    //       }
    //       GetComponent<ChoosingManager>().SelectedCard = null;
    //   }

}
