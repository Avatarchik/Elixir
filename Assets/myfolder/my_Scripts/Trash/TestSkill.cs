using UnityEngine;
using System.Collections;

public class TestSkill : MonoBehaviour {
    ChoosingManager CManager;
    public bool isSkillInUse = false;

    public int skillIndex;
    IEnumerator skillInUse;
    IEnumerator waitSequence;

    private int count;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize(int index)
    {
        CManager = GetComponent<ChoosingManager>();
        skillIndex = index;
        count++;
        if (isSkillInUse)//If another skill is currently in use
        {
            Debug.Log("Already skill in use");
            if(skillIndex != CManager.SelectedSkill)
            {
                Debug.Log("Different Skill: Activate new skill");
                StopCoroutine(waitSequence); //Stop nested coroutine
                StopCoroutine(skillInUse); //Stop previous coroutine

                CManager.SelectedSkill = skillIndex;
                skillInUse = processSkill(); //Load new coroutine
                StartCoroutine(skillInUse);
            }else
            {
                Debug.Log("Same Skill: Do nothing");
            }
            
        }
        else
        {
            Debug.Log("New skill");
            CManager.SelectedSkill = skillIndex;

            skillInUse = processSkill();
            StartCoroutine(skillInUse);
        }
        
    }

    IEnumerator processSkill()
    {
        isSkillInUse = true;
        Debug.Log("Before waitSequence");
        //------
        waitSequence = waitForSelection();
        yield return StartCoroutine(waitSequence);
        //------
        Debug.Log("After waitSequence");

        yield return null;
    }

    IEnumerator waitForSelection()
    {
        int numCount;
        numCount = 0;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log(count);
            numCount++;
            if(numCount >= 5)
            {
                Debug.Log("Skill Expired");
                isSkillInUse = false;
                yield break;
            }
            yield return null;
        }
        
    }
}
