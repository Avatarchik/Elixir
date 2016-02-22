using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {
    public RectTransform healthTransform;
    private float minXValue;
    private float maxXValue;
    private int currentHealth;
    private int maxHealth;

    private Monster monsterPref;
    void Start () {
        monsterPref = GameObject.Find("MonsterManager").GetComponent<MonsterPrefs>().monsterList[GetComponent<MonsterIndex>().MonsterID];

        maxHealth = monsterPref.maxHp;
        maxXValue = healthTransform.localPosition.x;
        minXValue = healthTransform.localPosition.x - healthTransform.rect.width;
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if(monsterPref.hp != currentHealth)
        {
            currentHealth = monsterPref.hp;
            HandleHealth();
        }
    }
    private void HandleHealth()
    {
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.localPosition = new Vector2(currentXValue, healthTransform.localPosition.y);
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
