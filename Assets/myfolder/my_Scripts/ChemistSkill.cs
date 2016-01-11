using UnityEngine;
using System.Collections;
using EnumsAndClasses;

public class ChemistSkill : MonoBehaviour {
    private bool isActive;
    GameObject currentChemistSkill;

	// Use this for initialization
	void Start () {
        isActive = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.gameObject.tag == "ChemistSkill")
            {
                Debug.Log("ChemistSkillActivated");
                GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode = AttackMode.Chemist;
                GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedCard = null;
                GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill = hit.collider.gameObject;
                currentChemistSkill = hit.collider.gameObject;
                StartCoroutine(cardActivated());
            }
        }
    }

    void OnMouseDown()
    {
        if (!isActive)
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(true);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(true);
            isActive = true;
        }
        else
        {
            gameObject.transform.Find("CoolIcon").gameObject.SetActive(false);
            gameObject.transform.Find("HeatIcon").gameObject.SetActive(false);
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");//Unactivate all selectable/selected logos
            foreach (GameObject monster in monsters)
            {
                monster.transform.Find("selectable").gameObject.SetActive(false);
                monster.transform.Find("selected").gameObject.SetActive(false);
            }
            isActive = false;
        }
    }

    IEnumerator cardActivated()
    {
        yield return StartCoroutine(gameObject.GetComponent<ChooseTargetByChemist>().SelectTarget());

        Debug.Log("Before Coroutine Stopped");
        //If Attack method is changed during the procedure, dismiss all actions
        if (GameObject.Find("GameManager").GetComponent<ChoosingManager>().AttackMode != AttackMode.Chemist ||
            GameObject.Find("GameManager").GetComponent<ChoosingManager>().SelectedChemistSkill != currentChemistSkill)
        {
            Debug.Log("Coroutine Stopped(Chemist)");
        }
        else
        {
            //Increment counter
            //Check if all turns are exhausted
            //If exhausted, change the state of TurnBasedCombatStateMachine
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().incrementTurn();

            if (GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().isTurnExhausted())
            {
                GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().currentState = TurnBasedCombatStateMachine.BattleStates.ENEMYCHOICE;
                GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().resetTurn();
            }

        }



    }
}
