using UnityEngine;
using System.Collections;

public class DetailPanel : MonoBehaviour {
    public GameObject DetailInfoPanel;


    public void PressDown(int index)
    {
        DetailInfoPanel.SetActive(true);
        DetailInfoPanel.GetComponent<SkillDetail>().DisplayInfo(index);
    }

    public void PressUp()
    {
        if (DetailInfoPanel.activeSelf)
        {
            DetailInfoPanel.SetActive(false);
        }
    }

}
