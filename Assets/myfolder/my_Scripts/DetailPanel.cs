using UnityEngine;
using System.Collections;

public class DetailPanel : MonoBehaviour {
    public GameObject DetailInfoPanel;

    private float timer;
    private bool checkPress = false;
    
    void Update()
    {
        if (checkPress)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                DetailInfoPanel.SetActive(true);
                DetailInfoPanel.GetComponent<SkillDetail>().DisplayInfo();
                checkPress = false;
            }
        }
    }

    public void CheckLongPress(int index)
    {
        checkPress = true;
        timer = 1.0f;
    }

    public void PressUp()
    {
        checkPress = false;
    }

}
